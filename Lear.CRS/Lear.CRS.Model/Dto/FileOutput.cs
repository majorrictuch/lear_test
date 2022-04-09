using System;

namespace Lear.CRS.Model

{
    /// <summary>
    /// 上传文件参数
    /// </summary>
    public class FileOutput : FileInput
    {
        /// <summary>
        /// 文件Id
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string CreateBy { get; set; }

        /// <summary>
        /// CreateTime
        /// </summary>
        public DateTime CreateTime { get; set; }
    }
}
