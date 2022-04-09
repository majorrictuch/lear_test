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
    /// 角色管理
    /// </summary>
    [Route("[controller]/[action]")]
    [ApiController]
    [Authorize]
    [AllowAnonymous]

    public class RoleController : BaseController
    {
        readonly IRoleServices _roleServices;
        readonly IUser _user;

        private readonly IWebHostEnvironment _webHostEnvironment;

        public RoleController(IRoleServices roleServices, IWebHostEnvironment webHostEnvironment)
        {
            _roleServices = roleServices;

            _webHostEnvironment = webHostEnvironment;
        }
        /// <summary>
        /// 获取全部角色
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>

        // GET: api/User
        [HttpGet]
        public async Task<ApiResult<List<RoleOutput>>> All([FromQuery] QueryRoleInput query)
        {


            var data = await _roleServices.GetRoleList(query);


            return Success(data);

        }
        [HttpGet]
        public async Task<ApiResult<dynamic>> Page([FromQuery] QueryRoleInput query) => Success(await _roleServices.Page(query));


        /// <summary>
        /// 添加角色
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        // POST: api/User
        [HttpPost]

        public async Task<ApiResult<long>> Add(AddRoleInput role) => Success((await _roleServices.Add(role)));


        /// <summary>
        /// 更新角色
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        // PUT: api/User/5
        [HttpPut]
        public async Task<ApiResult<bool>> Update([FromBody] UpdateRoleInput role) => Success((await _roleServices.Update(role)));

        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>

        [HttpDelete]
        public async Task<ApiResult<bool>> Delete(long roleId) => Success(await _roleServices.Del(roleId));



        [HttpGet]
        public async Task<IActionResult> ExportAssign()
        {
            var wwwrootPath = _webHostEnvironment.WebRootPath;
            var pathType = "tmp";
            var filePath = Path.Combine(wwwrootPath, pathType);
            if (!Directory.Exists(filePath))
                Directory.CreateDirectory(filePath);

            var data = await _roleServices.ExportData();

            string sFileName = $@"UserRoleMain{DateTime.Now.ToString("yyyyMMddHHmmss")}.xlsx";
            var path = Path.Combine(wwwrootPath, sFileName);
            FileInfo file = new FileInfo(path);

            using (ExcelPackage package = new ExcelPackage(file))
            {

                ExcelHelper.ReportData(package, "User Role Main", data.Column, data.Data);
                package.Save(); //Save the workbook.
            }


            var result = new FileStreamResult(new FileStream(path, FileMode.Open), "application/octet-stream") { FileDownloadName = sFileName };
            return result;
        }


    }
}
