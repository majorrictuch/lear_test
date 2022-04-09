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
    
    public class ContentWeekViewController : BaseController
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
        public ContentWeekViewController(IContentServices contentServices,  IUser user, ILogger<UserController> logger)
        {
            _contentServices = contentServices;
            _user = user;
            _logger = logger;
        }

        /// <summary>
        /// CRS_Content_Week_View
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult> AddContentWeekView(List<ContentWeekViewInput> input) => Success((await _contentServices.AddContentWeekView(input)).ToString());

        [HttpPut]
        public async Task<ApiResult> UpdateContentWeekView(List<ContentWeekViewInput> input) => Success((await _contentServices.UpdateContentWeekView(input)).ToString());

        [HttpGet]
        public async Task<ApiResult<List<ContentWeekViewOutput>>> GetContentWeekView([FromQuery] ContentWeekViewInput input)
        {
            var data = await _contentServices.GetContentWeekView(input);
            return Success(data);
        }



    }
}
