using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Lear.CRS.Common;
using Lear.CRS.Common.HttpContextUser;
using Lear.CRS.IServices;
using Lear.CRS.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;

namespace Lear.CRS.API.Controllers

{
    /// <summary>
    /// source group 
    /// </summary>
    [Route("[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class SourceGroupController : BaseController
    {
        readonly ISourceGroupServices _sourceGroupServices;

        private readonly IWebHostEnvironment _webHostEnvironment;

        public SourceGroupController(ISourceGroupServices sourceGroupServices, IWebHostEnvironment webHostEnvironment)
        {
            _sourceGroupServices = sourceGroupServices;
            _webHostEnvironment = webHostEnvironment;
        }


        // GET: api/User
        [HttpGet]
        public async Task<ApiResult<dynamic>> Page([FromQuery] QuerySourceGroupInput query) => Success(await _sourceGroupServices.Page(query));


        [HttpGet]
        public async Task<ApiResult<List<SourceGroupOutput>>> All([FromQuery] QuerySourceGroupInput query) => Success(await _sourceGroupServices.All(query));

        [HttpPost]
        public async Task<ApiResult<long>> Add([FromBody] AddSourceGroupInput input) => Success(await _sourceGroupServices.Add(input));

        [HttpPut]
        public async Task<ApiResult<bool>> Update([FromBody] UpdateSourceGroupInput input) => Success(await _sourceGroupServices.Update(input));

        [HttpDelete]
        public async Task<ApiResult<bool>> Delete(long id) => Success(await _sourceGroupServices.Del(id));



        [HttpGet]
        public async Task<IActionResult> ExportSource()
        {
            var wwwrootPath = _webHostEnvironment.WebRootPath;
            var pathType = "tmp";
            var filePath = Path.Combine(wwwrootPath, pathType);
            if (!Directory.Exists(filePath))
                Directory.CreateDirectory(filePath);

            var data = await _sourceGroupServices.ExportData();

            string sFileName = $@"DataAccessGroupMain{DateTime.Now.ToString("yyyyMMddHHmmss")}.xlsx";
            var path = Path.Combine(filePath, sFileName);
            FileInfo file = new FileInfo(path);

            using (ExcelPackage package = new ExcelPackage(file))
            {

                ExcelHelper.ReportData(package, "Data Access Group Main", data.Column, data.Data);
                package.Save(); //Save the workbook.
            }


            var result = new FileStreamResult(new FileStream(path, FileMode.Open), "application/octet-stream") { FileDownloadName = sFileName };
            return result;
        }


        ///// <summary>
        ///// 组绑定用户
        ///// </summary>
        ///// <param name="input"></param>
        ///// <returns></returns>
        //[HttpPost]
        //public async Task<ApiResult<bool>> BindToUser([FromBody] GroupToUserInput input) => Success(await _sourceGroupServices.BindToUser(input));





        ///// <summary>
        ///// 用户绑定组
        ///// </summary>
        ///// <param name="input"></param>
        ///// <returns></returns>
        //[HttpPost]
        //public async Task<ApiResult<bool>> BindToGroup([FromBody] UserToGroupInput input) => Success(await _sourceGroupServices.BindToGroup(input));


    }
}
