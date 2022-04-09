
using Lear.CRS.Common.CustomExceptions;
using Lear.CRS.Common.HttpContextUser;
using Lear.CRS.IServices;
using Lear.CRS.Model;
using Lear.CRS.Model.Dto;
using Lear.CRS.SqlSugarCore.Extensions;
using Mapster;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yitter.IdGenerator;

namespace Lear.CRS.Service
{
    /// <summary>
    /// RoleServices
    /// </summary>	
    public class RoleServices : IRoleServices
    {
        private readonly ISqlSugarRepository<UserMaster> _userRep;
        private readonly ISqlSugarRepository<RoleMaster> _authRoleRepository;
        private readonly ISqlSugarRepository<UserRole> _userRoleRepository;
        private readonly ISqlSugarRepository<RoleMenu> _roleMenuRepository;
        private readonly ISqlSugarRepository<GroupMaster> _groupRepository;
        private readonly ISqlSugarRepository<GroupSource> _groupSrouceRepository;
        private readonly ISqlSugarRepository<UserGroup> _userGroupRepository;
        private readonly ISqlSugarRepository<MenuMaster> _menuRepository;
        private readonly IUser _user;
        private readonly IDictionaryConfigServices _dictionary;
        private readonly ISourceGroupServices _sourceGroupServices;
        private readonly IReSourceServices _resourceServices;

        public RoleServices(ISqlSugarRepository<RoleMaster> authRoleRepository, ISqlSugarRepository<MenuMaster> menuRepository,
            ISqlSugarRepository<UserMaster> userRep, ISqlSugarRepository<UserRole> userRoleRepository, ISqlSugarRepository<RoleMenu> roleMenuRepository,
            ISqlSugarRepository<GroupMaster> groupRepository,
            ISqlSugarRepository<GroupSource> groupSrouceRepository,
            ISqlSugarRepository<UserGroup> userGroupRepository,
            IUser user, IDictionaryConfigServices dictionary
            , ISourceGroupServices sourceGroupServices
            , IReSourceServices resourceServices

            )
        {
            _authRoleRepository = authRoleRepository;
            _userRoleRepository = userRoleRepository;
            _roleMenuRepository = roleMenuRepository;
            this._groupRepository = groupRepository;
            this._groupSrouceRepository = groupSrouceRepository;
            this._userGroupRepository = userGroupRepository;
            _menuRepository = menuRepository;
            _userRep = userRep;
            _user = user;
            this._dictionary = dictionary;
            this._sourceGroupServices = sourceGroupServices;
            this._resourceServices = resourceServices;
        }

        [SqlSugarUnitOfWork]
        public async Task<long> Add(AddRoleInput input)
        {

            var h = await _authRoleRepository.AnyAsync(c => c.Name.Equals(input.Name));
            if (h)
            {
                throw new BusinessException("role name exists");
            }
            var model = input.Adapt<RoleMaster>();
            model.CreateBy = this._user.Name;
            model.CreateTime = DateTime.Now;

            model.Id = YitIdHelper.NextId();

            await _authRoleRepository.InsertAsync(model);



            //var listUserRole = new List<UserRole>();
            //input.UserIds.ForEach(c =>
            //{
            //    var userRole = new UserRole
            //    {
            //        CreateBy = _user.Name,
            //        CreateTime = DateTime.Now,
            //        RoleId = model.Id,
            //        UserId = c,
            //        Id = YitIdHelper.NextId()
            //    };
            //    listUserRole.Add(userRole);

            //});
            //await _userRoleRepository.InsertAsync(listUserRole);


            var listRoleMenu = new List<RoleMenu>();
            input.MenuIds.ForEach(c =>
            {
                var roleMenu = new RoleMenu
                {
                    CreateBy = _user.Name,
                    CreateTime = DateTime.Now,
                    RoleId = model.Id,
                    MenuId = c,
                    Id = YitIdHelper.NextId()
                };
                listRoleMenu.Add(roleMenu);

            });
            await _roleMenuRepository.InsertAsync(listRoleMenu);

            return model.Id;
        }
        [SqlSugarUnitOfWork]

        public async Task<bool> Del(long id)
        {


            var roleDefaultId = await GetDefaultRoleId();

            if (id == roleDefaultId)
            { 
                throw new BusinessException("The default role cannot be deleted");

            }
            return await _authRoleRepository.DeleteAsync(x => x.Id == id) > 0 &&
            await _userRoleRepository.DeleteAsync(x => x.RoleId == id) > 0 && await _roleMenuRepository.DeleteAsync(c => c.RoleId == id) > 0;

        }



        public async Task<long> GetDefaultRoleId()
        {
            var roleCode = await _dictionary.GetDefault("DefaultRole");

            if (string.IsNullOrEmpty(roleCode))
                return 0;
            return (await _authRoleRepository.FirstOrDefaultAsync(c => c.Name == roleCode && c.Enabled == true)).Id;

        }



