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
    public class ContentController : BaseController
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
        public ContentController(IContentServices contentServices,  IUser user, ILogger<UserController> logger)
        {
            _contentServices = contentServices;
            _user = user;
            _logger = logger;
        }

        /// <summary>
        /// 日报新增
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        //[HttpPost]
        //public async Task<ApiResult> AddContent(List<ContentInput> input) => Success((await _contentServices.AddContent(input)).ToString());

        /// <summary>
        /// 日报保存
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<ApiResult> UpdateContent(List<UpdateContentInput> input) => Success((await _contentServices.UpdateContent(input)).ToString());

        /// <summary>
        /// 日报查询
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<List<ContentOutput>>> GetContent([FromQuery] ContentInput input)
        {
            var data = await _contentServices.GetContent(input);
            return Success(data);

        }






    }
}
