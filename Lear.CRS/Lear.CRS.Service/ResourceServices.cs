
using Lear.CRS.Common.CustomExceptions;
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
    public class ResourceServices : IReSourceServices
    {
        private readonly ISqlSugarRepository<UserMaster> _userRep;
        private readonly ISqlSugarRepository<ResourceMaster> _resourceRep;
        private readonly ISqlSugarRepository<UserGroup> _userGroupRep;
        private readonly ISqlSugarRepository<GroupMaster> _groupRep;
        private readonly IUser _user;

        public ResourceServices(ISqlSugarRepository<ResourceMaster> resourceRep, ISqlSugarRepository<UserMaster> userRep, ISqlSugarRepository<UserGroup> userGroupRep,
             ISqlSugarRepository<GroupMaster> groupRep,
            IUser user)
        {
            _resourceRep = resourceRep;
            _userRep = userRep;
            _userGroupRep = userGroupRep;
            _groupRep = groupRep;


            this._user = user;
        }

        public async Task<long> Add(AddResourceInput input)
        {

            var models = await _resourceRep.FirstOrDefaultAsync(x => x.ResourceName == input.ResourceName && x.ParentId == input.ParentId);

            if (models != null) {
                throw new BusinessException("ResourceName is exists !");
            }

            var model = input.Adapt<ResourceMaster>();
            model.CreateBy = this._user.Name;
            model.CreateTime = DateTime.Now;

            model.Id = YitIdHelper.NextId();
            return await _resourceRep.InsertAsync(model);
        }

        public async Task<bool> Del(long id)
        {

            return await _resourceRep.DeleteAsync(c => c.Id == id) > 0;
        }


        public async Task<List<ResourceOutput>> All()
        {
            var mouduleList = await _resourceRep.Entities.OrderBy(c=>c.Sort).ToListAsync();

            return mouduleList.Adapt<List<ResourceOutput>>();
        }
        public async Task<dynamic> Page(QueryResourceInput input)
        {


            var list = await _resourceRep.Context.Queryable<ResourceMaster>()
              .WhereIF(!string.IsNullOrWhiteSpace(input.SearchValue), m => m.ResourceName.Contains(input.SearchValue.Trim()))
              .Select<ResourceOutput>()
               .ToPagedListAsync(input.PageNo, input.PageSize);
            return list.XnPagedResult();
           
        }

        public async Task<bool> Update(UpdateResourceInput input)
        {
            var model = await _resourceRep.FirstOrDefaultAsync(x=>x.Id == input.Id);

            model.ParentId = input.ParentId;
            model.Sort = input.Sort;
            model.ResourceDesc = input.ResourceDesc;

            model.UpdateTime = DateTime.Now;
            model.UpdateBy = this._user.Name;

            return await _resourceRep.UpdateAsync(model) > 0;
        }



      
    }
}
