using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lear.CRS.AuthHelper.OverWrite;
using Lear.CRS.Common;
using Lear.CRS.Common.Helper;
using Lear.CRS.Common.HttpContextUser;
using Lear.CRS.IServices;
using Lear.CRS.Model;
using Lear.CRS.Model.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Lear.CRS.API.Controllers

{
    /// <summary>
    /// User module
    /// </summary>
    [Route("[controller]/[action]")]
    [ApiController]
    [Authorize]
    
    public class UserController : BaseController
    {
        readonly IUserServices _sysUserInfoServices;
        readonly IUserRoleServices _userRoleServices;
        readonly IRoleServices _roleServices;
        private readonly IUser _user;
        private readonly ILogger<UserController> _logger;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="sysUserInfoServices"></param>
        /// <param name="userRoleServices"></param>
        /// <param name="roleServices"></param>
        /// <param name="user"></param>
        /// <param name="logger"></param>
        public UserController(IUserServices sysUserInfoServices, IUserRoleServices userRoleServices, IRoleServices roleServices, IUser user, ILogger<UserController> logger)
        {
            _sysUserInfoServices = sysUserInfoServices;
            _userRoleServices = userRoleServices;
            _roleServices = roleServices;
            _user = user;
            _logger = logger;
        }

        /// <summary>
        /// user list
        /// </summary>
        /// <param name="input">query parmas</param>
        /// <returns>return user list</returns>
        [HttpGet]
        public async Task<ApiResult<dynamic>> Page([FromQuery] QueryUserInput input)
        {


            var data = await _sysUserInfoServices.GetUsersList(input);
            return Success(data);

        }
        [HttpGet]
        public async Task<ApiResult<List<UserOutput>>> All([FromQuery] QueryUserAllInput input)
        {


            var data = await _sysUserInfoServices.All(input);
            return Success(data);

        }
        [HttpGet]
        public async Task<ApiResult> Sync()
        {

            await _sysUserInfoServices.AdSync(false);
            return Success("同步成功");

        }


        [HttpGet]
        public async Task<ApiResult<ADUserProfileOutput>> FindInAd(string name)
        {


            var res = await _sysUserInfoServices.FindInAd(name);

            if (res == null || res.EmpId == null)
                return Failed<ADUserProfileOutput>("This account does not exist");
            if (res.Active == 0)
                return Failed<ADUserProfileOutput>("This account is inAvtice");
            return Success(res);

        }
        /// <summary>
        /// add new user
        /// </summary>
        /// <param name="input">user model</param>
        /// <returns>return user id</returns>
        // POST: api/User
        [HttpPost]

        public async Task<ApiResult<long>> Add(AddUserInput input) => Success((await _sysUserInfoServices.Add(input)));

        /// <summary>
        /// update user for user model
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<ApiResult<bool>> Update(UpdateUserInput input) => Success(await _sysUserInfoServices.Update(input));


        /// <summary>
        /// 用户有用角色
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet]

        public async Task<ApiResult<List<long>>> OwnRole(long userId) => Success(await _sysUserInfoServices.GetUserOwnRole(userId));


        /// <summary>
        /// 用户拥有数据
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<List<long>>> OwnData(long userId) => Success(await _sysUserInfoServices.GetUserOwnData(userId));
        ///// <summary>
        ///// delete user for user id
        ///// </summary>
        ///// <param name="id">user id</param>
        ///// <returns></returns>
        //[HttpDelete]
        //public async Task<ApiResult<bool>> Delete(long id) => Success(await _sysUserInfoServices.Delete(id));

    }
}
