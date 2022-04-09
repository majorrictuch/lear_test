using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Lear.CRS.Common.HttpContextUser;
using Lear.CRS.IServices;
using Lear.CRS.Model;
using Lear.CRS.Model.Permission;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Lear.CRS.API.Controllers
{
    /// <summary>
    /// 接口管理
    /// </summary>
    [Route("[controller]/[action]")]
    [ApiController]
    [Authorize]

    public class ModuleController : BaseController
    {
        readonly IModuleServices _moduleServices;


        public ModuleController(IModuleServices moduleServices)
        {
            _moduleServices = moduleServices;
        }

        /// <summary>
        /// 获取全部接口api
        /// </summary>
        /// <param name="page"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        // GET: api/User
        [HttpGet]
        public async Task<ApiResult<List<ModuleOutput>>> List([FromQuery] QueryModuleInput query)
        {


            var data = await _moduleServices.GetMoudulePageList(query);

            return Success<List<ModuleOutput>>(data);

        }
        /// <summary>
        /// 根据父级id获取接口列表
        /// </summary>
        /// <param name="page"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        // GET: api/User
        [HttpGet]
        public async Task<ApiResult<List<ModuleOutput>>> ListByParentId(long parentId) => Success(await _moduleServices.GetByParentId(parentId));



        /// <summary>
        /// 添加一条接口信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<long>> Add([FromBody] AddModuleInput input)
        {

            var id = (await _moduleServices.Add(input));

            return Success(id);
        }

        /// <summary>
        /// 更新接口信息
        /// </summary>
        /// <param name="module"></param>
        /// <returns></returns>
        // PUT: api/User/5
        [HttpPut]
        public async Task<ApiResult<bool>> Update(UpdateModuleInput input)
        {
            return Success(await _moduleServices.Update(input));
        }

        /// <summary>
        /// 删除一条接口
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // DELETE: api/ApiWithActions/5
        [HttpDelete]
        public async Task<ApiResult<bool>> Delete(long id)
        {
            return Success(await _moduleServices.DelById(id));
        }
    }
}
