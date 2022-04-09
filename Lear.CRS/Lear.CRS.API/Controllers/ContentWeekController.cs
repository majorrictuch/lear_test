using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lear.CRS.AuthHelper.OverWrite;
using Lear.CRS.Common.Helper;
using Lear.CRS.Common.HttpContextUser;
using Lear.CRS.IServices;
using Lear.CRS.Model;
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
    
    public class ContentWeekController : BaseController
    {
        readonly IContentServices _contentServices;
        private readonly IUser _user;
        private readonly ILogger<UserController> _logger;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="contentServices"></param>
        /// <param name="user"></param>
        /// <param name="logger"></param>
        public ContentWeekController(IContentServices contentServices,  IUser user, ILogger<UserController> logger)
        {
            _contentServices = contentServices;
            _user = user;
            _logger = logger;
        }

        


        /// <summary>
        /// 周报
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        //[HttpPost]
        //public async Task<ApiResult> AddContentWeek(List<ContentWeekInput> input) => Success((await _contentServices.AddContentWeek(input)).ToString());

        /// <summary>
        /// 新增周报
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<ApiResult> UpdateContentWeek(List<UpdateContentWeekInput> input) => Success((await _contentServices.UpdateContentWeek(input)).ToString());

        /// <summary>
        /// 获取周报
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<List<ContentWeekOutput>>> GetContentWeek([FromQuery] ContentWeekInput input)
        {
            var data = await _contentServices.GetContentWeek(input);
            return Success(data);

        }




    }
}
