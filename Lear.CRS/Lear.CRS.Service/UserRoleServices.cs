
using Lear.CRS.Common.HttpContextUser;
using Lear.CRS.IServices;
using Lear.CRS.Model;
using Lear.CRS.Model.Dto;
using Mapster;
using Microsoft.AspNetCore.Mvc;
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
    public class UserRoleServices : IUserRoleServices
    {
        private readonly ISqlSugarRepository<UserRole> _authUserRole;
        private readonly ISqlSugarRepository<RoleMaster> _roleRep;
        private readonly ISqlSugarRepository<UserMaster> _userRep;
        private readonly ISqlSugarRepository<MenuMaster> _menuRep;
        private readonly IUser _user;

        public UserRoleServices(ISqlSugarRepository<UserRole> authUserRole, ISqlSugarRepository<RoleMaster> roleRep, ISqlSugarRepository<UserMaster> userRep, ISqlSugarRepository<MenuMaster> menuRep, IUser user)
        {
            _authUserRole = authUserRole;
            this._roleRep = roleRep;
            this._userRep = userRep;
            this._menuRep = menuRep;
            this._user = user;
        }


        /// <summary>
        /// 获取用户的角色Id集合
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<List<long>> GetUserRoleIdList(long userId)
        {
            var roles = await _authUserRole.Where(u => u.UserId == userId).ToListAsync();

            return roles.Select(u => u.RoleId).ToList();
        }

        public async Task<List<long>> GetRoleIdsByUserId(long id)
        {

            return await _authUserRole.Context.Queryable<UserRole, RoleMaster>((e, p) => new JoinQueryInfos(
                 JoinType.Left, e.RoleId == p.Id
                 ))
                .Where((e, p) => e.UserId == id)
                                      .Select((e, p) => e.RoleId).ToListAsync();

        }

        public async Task<List<RoleMaster>> GetRoleByUserId(long userId)
        {
            return await _authUserRole.Context.Queryable<UserRole, RoleMaster>((e, p) => new JoinQueryInfos(
           JoinType.Left, e.RoleId == p.Id
           ))
                                .Select((e, p) => p).ToListAsync();


        }

        public async Task<List<long>> GetUserIdsByRoleId(long rid)
        {


            var list = await _authUserRole.Where(c => c.RoleId == rid).ToListAsync();

            return list.Select(c => c.UserId).ToList();
        }

        /// <summary>
        /// 角色绑定用户
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<bool> BindToUser(BindToUserInput input)
        {
            var user = await _userRep.FirstOrDefaultAsync(x => x.Id == input.UserId);

            var items = await _authUserRole.Where(c => c.UserId == input.UserId).ToListAsync();
            if (items.Any())
                await _authUserRole.DeleteAsync(c => c.UserId == input.UserId);

            List<UserRole> list = new();
            foreach (var item in input.RoleId)
            {
                var userRole = new UserRole()
                {
                    CreateBy = _user.Name,
                    CreateTime = DateTime.Now,
                    Id = YitIdHelper.NextId(),
                    RoleId = item,
                    UserId = user.Id
                };
                list.Add(userRole);
            }
            return (await _authUserRole.InsertAsync(list)) > 0;
        }


        /// <summary>
        /// 给用户绑定角色
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<bool> BindToRole(BindToRoleInput input)
        {
            var role = await _roleRep.FirstOrDefaultAsync(x => x.Id == input.RoleId);
            var items = await _authUserRole.Where(c => c.RoleId == input.RoleId).ToListAsync();
            if (items.Any())
                await _authUserRole.DeleteAsync(c => c.RoleId == input.RoleId);
            List<UserRole> list = new();
            foreach (var item in input.UserId)
            {
                var userRole = new UserRole()
                {
                    CreateBy = _user.Name,
                    CreateTime = DateTime.Now,
                    Id = YitIdHelper.NextId(),
                    RoleId = role.Id,
                    UserId = item
                };
                list.Add(userRole);
            }
            return (await _authUserRole.InsertAsync(list)) > 0;
        }




        public async Task<AssessExcelDto> ExportData()
        {

            var userList = await _userRep.ToListAsync(x => x.Active == 1);
            var rolesAllList = await _roleRep.ToListAsync();


            var userRoleList = await _authUserRole.Context.Queryable<UserRole, RoleMaster>((u, r) => new JoinQueryInfos(
            JoinType.Left, u.RoleId == r.Id
                        ))
                              .Select((u, r) => new { UserId = u.UserId, RoleId = r.Id, RoleName = r.Name })
                              .ToListAsync();


            var menusList = await _userRep.Context.Queryable<RoleMenu, MenuMaster>((r, m) => new JoinQueryInfos(
               JoinType.Left, r.MenuId == m.Id

               ))
                              .Select((r, m) => new { RoleId = r.Id, MenuId = m.Id })
                              .ToListAsync();
            var menusAllList = (await _menuRep.ToListAsync(c => c.Enabled == true && (c.Type == 1 || c.Type == 0))).OrderBy(c => c.Sort).Select(c => new { MenuId = c.Id, MenuName = c.Name, NewSort = c.Sort + c.Type * 10 }).ToList();

            menusAllList.OrderBy(c => c.NewSort);
            List<string> listColumn = new List<string>();
            List<Dictionary<int, string>> list = new List<Dictionary<int, string>>();
            //List<AssessExcelDto> list = new List<AssessExcelDto>();
            listColumn.Add("Account");
            listColumn.Add("LastLoginTime");
            listColumn.AddRange(rolesAllList.Select(c => c.Name));
            listColumn.AddRange(menusAllList.Select(c => c.MenuName));


            userList.ForEach(c =>
            {
                Dictionary<int, string> dic = new Dictionary<int, string>();
                var userRole = userRoleList.Where(x => x.UserId == c.Id).ToList();
                var menus = menusList.Where(x => userRole.Select(s => s.RoleId).ToList().Contains(x.RoleId)).Select(c => c.MenuId).ToList();
                dic.Add(0, c.Account);
                dic.Add(1, DateTime.Now.ToString());


                int i = 1;
                rolesAllList.ForEach(f =>
                {

                    i++;
                    if (userRole.Any(m => m.RoleId == f.Id))
                        dic.Add(i, "Y");
                    else
                        dic.Add(i, "N");

                });
                menusAllList.ForEach(f =>
                {

                    i++;
                    if (menus.Any(m => m == f.MenuId))
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
