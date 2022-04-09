
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
        private readonly ISqlSugarRepository<FileMaster> _sysFileInfoRep;  // �ļ���Ϣ��ִ� 

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
        /// ��ҳ��ȡ�ļ��б�
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
        /// ��ȡ�ļ��б�
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<List<FileMaster>> GetFileInfoList(QueryFileInoInput input)
        {
            return await _sysFileInfoRep.ToListAsync();
        }

        /// <summary>
        /// ɾ���ļ�
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
        /// ��ȡ�ļ�����
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
        /// Ԥ���ļ�
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<IActionResult> PreviewFileInfo(QueryFileInoInput input)
        {
            return await DownloadFileInfo(input);
        }

        /// <summary>
        /// �ϴ��ļ�
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        [HttpPost("/sysFileInfo/upload")]
        public async Task<bool> UploadFileDefault(IFormFile file)
        {
            // ���Զ�ȡϵͳ�������������ļ��洢��ʲô�ط�
            await UploadFile(file, _configuration["UploadFile:Default:path"], FileLocation.LOCAL);
            return true;
        }

        /// <summary>
        /// �����ļ�
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
        /// �ϴ��ĵ�
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
        /// �޸��ϴ�ʱ�����
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
        /// �ϴ����ı�ͼƬ
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
        /// �ϴ��ļ�
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

            var fileSizeKb = (long)(file.Length / 1024.0); // �ļ���СKB
            var originalFilename = file.FileName; // �ļ�ԭʼ����
            var fileSuffix = Path.GetExtension(file.FileName).ToLower(); // �ļ���׺  

            if (await _sysFileInfoRep.AnyAsync(c => c.FileOriginName == originalFilename))
            {
                throw new BusinessException("The file is exists!");
            }
            

            // �ȴ���ȡId
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
            return newFile.Id; // �����ļ�Ψһ��ʶ
        }

        /// <summary>
        /// �ϴ��ļ�
        /// </summary>
        /// <param name="file">�ļ�</param>
        /// <param name="pathType">�洢·��</param>
        /// <param name="fileLocation">�ļ��洢λ��</param>
        /// <returns></returns>
        private  async Task<long> UploadFile(IFormFile file, string pathType, FileLocation fileLocation)
        {
            var fileSizeKb = (long)(file.Length / 1024.0); // �ļ���СKB
            var originalFilename = file.FileName; // �ļ�ԭʼ����
            var fileSuffix = Path.GetExtension(file.FileName).ToLower(); // �ļ���׺
            var wwwrootPath = _webHostEnvironment.WebRootPath;
            // �ȴ���ȡId
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

            var finalName = newFile.FileObjectName; // �����ļ�����������   
            if (fileLocation == FileLocation.LOCAL) // ���ش洢
            {
                var filePath = Path.Combine(wwwrootPath, pathType);
                if (!Directory.Exists(filePath))
                    Directory.CreateDirectory(filePath);
                using (var stream = File.Create(Path.Combine(filePath, finalName)))
                {
                    await file.CopyToAsync(stream);
                }

                newFile.FileObjectName = finalName;
                return newFile.Id; // �����ļ�Ψһ��ʶ
            }
            return -1;
        }
    }
}
