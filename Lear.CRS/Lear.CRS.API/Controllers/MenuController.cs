using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Lear.CRS.API.Controllers;
using Lear.CRS.AuthHelper;
using Lear.CRS.AuthHelper.OverWrite;
using Lear.CRS.Common;
using Lear.CRS.Common.Helper;
using Lear.CRS.Common.HttpContextUser;
using Lear.CRS.IServices;
using Lear.CRS.Model;
using Lear.CRS.Model.Permission;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Lear.CRS.API.Controllers
{
    /// <summary>
    /// 菜单管理
    /// </summary>
    [Route("[controller]/[action]")]
    [Authorize]
    [ApiController]
    

    public class MenuController : BaseController
    {
        readonly IMenuServices _menuServices;
        readonly IModuleServices _moduleServices;
        readonly IRoleModulePermissionServices _roleModulePermissionServices;
        readonly IUserRoleServices _userRoleServices;
        readonly IHttpContextAccessor _httpContext;
        readonly IUser _user;
        private readonly PermissionRequirement _requirement;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="permissionServices"></param>
        /// <param name="moduleServices"></param>
        /// <param name="roleModulePermissionServices"></param>
        /// <param name="userRoleServices"></param>
        /// <param name="httpContext"></param>
        /// <param name="user"></param>
        /// <param name="requirement"></param>
        public MenuController(IMenuServices menuServices, IModuleServices moduleServices, IRoleModulePermissionServices roleModulePermissionServices, IUserRoleServices userRoleServices, IHttpContextAccessor httpContext, IUser user, PermissionRequirement requirement)
        {
            _menuServices = menuServices;
            _moduleServices = moduleServices;
            _roleModulePermissionServices = roleModulePermissionServices;
            _userRoleServices = userRoleServices;
            _httpContext = httpContext;
            _user = user;
            _requirement = requirement;
        }
       
        /// <summary>
        /// 获取所有菜单
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<List<MenuOutput>>> All()
        {

            var list = await _menuServices.All();

            return Success(list);

        }


        /// <summary>
        /// 新增菜单
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult> Add(AddMenuInput input)
        {

            var res = await _menuServices.Add(input);

            return Success(res.ToString());

        }

        /// <summary>
        /// 更新菜单
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<bool>> Update(UpdateMenuInput input)
        {

            var res = await _menuServices.Update(input);

            return Success(res);

        }
        /// <summary>
        /// 删除菜单
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<ApiResult<bool>> Delete(long  menuId)
        {

            var res = await _menuServices.Delete(menuId);

            return Success(res);

        }
        

    }


}