        public async Task<List<RoleOutput>> GetRoleList(QueryRoleInput query)
        {
            var roleList = await _authRoleRepository.Entities
                 .WhereIF(query.Enabled.HasValue, m => m.Enabled.Value == query.Enabled)
                 .WhereIF(!string.IsNullOrWhiteSpace(query.Name), m => m.Name.Contains(query.Name.Trim()))
                 .ToListAsync();


            var resList = roleList.Adapt<List<RoleOutput>>();

            var roleIds = resList.Select(c => c.Id).ToList();
           // var userRoleData = await _userRoleRepository.ToListAsync(x => roleIds.Contains(x.RoleId));
            var roleMenuData = await _roleMenuRepository.ToListAsync(x => roleIds.Contains(x.RoleId));

            resList.ForEach(x =>
            {
               // x.UserIds = userRoleData.Where(c => c.RoleId == x.Id).Select(c => c.UserId).ToList();
                x.MenuIds = roleMenuData.Where(c => c.RoleId == x.Id).Select(c => c.MenuId).ToList();

            });


            return resList;
        }



        public async Task<dynamic> Page(QueryRoleInput query)
        {

            var list = await _authRoleRepository.Context.Queryable<RoleMaster>()
         .WhereIF(!string.IsNullOrWhiteSpace(query.SearchValue), m => m.Name.Contains(query.SearchValue.Trim()))
         .Select<RoleOutput>()
          .ToPagedListAsync(query.PageNo, query.PageSize);








            return list.XnPagedResult();


        }
        [SqlSugarUnitOfWork]
        public async Task<bool> Update(UpdateRoleInput input)
        {
            var model = input.Adapt<RoleMaster>();
            model.UpdateTime = DateTime.Now;
            model.UpdateBy = this._user.Name;

            await _userRoleRepository.DeleteAsync(c => c.RoleId == input.Id);
            await _roleMenuRepository.DeleteAsync(c => c.RoleId == input.Id);
            //如禁用,则删除所有关联
            if (input.Enabled == true)
            {
                //var listUserRole = new List<UserRole>();
                //input.UserIds.ForEach(c =>
                //{
                //    var userRole = new UserRole
                //    {
                //        CreateBy = _user.Name,
                //        CreateTime = DateTime.Now,
                //        RoleId = model.Id,
                //        UserId = c,
                //        Id = YitIdHelper.NextId()
                //    };
                //    listUserRole.Add(userRole);

                //});
                //if (listUserRole.Count > 0)
                //    await _userRoleRepository.InsertAsync(listUserRole);


                var listRoleMenu = new List<RoleMenu>();
                input.MenuIds.ForEach(c =>
                {
                    var roleMenu = new RoleMenu
                    {
                        CreateBy = _user.Name,
                        CreateTime = DateTime.Now,
                        RoleId = model.Id,
                        MenuId = c,
                        Id = YitIdHelper.NextId()
                    };
                    listRoleMenu.Add(roleMenu);

                });
                if (listRoleMenu.Count > 0)
                    await _roleMenuRepository.InsertAsync(listRoleMenu);

            }

            return await _authRoleRepository.UpdateAsync(model) > 0;
        }



