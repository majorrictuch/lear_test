

using Lear.CRS.Common;
using Lear.CRS.Common.HttpContextUser;
using Lear.CRS.IServices;
using Lear.CRS.Model;
using Lear.CRS.Model.Dto;
using Lear.CRS.Model.Permission;
using Mapster;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Yitter.IdGenerator;

namespace Lear.CRS.Services
{
    /// <summary>
    /// PermissionServices
    /// </summary>	
    public class PermissionServices : IPermissionServices
    {
        private readonly ISqlSugarRepository<MenuMaster> _permissionRep;
        private readonly ISqlSugarRepository<ModuleMaster> _moduleRep;
        private readonly ISqlSugarRepository<UserRole> _userRoleRep;
        private readonly ISqlSugarRepository<RoleMenu> _roleMenuRep;
        private readonly ISqlSugarRepository<GroupSource> _groupSourceRep;
        private readonly ISqlSugarRepository<ResourceMaster> _resourceMasterRep;
        private readonly ISqlSugarRepository<UserGroup> _userGroupRep;
        private readonly IUser _user;

        public PermissionServices(ISqlSugarRepository<MenuMaster> permissionRep, ISqlSugarRepository<ModuleMaster> moduleRep,
            ISqlSugarRepository<UserRole> userRoleRep,
            ISqlSugarRepository<RoleMenu> roleMenuRep, ISqlSugarRepository<GroupSource> groupSourceRep,
            ISqlSugarRepository<ResourceMaster> resourceMasterRep,
            ISqlSugarRepository<UserGroup> userGroupRep,
            IUser user

            )
        {
            this._permissionRep = permissionRep;
            this._moduleRep = moduleRep;
            this._userRoleRep = userRoleRep;
            this._roleMenuRep = roleMenuRep;
            this._groupSourceRep = groupSourceRep;
            this._resourceMasterRep = resourceMasterRep;
            this._userGroupRep = userGroupRep;
            this._user = user;
        }

        public async Task<List<MenuCurrentOutput>> GetMenus()
        {

            var roles = _user.GetClaimValueByType(ClaimTypes.Role);
            var roleMenus = await _roleMenuRep.ToListAsync(c => roles.Contains(c.RoleId.ToString()));
            var menuList = await _permissionRep.ToListAsync(c => c.Enabled == true);


            //_unitOfWork.BeginTran();

            //_unitOfWork.CommitTran();

            //_unitOfWork.RollbackTran();


            var list = menuList.Where(c => roleMenus.Select(c => c.MenuId).Contains(c.Id)).OrderBy(c => c.Sort);



            return list.Adapt<List<MenuCurrentOutput>>();
        }

        public async Task<List<PlanDepartmentOutput>> GetPlanDepartment()
        {
            var userID = this._user.ID;
            var userGroupList = _userGroupRep.Where(x => x.UserId == userID).Select(x => x.GroupId).ToList();
            var groupSourceList = _groupSourceRep.Where(x => userGroupList.Contains(x.GroupId)).Select(x => x.ResourceId).ToList();


            var list = await _resourceMasterRep.Where(x => groupSourceList.Contains(x.Id)).OrderBy(x=>x.Sort).ToListAsync();

            return list.Adapt<List<PlanDepartmentOutput>>();
        }


        /// <summary>
        /// 获取所有权限集合
        /// </summary>
        /// <returns></returns>
        public async Task<List<RoleModulePermission>> GetAllPermission()
        {
            var menuList = await _permissionRep.ToListAsync(c => c.Enabled == true);
            var moduleList = await _moduleRep.ToListAsync();
            var roleMenus = await _roleMenuRep.ToListAsync();


            var list = from c in roleMenus
                       join m in menuList on c.MenuId equals m.Id
                       join r in moduleList on m.Mid equals r.Id
                       select new RoleModulePermission
                       {
                           Module = r.Adapt<Modules>(),
                           Permission = m.Adapt<Permission>(),
                           Role = c.Adapt<Role>(),
                       };



            return list.ToList();
        }

        public async Task<bool> Assign(AssignInput assign)
        {
            if (assign.RoleId <= 0)
                return false;


            var roleModulePermissions = await _roleMenuRep.ToListAsync(d => d.RoleId == assign.RoleId);

            var removeIds = roleModulePermissions.Select(c => c.Id.ToString()).ToArray();
            if (removeIds.Any())
                await _roleMenuRep.DeleteAsync(c => removeIds.Contains(c.Id.ToString()));

            List<RoleMenu> list = new();

            foreach (var item in assign.MenuIds)
            {
                RoleMenu roleMenu = new()
                {
                    CreateBy = _user.Name,
                    CreateTime = DateTime.Now,
                    RoleId = assign.RoleId,
                    Id = YitIdHelper.NextId(),
                    MenuId = item

                };

                list.Add(roleMenu);
            }

            return (await _roleMenuRep.InsertAsync(list)) > 0;

        }
        [SqlSugarUnitOfWork]
        public async Task<bool> UserBind(UserBindInput input)
        {

            await _userRoleRep.DeleteAsync(c => c.UserId == input.UserId);
            await _userGroupRep.DeleteAsync(c => c.UserId == input.UserId);

            var listUserGroup = new List<UserGroup>();
            input.GroupIds.ForEach(c =>
            {
                var userGroup = new UserGroup
                {
                    CreateBy = _user.Name,
                    CreateTime = DateTime.Now,
                    GroupId = c,
                    UserId = input.UserId,
                    Id = YitIdHelper.NextId()
                };
                listUserGroup.Add(userGroup);

            });

            await _userGroupRep.InsertAsync(listUserGroup);

            var listUserRole = new List<UserRole>();
            input.RoleIds.ForEach(c =>
            {
                var userRole = new UserRole
                {
                    CreateBy = _user.Name,
                    CreateTime = DateTime.Now,
                    RoleId = c,
                    UserId = input.UserId,
                    Id = YitIdHelper.NextId()
                };
                listUserRole.Add(userRole);

            });

            await _userRoleRep.InsertAsync(listUserRole);

            return true;

        }

        public async Task<List<long>> GetAssignByRole(long roleId)
        {

            var rmps = await _roleMenuRep.ToListAsync(d => d.RoleId == roleId);

            return rmps.Select(c => c.MenuId).ToList();

        }

        public async Task<List<long>> GetResourceByGroup(long groupId)
        {
            var rmps = await _groupSourceRep.ToListAsync(d => d.GroupId == groupId);

            return rmps.Select(c => c.ResourceId).ToList();
        }
    }
}
