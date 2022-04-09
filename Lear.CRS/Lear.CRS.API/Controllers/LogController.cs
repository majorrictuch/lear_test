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
using Microsoft.AspNetCore.Http;

namespace Lear.CRS.API.Controllers

{
    /// <summary>
    /// User module
    /// </summary>
    [Route("[controller]/[action]")]
    [ApiController]
    [Authorize]
    
    public class LogController : BaseController
    {
        readonly ILogServices _logServices;
        private readonly IUser _user;
        private readonly ILogger<LogController> _logger;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="logServices"></param>
        /// <param name="user"></param>
        /// <param name="logger"></param>
        public LogController(ILogServices logServices, IUser user, ILogger<LogController> logger)
        {
            _logServices = logServices;
            _user = user;
            _logger = logger;
        }

        /// <summary>
        /// 获取日志列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<dynamic>> All([FromQuery]LogInput input)
        {

            var list = await _logServices.QueryJobLogList( input);

            return Success(list);

        }

    }
}
