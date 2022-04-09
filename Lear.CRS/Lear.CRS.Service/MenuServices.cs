

using Lear.CRS.Common;
using Lear.CRS.Common.HttpContextUser;
using Lear.CRS.IServices;
using Lear.CRS.Model;
using Lear.CRS.Model.Permission;
using Mapster;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yitter.IdGenerator;

namespace Lear.CRS.Services
{
    /// <summary>
    /// PermissionServices
    /// </summary>	
    public class MenuServices : IMenuServices
    {
        private readonly ISqlSugarRepository<MenuMaster> _menuRep;
        private readonly ISqlSugarRepository<ModuleMaster> _moduleRep;
        private readonly ISqlSugarRepository<UserRole> _userRoleRep;
        private readonly ISqlSugarRepository<RoleMenu> _roleMenuRep;
        private readonly IUser _user;

        public MenuServices(ISqlSugarRepository<MenuMaster> menuRep, ISqlSugarRepository<ModuleMaster> moduleRep,
            ISqlSugarRepository<UserRole> userRoleRep,
            IUser user)
        {
            this._menuRep = menuRep;
            this._moduleRep = moduleRep;
            this._userRoleRep = userRoleRep;
            this._user = user;
        }

        public async Task<List<MenuOutput>> All()
        {


            var list = await _menuRep.Context.Queryable<MenuMaster, ModuleMaster>((u, r) => new JoinQueryInfos(
            JoinType.Left, u.Mid == r.Id

            ))
                               .Select<MenuOutput>()
                              .ToListAsync();

            return list.OrderBy(x=>x.Sort).ToList();
        }


        public async Task<long> Add(AddMenuInput input)
        {
            var model = input.Adapt<MenuMaster>();
            model.CreateBy = this._user.Name;
            model.CreateTime = DateTime.Now;

            model.Id = YitIdHelper.NextId();

            return await _menuRep.InsertAsync(model);
        }
        public async Task<bool> Update(UpdateMenuInput input)
        {
            var model = input.Adapt<MenuMaster>();
            model.UpdateBy = this._user.Name;
            model.UpdateTime = DateTime.Now;


            return (await _menuRep.UpdateAsync(model)) > 0;
        }
        public async Task<bool> Delete(long menuId)
        {


            return await _menuRep.DeleteAsync(c => c.Id == menuId) > 0;
        }

        public async Task<List<MenuTreeOutput>> TreeList()
        {

            var menuList = await _menuRep.Where(c => c.Enabled == true).OrderBy(c=>c.Id).ToListAsync();


            var resList = menuList.OrderBy(x=>x.Sort).Select(u => new MenuTreeOutput
            {
                Id = u.Id,
                ParentId = u.Pid ?? 0,
                Value = u.Id.ToString(),
                Title = u.Name,
                Weight = u.Sort ?? 0
            }).ToList();
            return resList;
        }
    }
}
