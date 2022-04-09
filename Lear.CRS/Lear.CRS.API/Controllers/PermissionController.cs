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
using Lear.CRS.Model.Dto;
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
    [ApiController]
    [Authorize]

    public class PermissionController : BaseController
    {
        readonly IPermissionServices _permissionServices;
        readonly IModuleServices _moduleServices;
        readonly IMenuServices _menuServices;
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
        public PermissionController(IPermissionServices permissionServices, IModuleServices moduleServices, IMenuServices menuServices, IUserRoleServices userRoleServices, IHttpContextAccessor httpContext, IUser user, PermissionRequirement requirement)
        {
            _permissionServices = permissionServices;
            _moduleServices = moduleServices;
            _menuServices = menuServices;
            _userRoleServices = userRoleServices;
            _httpContext = httpContext;
            _user = user;
            _requirement = requirement;
        }
       
        /// <summary>
        /// 获取所有菜单(当前登录用户)
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        
        public async Task<ApiResult<List<MenuCurrentOutput>>> GetMenus()
        {

            var permissions = await _permissionServices.GetMenus();

            return Success(permissions);

        }

        /// <summary>
        /// 获取所有工厂-部门(当前登录用户)
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        

        public async Task<ApiResult<List<PlanDepartmentOutput>>> GetPlanDepartment()
        {

            var permissions = await _permissionServices.GetPlanDepartment();

            return Success(permissions);

        }


        /// <summary>
        /// 获取菜单树
        /// </summary>
        /// <param name="pid"></param>
        /// <param name="needbtn"></param>
        /// <returns></returns>
        [HttpGet]
        
        public async Task<ApiResult<List<MenuTreeOutput>>> GetMenuTree()=> Success(await _menuServices.TreeList());




        /// <summary>
        /// 角色配置权限
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        

        [HttpPost]
        public async Task<ApiResult<bool>> Assign(AssignInput input)=> Success(await _permissionServices.Assign(input));




        /// <summary>
        /// 分配角色和资源组
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        

        [HttpPost]
        public async Task<ApiResult<bool>> UserBind(UserBindInput input) => Success(await _permissionServices.UserBind(input));


        /// <summary>
        /// 通过角色获取菜单【无权限】
        /// </summary>
        /// <param name="rid"></param>
        /// <returns></returns>
        [HttpGet]
        
        public async Task<ApiResult<List<long>>> GetAssignByRoleId(long rid = 0) => Success(await _permissionServices.GetAssignByRole(rid));
        [HttpGet]

        /// <summary>
        /// 通过组id获取组下资源【无权限】
        /// </summary>
        /// <param name="rid"></param>
        /// <returns></returns>
        
        public async Task<ApiResult<List<long>>> GetResourceByGroupId(long gid = 0) => Success(await _permissionServices.GetResourceByGroup(gid));

        public class AssignShow
        {
            public List<int> permissionids { get; set; }
            public List<string> assignbtns { get; set; }
        }

    }


}
