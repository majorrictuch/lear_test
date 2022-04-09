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

    public class FileController : BaseController
    {
        readonly IFileServices _fileServices;
        private readonly IUser _user;
        private readonly ILogger<UserController> _logger;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="fileServices"></param>
        /// <param name="user"></param>
        /// <param name="logger"></param>
        public FileController(IFileServices fileServices, IUser user, ILogger<UserController> logger)
        {
            _fileServices = fileServices;
            _user = user;
            _logger = logger;
        }

        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpDelete]

        public async Task<ApiResult<bool>> DeleteFileInfo(DeleteFileInfoInput input) => Success(await _fileServices.DeleteFileInfo(input));
        /// <summary>
        /// 下载文件
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]

        public async Task<IActionResult> DownloadFileInfo([FromQuery] QueryFileInoInput input) => await _fileServices.DownloadFileInfo(input);


        /// <summary>
        /// 获取文件
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet]

        public async Task<ApiResult<FileMaster>> GetFileInfo(QueryFileInoInput input) => Success(await _fileServices.GetFileInfo(input));



        /// <summary>
        /// 获取文件列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet]

        public async Task<ApiResult<List<FileMaster>>> GetFileInfoList(QueryFileInoInput input) => Success(await _fileServices.GetFileInfoList(input));


        /// <summary>
        /// 预览文件
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet]

        public async Task<ApiResult<IActionResult>> PreviewFileInfo(QueryFileInoInput input) => Success(await _fileServices.PreviewFileInfo(input));


        /// <summary>
        /// 分页获取文件列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet]

        public async Task<ApiResult<dynamic>> QueryFileInfoPageList([FromQuery] QueryFileInoInput input) => Success(await _fileServices.QueryFileInfoPageList(input));





        /// <summary>
        /// 上传word
        /// </summary>
        /// <param name="files"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public async Task<ApiResult<long>> UploadFileDocument(IFormFile files) => Success(await _fileServices.UploadFileDocument(files));


        /// <summary>
        /// 修改上传文件的上传人
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]

        public async Task<ApiResult<bool>> UpdateFileDocument([FromBody] UpdateFileInoInput input) => Success(await _fileServices.UpdateFileDocument(input));























    }
}
