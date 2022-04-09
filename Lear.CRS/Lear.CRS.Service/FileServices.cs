
using Lear.CRS.Common.CustomExceptions;
using Lear.CRS.Common.HttpContextUser;
using Lear.CRS.IServices;
using Lear.CRS.Model;
using Lear.CRS.SqlSugarCore.Extensions;
using Mapster;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Yitter.IdGenerator;

namespace Lear.CRS.Service
{
    /// <summary>
    /// RoleServices
    /// </summary>	
    public class FileServices : IFileServices
    {
        private readonly ISqlSugarRepository<FileMaster> _sysFileInfoRep;  // 文件信息表仓储 

        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IUser _user;
        public FileServices(ISqlSugarRepository<FileMaster> sysFileInfoRep, IConfiguration configuration, IWebHostEnvironment webHostEnvironment, IUser user)
        {
            _sysFileInfoRep = sysFileInfoRep;
            _configuration = configuration;
            _webHostEnvironment = webHostEnvironment;
            this._user = user;
        }

        /// <summary>
        /// 分页获取文件列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<dynamic> QueryFileInfoPageList(QueryFileInoInput input)
        {
            var fileOriginName = !string.IsNullOrEmpty(input.FileOriginName?.Trim());

            var files = await _sysFileInfoRep.Context.Queryable<FileMaster>()
                .WhereIF(!string.IsNullOrWhiteSpace(input.FileOriginName), u => u.FileOriginName.Contains(input.FileOriginName.Trim()))
                .Select<FileOutput>()
                .ToPagedListAsync(input.PageNo, input.PageSize);
            return files.XnPagedResult();
        }

        /// <summary>
        /// 获取文件列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<List<FileMaster>> GetFileInfoList(QueryFileInoInput input)
        {
            return await _sysFileInfoRep.ToListAsync();
        }

        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<bool> DeleteFileInfo(DeleteFileInfoInput input)
        {
            var wwwrootPath = _webHostEnvironment.WebRootPath;

            var file = await _sysFileInfoRep.FirstOrDefaultAsync(u => u.Id == input.Id);
            if (file != null)
            {
                await _sysFileInfoRep.DeleteAsync(file);

                var filePath = Path.Combine(wwwrootPath, file.FileBucket, file.FileObjectName);
                if (File.Exists(filePath))
                    File.Delete(filePath);
                return true;
            }
            return false;

        }

        /// <summary>
        /// 获取文件详情
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<FileMaster> GetFileInfo(QueryFileInoInput input)
        {
            var file = await _sysFileInfoRep.FirstOrDefaultAsync(u => u.Id == input.Id);
            if (file == null)
                throw new BusinessException("This files is not found!");
            return file;
        }

        /// <summary>
        /// 预览文件
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<IActionResult> PreviewFileInfo(QueryFileInoInput input)
        {
            return await DownloadFileInfo(input);
        }

        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        [HttpPost("/sysFileInfo/upload")]
        public async Task<bool> UploadFileDefault(IFormFile file)
        {
            // 可以读取系统配置来决定将文件存储到什么地方
            await UploadFile(file, _configuration["UploadFile:Default:path"], FileLocation.LOCAL);
            return true;
        }

        /// <summary>
        /// 下载文件
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
       
        public async Task<IActionResult> DownloadFileInfo(QueryFileInoInput input)
        {
            var wwwrootPath = _webHostEnvironment.WebRootPath;
            var file = await GetFileInfo(input);
            var filePath = Path.Combine(wwwrootPath, file.FilePath, file.FileObjectName);

            if (!File.Exists(filePath))
                throw new BusinessException("file does not exist");
            var fileName = HttpUtility.UrlEncode(file.FileOriginName, Encoding.GetEncoding("UTF-8"));
            var result = new FileStreamResult(new FileStream(filePath, FileMode.Open), "application/octet-stream") { FileDownloadName = fileName };
            return result;
        }

       

