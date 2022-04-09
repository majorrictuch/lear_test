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
    
    public class ReportController : BaseController
    {
        readonly IContentServices _contentServices;
        readonly IReprotServices _reportServices;
        private readonly IUser _user;
        private readonly ILogger<UserController> _logger;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="reportServices"></param>
        /// <param name="contentServices"></param>
        /// <param name="user"></param>
        /// <param name="logger"></param>
        public ReportController(IReprotServices reportServices, IContentServices contentServices,IUser user, ILogger<UserController> logger)
        {
            _reportServices = reportServices;
            _contentServices = contentServices;
            _user = user;
            _logger = logger;
        }

       
        [HttpGet]
        public ApiResult<List<PeriodReportOutput>> PeriodReport([FromQuery] PeriodReportInput input)
        {
            var data =  _reportServices.PeriodReport(input);
            return Success(data);
        }

        [HttpGet]
        public ApiResult<List<WeeklyReportOutput>> WeeklyReport([FromQuery] WeeklyReportInput input)
        {
            var data = _reportServices.WeeklyReport(input);
            return Success(data);
        }


        [HttpGet]
        public async Task<ApiResult<List<DailyReportOutput>>> GetDailyReport([FromQuery] DailyReportInput input)
        {
            var data = await _contentServices.GetDailyReport(input);
            return Success(data);
        }

    }
}
