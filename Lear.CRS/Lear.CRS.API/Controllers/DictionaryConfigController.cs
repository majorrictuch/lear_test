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
    
    public class DictionaryConfigController : BaseController
    {
        readonly IDictionaryConfigServices _configServices;
        private readonly IUser _user;
        private readonly ILogger<UserController> _logger;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="configServices"></param>
        /// <param name="user"></param>
        /// <param name="logger"></param>
        public DictionaryConfigController(IDictionaryConfigServices configServices,  IUser user, ILogger<UserController> logger)
        {
            _configServices = configServices;
            _user = user;
            _logger = logger;
        }

        /// <summary>
        /// 增加字典
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult> AddDictionaryConfig(AddDictionaryConfigInput input) => Success((await _configServices.AddDictionaryConfig(input)).ToString());

        /// <summary>
        /// 修改字典
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<ApiResult> UpdateDictionaryConfig(UpdateDictionaryConfigInput input) => Success((await _configServices.UpdateDictionaryConfig(input)).ToString());

        /// <summary>
        /// 获取字典
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<List<DictionaryConfigOutput>>> GetDictionaryConfig([FromQuery] DictionaryConfigInput input)
        {
            var data = await _configServices.GetDictionaryConfig(input);
            return Success(data);

        }

        /// <summary>
        /// 获取字典key-value
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<List<DictionaryConfigKeyValueOutput>>> GetDictionaryConfigKeyValue()
        {
            var data = await _configServices.GetDictionaryConfigKeyValue();
            return Success(data);

        }



        /// <summary>
        /// 获取周时间key-value
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<List<DictionaryConfigKeyValueOutput>>> GetWeekValue()
        {
            var data = await _configServices.GetWeekValue();
            return Success(data);

        }

        /// <summary>
        /// 获取月时间key-value
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<List<DictionaryConfigKeyValueOutput>>> GetPeriodValue()
        {
            var data = await _configServices.GetPeriodValue();
            return Success(data);

        }

    }
}