        public async Task<AssessExcelDto> ExportData()
        {

            var userList = await _userRep.Entities.OrderBy(c => c.EmpId).ToListAsync();
            var rolesAllList = await _authRoleRepository.Entities.OrderBy(c => c.Sort).ToListAsync();
            var input = new QuerySourceGroupInput();
            var sourceGroupAllList = await _sourceGroupServices.All(input);
            var resourceAllList = await _resourceServices.All();


            var userRoleList = await _userRoleRepository.Context.Queryable<UserRole, RoleMaster>((u, r) => new JoinQueryInfos(
            JoinType.Left, u.RoleId == r.Id
                        ))
                              .Select((u, r) => new { UserId = u.UserId, RoleId = r.Id, RoleName = r.Name })
                              .ToListAsync();
            var userGroupList = await _userGroupRepository.Context.Queryable<UserGroup, GroupMaster>((u, r) => new JoinQueryInfos(
      JoinType.Left, u.GroupId == r.Id
                  ))
                        .Select((u, r) => new { UserId = u.UserId, GroupId = r.Id, GroupName = r.Name })
                        .ToListAsync();


            var menusList = await _userRep.Context.Queryable<RoleMenu, MenuMaster>((r, m) => new JoinQueryInfos(
               JoinType.Left, r.MenuId == m.Id

               ))
                .Where((r, m) => m.Id > 0)

                              .Select((r, m) => new { RoleId = r.RoleId, MenuId = m.Id })
                              .ToListAsync();


            var resourceList = await _groupSrouceRepository.Context.Queryable<GroupSource, ResourceMaster>((r, m) => new JoinQueryInfos(
             JoinType.Left, r.ResourceId == m.Id

             ))
                 .Where((r, m) => m.Id > 0)
                            .Select((r, m) => new { GroupId = r.GroupId, ResourceId = m.Id })
                            .ToListAsync();
            var menusAllList = await _menuRepository.Where(c => c.Enabled == true && (c.Type == 1 || c.Type == 0)).Select<MenuOutput>().ToListAsync();


            var newMenuAllList = new List<MenuOutput>();
            var PmenuAllList = new List<MenuOutput>();
            var newResourceAllList = new List<ResourceOutput>();
            var PresourceAllList = new List<ResourceOutput>();
            var allGroupList = sourceGroupAllList.Select(c => "Data Access Group_" + c.Name).ToList();

            PmenuAllList.AddRange(menusAllList.Where(c => c.Pid == null || c.Pid == 0).OrderBy(c => c.Sort).ToList());

            PmenuAllList.ForEach(c =>
            {
                newMenuAllList.Add(c);
                newMenuAllList.AddRange(menusAllList.Where(m => m.Pid == c.Id).OrderBy(m => m.Sort).ToList());
            });

            PresourceAllList.AddRange(resourceAllList.Where(c => c.ParentId == null || c.ParentId == 0).OrderBy(c => c.Sort).ToList());

            //PresourceAllList.ForEach(c =>
            //{
            //    newResourceAllList.Add(c);
            //    newResourceAllList.AddRange(resourceAllList.Where(m => m.ParentId == c.Id).OrderBy(m => m.Sort).ToList());
            //});

            PresourceAllList.ForEach(c =>
            {
                c.ResourceDesc = $"{c.ResourceDesc}";
                newResourceAllList.Add(c);

                var resourceListdep = resourceAllList.Where(m => m.ParentId == c.Id).OrderBy(m => m.Sort).ToList();
                resourceListdep.ForEach(r =>
                {
                    r.ResourceDesc = $"{c.ResourceDesc}_{r.ResourceDesc}";
                });
                newResourceAllList.AddRange(resourceListdep);
            });



            List<string> listColumn = new List<string>();
            List<Dictionary<int, string>> list = new List<Dictionary<int, string>>();
            //List<AssessExcelDto> list = new List<AssessExcelDto>();
            listColumn.Add("Employee Number");
            listColumn.Add("Employee Name");
            listColumn.Add("Last Name");
            listColumn.Add("First  Name");
            listColumn.Add("Email");
            listColumn.Add("Account");
            listColumn.Add("Created Date");
            listColumn.Add("Created By");
            listColumn.Add("Last Modified By");
            listColumn.Add("Last Modified Date");
            listColumn.Add("Status");
            listColumn.Add("LastLoginTime");

            listColumn.AddRange(rolesAllList.Select(c => "Role_" + c.Name));
            listColumn.AddRange(newMenuAllList.Select(c => "Menu_" + c.Name));
            listColumn.AddRange(sourceGroupAllList.Select(c => "Data Access Group_" + c.Name));
            listColumn.AddRange(newResourceAllList.Select(c => "Data Access_" + c.ResourceDesc));


            userList.ForEach(c =>
            {
                Dictionary<int, string> dic = new Dictionary<int, string>();
                var userRole = userRoleList.Where(x => x.UserId == c.Id).ToList();
                var menus = menusList.Where(x => userRole.Select(s => s.RoleId).ToList().Contains(x.RoleId)).Select(c => c.MenuId).ToList();
                var userGroup = userGroupList.Where(x => x.UserId == c.Id).ToList();
                var resource = resourceList.Where(x => userGroup.Select(s => s.GroupId).ToList().Contains(x.GroupId)).Select(c => c.ResourceId).ToList();
                dic.Add(0, c.EmpId);
                dic.Add(1, c.FullName);
                dic.Add(2, c.LastName);
                dic.Add(3, c.FirstName);
                dic.Add(4, c.Email);
                dic.Add(5, c.Account);
                dic.Add(6, c.CreateBy);
                dic.Add(7, c.CreateTime.ToString("yyyy-MM-dd HH:mm:ss"));
                dic.Add(8, c.UpdateBy);
                dic.Add(9, c.UpdateTime.HasValue ? c.UpdateTime.Value.ToString("yyyy-MM-dd HH:mm:ss") : "");

                dic.Add(10, c.Active == 1 ? "Active" : "InActive");
                dic.Add(11, c.LastLoginTime.HasValue ? c.LastLoginTime.Value.ToString("yyyy-MM-dd HH:mm:ss") : "");
                int i = 11;
                rolesAllList.ForEach(f =>
                {

                    i++;
                    if (userRole.Any(m => m.RoleId == f.Id))
                        dic.Add(i, "Y");
                    else
                        dic.Add(i, "N");

                });
                newMenuAllList.ForEach(f =>
                {

                    i++;
                    if (menus.Any(m => m == f.Id))
                        dic.Add(i, "Y");
                    else
                        dic.Add(i, "N");

                });
                sourceGroupAllList.ForEach(f =>
                {

                    i++;
                    if (f.UserIds.Any(m => m == c.Id))
                        dic.Add(i, "Y");
                    else
                        dic.Add(i, "N");

                });
                newResourceAllList.ForEach(f =>
                {

                    i++;
                    if (resource.Any(m => m == f.Id))
                        dic.Add(i, "Y");
                    else
                        dic.Add(i, "N");

                });
                list.Add(dic);

            });





            var res = new AssessExcelDto
            {
                Column = listColumn,
                Data = list
            };

            return res;
        }
    }
}
