

using Lear.CRS.AuthHelper;
using Lear.CRS.Common;
using Lear.CRS.Common.Helper;
using Lear.CRS.IService;
using Lear.CRS.IServices;
using Lear.CRS.Model;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Lear.CRS.API.Controllers
{
    /// <summary>
    /// 登录管理【无权限】
    /// </summary>
    [Produces("application/json")]
    [AllowAnonymous]
    public class AuthController : BaseController
    {
        private readonly IUserServices _userServices;
        private readonly IUserRoleServices _userRoleServices;
        private readonly IRoleServices _roleServices;
        private readonly PermissionRequirement _requirement;
        private readonly IRoleModulePermissionServices _roleModulePermissionServices;
        private readonly IAuthService _authService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthController(IUserServices userServices,
            IUserRoleServices userRoleServices,
            IRoleServices roleServices,
            PermissionRequirement requirement,
            IRoleModulePermissionServices roleModulePermissionServices,
            IAuthService authService,
            IHttpContextAccessor httpContextAccessor)
        {
            this._userServices = userServices;
            this._userRoleServices = userRoleServices;
            this._roleServices = roleServices;
            _requirement = requirement;
            _roleModulePermissionServices = roleModulePermissionServices;
            this._authService = authService;
            this._httpContextAccessor = httpContextAccessor;
        }


       /// <summary>
       /// login
       /// </summary>
       /// <param name="input"></param>
       /// <returns></returns>
        [HttpGet]
        [Route("Login")]
        public async Task<ApiResult<TokenInfoViewModel>> GetJwtToken3(LoginInput input)
        {
            string jwtStr = string.Empty;

            if (string.IsNullOrEmpty(input.Password) || string.IsNullOrEmpty(input.Account))
                return Failed<TokenInfoViewModel>("用户名或密码不能为空");


            var user = await _authService.LoginAsync(input);
            if (user.Item1)
            {

                //如果是基于用户的授权策略，这里要添加用户;如果是基于角色的授权策略，这里要添加角色
                var claims = new Dictionary<string, object>
            {
                {ClaimConst.CLAINM_USERID, user.id },
                {ClaimConst.CLAINM_ACCOUNT, user.Account},
                {ClaimConst.CLAINM_NAME, user.FullName },
                {ClaimConst.CLAINM_SUPERADMIN,"" },
            };


                // ids4和jwt切换
                // jwt
                if (!Permissions.IsUseIds4)
                {
                    var data = await _roleModulePermissionServices.RoleModuleMaps();
                    var list = (from item in data
                                orderby item.Id
                                select new PermissionItem
                                {
                                    Url = item.Module?.LinkUrl,
                                    Role = item.Role?.Name.ToString(),
                                }).ToList();

                    _requirement.Permissions = list;
                }

                var token = JwtToken.BuildJwtToken(claims.ToArray(), _requirement);

                // 设置Swagger自动登录
                //_httpContextAccessor.HttpContext.Response.Headers["access-token"] = token.token;

                return Success(token, "获取成功");
            }
            else
            {
                return Failed<TokenInfoViewModel>("认证失败");
            }
        }

    }
}