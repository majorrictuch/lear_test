using Lear.CRS.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lear.CRS.IServices
{	
	/// <summary>
	/// RoleServices 
	/// </summary>	
    public interface IFileServices
    {
        Task<bool> DeleteFileInfo(DeleteFileInfoInput input);
        Task<IActionResult> DownloadFileInfo(QueryFileInoInput input);
        Task<FileMaster> GetFileInfo(QueryFileInoInput input);
        Task<List<FileMaster>> GetFileInfoList(QueryFileInoInput input);
        Task<IActionResult> PreviewFileInfo(QueryFileInoInput input);
        Task<dynamic> QueryFileInfoPageList(QueryFileInoInput input);
        Task<string> UploadFileEditor(IFormFile file);
        Task<bool> UploadFileDefault(IFormFile file);
        Task<long> UploadFileDocument(IFormFile file);
        Task<bool> UpdateFileDocument(UpdateFileInoInput input);


    }
}
