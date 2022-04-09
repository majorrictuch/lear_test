
using Lear.CRS.Common.Helper;
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
using System.Security.Claims;
using System.Threading.Tasks;
using Yitter.IdGenerator;

namespace Lear.CRS.Service
{
    /// <summary>
    /// RoleServices
    /// </summary>	
    public class SourceGroupServices : ISourceGroupServices
    {
        private readonly ISqlSugarRepository<UserMaster> _userRep;
        private readonly ISqlSugarRepository<ResourceMaster> _resourceRep;
        private readonly ISqlSugarRepository<UserGroup> _userGroupRep;
        private readonly ISqlSugarRepository<GroupSource> _groupSourceRep;
        private readonly ISqlSugarRepository<GroupMaster> _groupRep;
        private readonly IDictionaryConfigServices _dictionary;
        private readonly IUser _user;

        public SourceGroupServices(ISqlSugarRepository<ResourceMaster> resourceRep, ISqlSugarRepository<UserMaster> userRep, ISqlSugarRepository<UserGroup> userGroupRep,
             ISqlSugarRepository<GroupMaster> groupRep, ISqlSugarRepository<GroupSource> groupSourceRep, IDictionaryConfigServices dictionary,
            IUser user)
        {
            _resourceRep = resourceRep;
            _userRep = userRep;
            _userGroupRep = userGroupRep;
            _groupSourceRep = groupSourceRep;
            _groupRep = groupRep;
            this._dictionary = dictionary;


            this._user = user;
        }

        public async Task<long> Add(AddSourceGroupInput input)
        {
            var model = input.Adapt<GroupMaster>();
            model.CreateBy = this._user.Name;
            model.CreateTime = DateTime.Now;

            model.Id = YitIdHelper.NextId();
            await _groupRep.InsertAsync(model);


            var listSourceGroup = new List<GroupSource>();
            input.ResourceIds.ForEach(c =>
            {
                var sourceGroup = new GroupSource
                {
                    CreateBy = _user.Name,
                    CreateTime = DateTime.Now,
                    GroupId = model.Id,
                    ResourceId = c,
                    Id = YitIdHelper.NextId()
                };
                listSourceGroup.Add(sourceGroup);

            });

            await _groupSourceRep.InsertAsync(listSourceGroup);



            var listUserGroup = new List<UserGroup>();
            input.UserIds.ForEach(c =>
            {
                var userGroup = new UserGroup
                {
                    CreateBy = _user.Name,
                    CreateTime = DateTime.Now,
                    GroupId = model.Id,
                    UserId = c,
                    Id = YitIdHelper.NextId()
                };
                listUserGroup.Add(userGroup);

            });

            await _userGroupRep.InsertAsync(listUserGroup);

            return model.Id;
        }

        public async Task<bool> Del(long id)
        {
            return await _groupRep.DeleteAsync(c => c.Id == id) > 0 && await _userGroupRep.DeleteAsync(c => c.GroupId == id) > 0 && await _groupSourceRep.DeleteAsync(c => c.GroupId == id) > 0;
        }

        public async Task<List<SourceGroupOutput>> All(QuerySourceGroupInput input)
        {


            var groupList = await _groupRep.Entities
                                    .WhereIF(!string.IsNullOrEmpty(input.Name),x=>x.Name.Contains(input.Name.Trim())).ToListAsync();


            var groupResList = groupList.Adapt<List<SourceGroupOutput>>();

            var groupIds = groupResList.Select(c => c.Id).ToList();
            var groupSourceData = await _groupSourceRep.ToListAsync(x => groupIds.Contains(x.GroupId));
            var userGroupData = await _userGroupRep.ToListAsync(x => groupIds.Contains(x.GroupId));

            groupResList.ForEach(x =>
            {
                x.RecourceIds = groupSourceData.Where(c => c.GroupId == x.Id).Select(c => c.ResourceId).ToList();
                x.UserIds = userGroupData.Where(c => c.GroupId == x.Id).Select(c => c.UserId).ToList();

            });




            return groupResList;
        }
        public async Task<dynamic> Page(QuerySourceGroupInput input)
        {

            var list = await _groupRep.Context.Queryable<GroupMaster>()
              .WhereIF(!string.IsNullOrWhiteSpace(input.SearchValue), m => m.Name.Contains(input.SearchValue.Trim()))
              .Select<UserOutput>()
               .ToPagedListAsync(input.PageNo, input.PageSize);
            return list.XnPagedResult();

        }

        public async Task<bool> Update(UpdateSourceGroupInput input)
        {
            var model = input.Adapt<GroupMaster>();
            model.UpdateTime = DateTime.Now;
            model.UpdateBy = this._user.Name;

            await _groupSourceRep.DeleteAsync(c => c.GroupId == input.Id);
            await _userGroupRep.DeleteAsync(c => c.GroupId == input.Id);

            var listSourceGroup = new List<GroupSource>();
            input.ResourceIds.ForEach(c =>
            {
                var sourceGroup = new GroupSource
                {
                    CreateBy = _user.Name,
                    CreateTime = DateTime.Now,
                    GroupId = model.Id,
                    ResourceId = c,
                    Id = YitIdHelper.NextId()
                };
                listSourceGroup.Add(sourceGroup);

            });

            await _groupSourceRep.InsertAsync(listSourceGroup);



            var listUserGroup = new List<UserGroup>();
            input.UserIds.ForEach(c =>
            {
                var userGroup = new UserGroup
                {
                    CreateBy = _user.Name,
                    CreateTime = DateTime.Now,
                    GroupId = model.Id,
                    UserId = c,
                    Id = YitIdHelper.NextId()
                };
                listUserGroup.Add(userGroup);

            });

            await _userGroupRep.InsertAsync(listUserGroup);

            return await _groupRep.UpdateAsync(model) > 0;
        }


