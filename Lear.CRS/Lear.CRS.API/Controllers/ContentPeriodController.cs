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
    
    public class ContentPeriodController : BaseController
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
        public ContentPeriodController(IContentServices contentServices,  IUser user, ILogger<UserController> logger)
        {
            _contentServices = contentServices;
            _user = user;
            _logger = logger;
        }

        /// <summary>
        /// CRS_Content_Period
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        //[HttpPost]
        //public async Task<ApiResult> AddContentPeriod(List<ContentPeriodInput> input) => Success((await _contentServices.AddContentPeriod(input)).ToString());
        
        
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<ApiResult> UpdateContentPeriod(List<UpdateContentPeriodInput> input) => Success((await _contentServices.UpdateContentPeriod(input)).ToString());

        /// <summary>
        /// 获取
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<List<ContentPeriodOutput>>> GetContentPeriod([FromQuery] ContentPeriodInput input)
        {
            var data = await _contentServices.GetContentPeriod(input);
            return Success(data);

        }





    }
}
