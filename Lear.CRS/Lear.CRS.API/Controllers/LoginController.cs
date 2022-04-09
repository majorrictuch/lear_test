

using Lear.CRS.AuthHelper;
using Lear.CRS.Common;
using Lear.CRS.Common.Helper;
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

    [Route("[controller]/[action]")]
    [ApiController]
    [AllowAnonymous]
    public class LoginController : BaseController
    {
        private readonly IUserServices _userServices;
        private readonly IUserRoleServices _userRoleServices;
        private readonly IRoleServices _roleServices;
        private readonly PermissionRequirement _requirement;
        private readonly IRoleModulePermissionServices _roleModulePermissionServices;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public LoginController(IUserServices userServices,
            IUserRoleServices userRoleServices,
            IRoleServices roleServices,
            PermissionRequirement requirement,
            IRoleModulePermissionServices roleModulePermissionServices,
            IHttpContextAccessor httpContextAccessor)
        {
            this._userServices = userServices;
            this._userRoleServices = userRoleServices;
            this._roleServices = roleServices;
            _requirement = requirement;
            _roleModulePermissionServices = roleModulePermissionServices;
            this._httpContextAccessor = httpContextAccessor;
        }



        /// <summary>
        /// login
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<TokenInfoViewModel>> Login(LoginInput input)
        {
            string jwtStr = string.Empty;

            if (string.IsNullOrEmpty(input.Account) || string.IsNullOrEmpty(input.Password))
                return Failed<TokenInfoViewModel>("用户名或密码不能为空");

            input.Password = MD5Helper.MD5Encrypt32(input.Password);

            var user = await _userServices.Login(input.Account, input.Password);


            if (user != null)
            {

                var roles = await _userRoleServices.GetRoleIdsByUserId(user.Id);
                //如果是基于用户的授权策略，这里要添加用户;如果是基于角色的授权策略，这里要添加角色
                var claims = new List<Claim> {
                    new Claim(ClaimConst.CLAINM_USERID, user.Id.ToString()),
                                        new Claim(ClaimTypes.Name, $"{user.FullName}({user.Account})"),
                      new Claim(ClaimTypes.Expiration, DateTime.Now.AddSeconds(_requirement.Expiration.TotalSeconds).ToString())
                    };
                claims.AddRange(roles.Select(s => new Claim(ClaimTypes.Role, s.ToString())));

                var token = JwtToken.BuildJwtToken(claims.ToArray(), _requirement);

                // 设置Swagger自动登录
                //_httpContextAccessor.HttpContext.Response.Headers["access-token"] = token.token;




                return Success(token, "获取成功");
            }
            else
            {
                return Failed<TokenInfoViewModel>("登录失败");
            }
        }



        /// <summary>
        /// login in
        /// </summary>
        /// <param name="name"></param>
        /// <param name="pass"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<TokenInfoViewModel>> LoginByAd(string name)
        {
            string jwtStr = string.Empty;

            if (string.IsNullOrEmpty(name))
                return Failed<TokenInfoViewModel>("用户名或密码不能为空");

            var res = await _userServices.FindInAd(name);
            if (res == null)
                return Failed<TokenInfoViewModel>("AD域中不存在");

            var user = await _userServices.Login(name);


            if (user != null)
            {

                var roles = await _userRoleServices.GetRoleIdsByUserId(user.Id);
                //如果是基于用户的授权策略，这里要添加用户;如果是基于角色的授权策略，这里要添加角色
                var claims = new List<Claim> {
                    new Claim(ClaimConst.CLAINM_USERID, user.Id.ToString()),
                    new Claim(ClaimTypes.Name, $"{user.FullName}({user.Account})"),
                      new Claim(ClaimTypes.Expiration, DateTime.Now.AddSeconds(_requirement.Expiration.TotalSeconds).ToString())
                    };
                claims.AddRange(roles.Select(s => new Claim(ClaimTypes.Role, s.ToString())));

                var token = JwtToken.BuildJwtToken(claims.ToArray(), _requirement);

                // 设置Swagger自动登录
                //_httpContextAccessor.HttpContext.Response.Headers["access-token"] = token.token;




                return Success(token, "获取成功");
            }
            else
            {
                return Failed<TokenInfoViewModel>("登录失败");
            }
        }


    }
}