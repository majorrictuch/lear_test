using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Lear.CRS.Model

{
    /// <summary>
    /// 上传文件参数
    /// </summary>
    public class FileInput : PageInputBase
    {


        /// <summary>
        /// 文件仓库
        /// </summary>
        public string FileBucket { get; set; }

        /// <summary>
        /// 文件名称（上传时候的文件名）
        /// </summary>
        public string FileOriginName { get; set; }

        /// <summary>
        /// 文件后缀
        /// </summary>
        public string FileSuffix { get; set; }

        /// <summary>
        /// 文件大小kb
        /// </summary>
        public long FileSizeKb { get; set; }

        /// <summary>
        /// 文件大小信息，计算后的
        /// </summary>
        public string FileSizeInfo { get; set; }

        /// <summary>
        /// 存储到bucket的名称（文件唯一标识id）
        /// </summary>
        public string FileObjectName { get; set; }

        /// <summary>
        /// 存储路径
        /// </summary>
        public string FilePath { get; set; }

        public string Remark { get; set; }
    }

    public class DeleteFileInfoInput
    {
        /// <summary>
        /// 文件Id
        /// </summary>
        [Required(ErrorMessage = "文件Id不能为空")]
        [JsonIgnore]
        public long Id { get; set; }
    }

    public class QueryFileInoInput : PageInputBase
    {
        [JsonIgnore]
        public long Id { get; set; }

        /// <summary>
        /// fileName
        /// </summary>
        /// 
        [JsonIgnore]
        public string FileOriginName { get; set; }
    }

    public class UpdateFileInoInput
    {
        public long Id { get; set; }

        public string Remark { get; set; }
    }

}