        public async Task<AssessExcelDto> ExportData()
        {

            var groupAllList = await _groupRep.Entities.OrderBy(c => c.Id).ToListAsync();



            var resourceList = await _groupRep.Context.Queryable<GroupSource, ResourceMaster>((g, r) => new JoinQueryInfos(
               JoinType.Left, g.ResourceId == r.Id
               ))
                              .Select((g, r) => new { GroupId = g.GroupId, ResourceId = r.Id })
                              .ToListAsync();


            var resourceAllList = await _resourceRep.Entities.Select<ResourceOutput>().ToListAsync();


            var newResourceAllList = new List<ResourceOutput>();
            var pResourceAllList = new List<ResourceOutput>();

            pResourceAllList.AddRange(resourceAllList.Where(c => c.ParentId == null || c.ParentId == 0).OrderBy(c => c.Sort).ToList());

            pResourceAllList.ForEach(c =>
            {
                c.ResourceDesc = $"Data Access_{c.ResourceDesc}";
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

            listColumn.Add("Name");
            listColumn.AddRange(newResourceAllList.Select(c => c.ResourceDesc));


            groupAllList.ForEach(c =>
            {
                Dictionary<int, string> dic = new Dictionary<int, string>();
                var source = resourceList.Where(x => x.GroupId == c.Id).Select(c => c.ResourceId).ToList();
                dic.Add(0, c.Name);


                int i = 0;

                resourceAllList.ForEach(f =>
                        {

                            i++;
                            if (source.Any(m => m == f.Id))
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

        public async Task<List<long>> GetUserIdsByGroupId(long gid)
        {


            var list = await _userGroupRep.ToListAsync(c => c.GroupId == gid);

            return list.Select(c => c.UserId).ToList();
        }

        public async Task<long> GetDefaultGroupId()
        {
            var defaultCode = await _dictionary.GetDefault("DefaultGroup");
            if (string.IsNullOrEmpty(defaultCode))
                return 0;
            return (await _groupRep.FirstOrDefaultAsync(c => c.Name == defaultCode)).Id;

        }


        ///// <summary>
        ///// 角色绑定用户
        ///// </summary>
        ///// <param name="input"></param>
        ///// <returns></returns>
        //public async Task<bool> BindToUser(GroupToUserInput input)
        //{
        //    var user = await _userRep.FirstOrDefaultAsync(x => x.Id == input.UserId);
        //    var items = await _userGroupRep.Where(c => c.UserId == input.UserId).ToListAsync();
        //    if (items.Any())
        //        await _userGroupRep.DeleteAsync(c => c.UserId == input.UserId);
        //    List<UserGroup> list = new();
        //    foreach (var item in input.GroupId)
        //    {
        //        var usergroup = new UserGroup()
        //        {
        //            CreateBy = _user.Name,
        //            CreateTime = DateTime.Now,
        //            Id = YitIdHelper.NextId(),
        //            GroupId = item,
        //            UserId = user.Id
        //        };
        //        list.Add(usergroup);
        //    }
        //    return (await _userGroupRep.InsertAsync(list)) > 0;
        //}


        ///// <summary>
        ///// 给用户绑定角色
        ///// </summary>
        ///// <param name="input"></param>
        ///// <returns></returns>
        //public async Task<bool> BindToGroup(UserToGroupInput input)
        //{
        //    var group = _groupRep.Single(input.GroupId);
        //    var items = await _userGroupRep.ToListAsync(c => c.GroupId == input.GroupId);
        //    if (items.Any())
        //        await _userGroupRep.DeleteAsync(c => c.GroupId == input.GroupId);
        //    List<UserGroup> list = new();
        //    foreach (var item in input.UserId)
        //    {
        //        var usergroup = new UserGroup()
        //        {
        //            CreateBy = _user.Name,
        //            CreateTime = DateTime.Now,
        //            Id = YitIdHelper.NextId(),
        //            GroupId = group.Id,
        //            UserId = item
        //        };
        //        list.Add(usergroup);
        //    }
        //    return (await _userGroupRep.InsertAsync(list)) > 0;
        //}

        public async Task<List<long>> GetUserDataScopeIdList(long userId)
        {

            var roles = await _userGroupRep.Where(u => u.UserId == userId).ToListAsync();

            return roles.Select(u => u.GroupId).ToList();
        }

        public Task<List<string>> GetGroupIdsByUserId(long id)
        {
            throw new NotImplementedException();
        }

        public Task<List<GroupMaster>> GetGroupByUserId(long userId)
        {
            throw new NotImplementedException();
        }
    }
}
