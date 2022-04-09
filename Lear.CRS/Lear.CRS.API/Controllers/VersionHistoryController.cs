using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Lear.CRS.Common;
using Lear.CRS.Common.HttpContextUser;
using Lear.CRS.IServices;
using Lear.CRS.Model;
using Lear.CRS.Model.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;

namespace Lear.CRS.API.Controllers

{
    /// <summary>
    /// 版本记录
    /// </summary>
    [Route("[controller]/[action]")]
    [ApiController]
    

    public class VersionHistoryController : BaseController
    {
        readonly IVersionHistoryServices _versionHistoryServices;
        readonly IUser _user;

        private readonly IWebHostEnvironment _webHostEnvironment;

        public VersionHistoryController(IVersionHistoryServices versionHistoryServices, IWebHostEnvironment webHostEnvironment)
        {
            _versionHistoryServices = versionHistoryServices;

            _webHostEnvironment = webHostEnvironment;
        }
        /// <summary>
        /// 获取所有记录
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>

        // GET: api/User
        [HttpGet]
        public async Task<ApiResult<List<VersionHistoryOutput>>> All()
        {


            var data = await _versionHistoryServices.All();


            return Success(data);

        }



        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        // POST: api/User
        [HttpPost]

        public async Task<ApiResult<long>> Add(AddVersionHistoryInput input) => Success((await _versionHistoryServices.Add(input)));


        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        // PUT: api/User/5
        [HttpPut]
        public async Task<ApiResult<bool>> Update([FromBody] UpdateVersionHistoryInput input) => Success((await _versionHistoryServices.Update(input)));

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>

        [HttpDelete]
        public async Task<ApiResult<bool>> Delete(DeleteVersionHistoryInput input) => Success(await _versionHistoryServices.Del(input));





    }
}
