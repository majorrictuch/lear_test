using System.Collections.Generic;
using System.Threading.Tasks;
using Lear.CRS.Common;
using Lear.CRS.Common.HttpContextUser;
using Lear.CRS.IServices;
using Lear.CRS.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Lear.CRS.API.Controllers

{
    /// <summary>
    /// 资源管理
    /// </summary>
    [Route("[controller]/[action]")]
    [ApiController]
    
    public class ResourceController : BaseController
    {
        readonly IReSourceServices _resourceServices;


        public ResourceController(IReSourceServices resourceServices)
        {
            _resourceServices = resourceServices;
        }


        // GET: api/User
        [HttpGet]
        public async Task<ApiResult<dynamic>> Page([FromQuery] QueryResourceInput query) => Success(await _resourceServices.Page(query));
        [HttpGet]
        public async Task<ApiResult<List<ResourceOutput>>> All() => Success(await _resourceServices.All());

        [HttpPost]
        public async Task<ApiResult<long>> Add([FromBody] AddResourceInput input) => Success(await _resourceServices.Add(input));

        [HttpPut]
        public async Task<ApiResult<bool>> Update([FromBody] UpdateResourceInput input) => Success(await _resourceServices.Update(input));

        [HttpDelete]
        public async Task<ApiResult<bool>> Delete(long id) => Success(await _resourceServices.Del(id));





    }
}
