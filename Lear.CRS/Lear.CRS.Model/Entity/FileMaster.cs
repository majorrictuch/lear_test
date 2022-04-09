using SqlSugar;

namespace Lear.CRS.Model
{
    /// <summary>
    /// 
    /// </summary>
    public class FileMaster
    {
        /// <summary>
        /// 
        /// </summary>
        public FileMaster()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        [SugarColumn(IsPrimaryKey = true)]
        public System.Int64 Id { get; set; }

        /// <summary>
        /// 原文件名
        /// </summary>
        public System.String FileOriginName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String FileBucket { get; set; }

        /// <summary>
        /// 文件类型
        /// </summary>
        public System.String FileSuffix { get; set; }

        /// <summary>
        /// 文件大小
        /// </summary>
        public System.String FileSizeKb { get; set; }

        /// <summary>
        /// 文件说明
        /// </summary>
        public System.String FileSizeInfo { get; set; }

        /// <summary>
        /// 文件名
        /// </summary>
        public System.String FileObjectName { get; set; }

        /// <summary>
        /// 路径
        /// </summary>
        public System.String FilePath { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String CreateBy { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.DateTime? CreateTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String UpdateBy { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.DateTime? UpdateTime { get; set; }


        public string Remark { get; set; }
    }
}