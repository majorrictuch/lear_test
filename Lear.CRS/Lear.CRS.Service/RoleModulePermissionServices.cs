
using Lear.CRS.Common;
using Lear.CRS.IServices;
using Lear.CRS.Model;
using Lear.CRS.Model.Permission;
using Mapster;
using SqlSugar;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lear.CRS.Services
{
    /// <summary>
    /// RoleModulePermissionServices 应用服务
    /// </summary>	
    public class RoleModulePermissionServices : IRoleModulePermissionServices
    {

        readonly ISqlSugarRepository<RoleMenu> _accessRepository;
        readonly ISqlSugarRepository<ModuleMaster> _moduleRepository;
        readonly ISqlSugarRepository<RoleMaster> _roleRepository;
        readonly ISqlSugarRepository<MenuMaster> _menuRepository;

        // 将多个仓储接口注入
        public RoleModulePermissionServices(
            ISqlSugarRepository<RoleMenu> accessRepository,
            ISqlSugarRepository<ModuleMaster> moduleRepository,
            ISqlSugarRepository<MenuMaster> menuRepository,
            ISqlSugarRepository<RoleMaster> roleRepository)
        {

            this._accessRepository = accessRepository;
            this._moduleRepository = moduleRepository;
            this._menuRepository = menuRepository;
            this._roleRepository = roleRepository;

        }

        /// <summary>
        /// 获取全部 角色接口(按钮)关系数据
        /// </summary>
        /// <returns></returns>

        public async Task<List<RoleModulePermission>> GetRoleModule()
        {

            var roleModulePermissions = await _accessRepository.ToListAsync();
            var roles = await _roleRepository.ToListAsync();
            var modules = await _moduleRepository.ToListAsync();

            //var roleModulePermissionsAsync = base.Query(a => a.IsDeleted == false);
            //var rolesAsync = _roleRepository.Query(a => a.IsDeleted == false);
            //var modulesAsync = _moduleRepository.Query(a => a.IsDeleted == false);

            //var roleModulePermissions = await roleModulePermissionsAsync;
            //var roles = await rolesAsync;
            //var modules = await modulesAsync;

            var roleModulePermissionsAdt = roleModulePermissions.Adapt<List<RoleModulePermission>>();

            if (roleModulePermissionsAdt.Count > 0)
            {
                foreach (var item in roleModulePermissionsAdt)
                {
                    item.Role = roles.FirstOrDefault(d => d.Id == item.RoleId).Adapt<Role>();
                    item.Module = modules.FirstOrDefault(d => d.Id == item.ModuleId).Adapt<Modules>();
                }

            }
            return roleModulePermissionsAdt;
        }







        /// <summary>
        /// 查询出角色-菜单-接口关系表全部Map属性数据
        /// </summary>
        /// <returns></returns>
        public async Task<List<RoleModulePermission>> GetRMPMapsPage()
        {
            return await _accessRepository.Context.Queryable<RoleModulePermission>()
                .Mapper(rmp => rmp.Module, rmp => rmp.ModuleId)
                .Mapper(rmp => rmp.Permission, rmp => rmp.PermissionId)
                .Mapper(rmp => rmp.Role, rmp => rmp.RoleId)
                .ToPageListAsync(1, 5, 10);
        }

        public async Task<List<RoleModulePermission>> RoleModuleMaps()
        {


            var list = await _accessRepository.Context.Queryable<RoleMenu, MenuMaster, RoleMaster>((rmp, m, r) => new JoinQueryInfos(
              JoinType.Left, rmp.MenuId == m.Id,
                   JoinType.Left, rmp.RoleId == r.Id

              ))
                                 .Select((rmp, m, r) => new RoleModulePermission
                                 {
                                     RoleId = r.Id,
                                     ModuleId = m.Mid??0,
                                     PermissionId = m.Id ,

                                 }).ToListAsync();



            foreach (var item in list)
            {
                var role = _roleRepository.Single(item.RoleId);
                var module = _moduleRepository.Single(item.ModuleId);
                var menu = _menuRepository.Single(item.PermissionId);
                if (role != null)
                {
                    item.Role = role.Adapt<Role>();
                }
                if (module != null)
                {
                    item.Module = module.Adapt<Modules>();
                }
                if (menu != null)
                {
                    item.Permission = menu.Adapt<Permission>();
                }

            }



            return list;
        }

        public Task<List<RoleModulePermission>> GetRMPMaps()
        {
            throw new System.NotImplementedException();
        }
    }
}
