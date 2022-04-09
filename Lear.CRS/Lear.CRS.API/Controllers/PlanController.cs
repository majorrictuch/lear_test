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
    
    public class PlanController : BaseController
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
        public PlanController(IContentServices contentServices,  IUser user, ILogger<UserController> logger)
        {
            _contentServices = contentServices;
            _user = user;
            _logger = logger;
        }



        /// <summary>
        /// 新增计划
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult> AddPlan(List<AddPlanInput> input) => Success((await _contentServices.AddPlan(input)).ToString());

        /// <summary>
        /// 修改计划
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<ApiResult> UpdatePlan(List<UpdatePlanInput> input) => Success((await _contentServices.UpdatePlan(input)).ToString());

        /// <summary>
        /// 获取计划
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<List<PlanOutput>>> GetPlan([FromQuery] PlanInput input)
        {
            var data = await _contentServices.GetPlan(input);
            return Success(data);

        }




    }
}