        /// <summary>
        /// 上传文档
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        [HttpPost("/sysFileInfo/uploadDocument")]
        public async Task<long> UploadFileDocument(IFormFile file)
        {
            var id = await UploadFile(file, _configuration["UploadFile:Document:path"]);
            return id;
        }
        /// <summary>
        /// 修改上传时间跟人
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public async Task<bool> UpdateFileDocument(UpdateFileInoInput input)
        {
            var model = await _sysFileInfoRep.FirstOrDefaultAsync(u => u.Id == input.Id);
            if (model != null) {
                model.CreateBy = _user.Name;
                model.CreateTime = DateTime.Now;
                model.Remark = input.Remark;
                _sysFileInfoRep.Update(model);
                return true;
            }
            return false;
        }

        /// <summary>
        /// 上传富文本图片
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        [HttpPost("/sysFileInfo/uploadEditor")]
        public async Task<string> UploadFileEditor(IFormFile file)
        {
            var id = await UploadFile(file, _configuration["UploadFile:Editor:path"], FileLocation.LOCAL);
            var fileInfo = _sysFileInfoRep.Single(id);
            return Path.Combine(fileInfo.FilePath, fileInfo.FileObjectName);

        }
     
        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="file"></param>
        /// <param name="pathType"></param>
        /// <returns></returns>
        private  async Task<long> UploadFile(IFormFile file, string pathType)
        {
            var wwwrootPath = _webHostEnvironment.WebRootPath;

            var filePath = Path.Combine(wwwrootPath, pathType);
            if (!Directory.Exists(filePath))
                Directory.CreateDirectory(filePath);

            var fileSizeKb = (long)(file.Length / 1024.0); // 文件大小KB
            var originalFilename = file.FileName; // 文件原始名称
            var fileSuffix = Path.GetExtension(file.FileName).ToLower(); // 文件后缀  

            if (await _sysFileInfoRep.AnyAsync(c => c.FileOriginName == originalFilename))
            {
                throw new BusinessException("The file is exists!");
            }
            

            // 先存库获取Id
            var id = YitIdHelper.NextId();
            var newFile = new FileMaster
            {
                Id = id,
                FileBucket = FileLocation.LOCAL.ToString(),
                FileObjectName = $"{id}{fileSuffix}",
                FileOriginName = originalFilename,
                FileSuffix = fileSuffix.TrimStart('.'),
                FileSizeKb = fileSizeKb.ToString(),
                FilePath = pathType,
                CreateBy = this._user.Name,
                CreateTime = DateTime.Now
            };
            await _sysFileInfoRep.InsertAsync(newFile);

            using (var stream = File.Create(Path.Combine(filePath, newFile.FileObjectName)))
            {
                await file.CopyToAsync(stream);
            }
            return newFile.Id; // 返回文件唯一标识
        }

        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="file">文件</param>
        /// <param name="pathType">存储路径</param>
        /// <param name="fileLocation">文件存储位置</param>
        /// <returns></returns>
        private  async Task<long> UploadFile(IFormFile file, string pathType, FileLocation fileLocation)
        {
            var fileSizeKb = (long)(file.Length / 1024.0); // 文件大小KB
            var originalFilename = file.FileName; // 文件原始名称
            var fileSuffix = Path.GetExtension(file.FileName).ToLower(); // 文件后缀
            var wwwrootPath = _webHostEnvironment.WebRootPath;
            // 先存库获取Id
            var id = YitIdHelper.NextId();
            var newFile = new FileMaster
            {
                Id = id,
                FileBucket = FileLocation.LOCAL.ToString(),
                FileObjectName = $"{YitIdHelper.NextId()}{fileSuffix}",
                FileOriginName = originalFilename,
                FileSuffix = fileSuffix.TrimStart('.'),
                FileSizeKb = fileSizeKb.ToString(),
                FilePath = pathType,
                CreateBy = this._user.Name,
                CreateTime = DateTime.Now
            };
            await _sysFileInfoRep.InsertAsync(newFile);

            var finalName = newFile.FileObjectName; // 生成文件的最终名称   
            if (fileLocation == FileLocation.LOCAL) // 本地存储
            {
                var filePath = Path.Combine(wwwrootPath, pathType);
                if (!Directory.Exists(filePath))
                    Directory.CreateDirectory(filePath);
                using (var stream = File.Create(Path.Combine(filePath, finalName)))
                {
                    await file.CopyToAsync(stream);
                }

                newFile.FileObjectName = finalName;
                return newFile.Id; // 返回文件唯一标识
            }
            return -1;
        }
    }
}
