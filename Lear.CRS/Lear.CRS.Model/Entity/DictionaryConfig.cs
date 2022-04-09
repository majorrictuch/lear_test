using SqlSugar;

namespace Lear.CRS.Model
{
    /// <summary>
    /// 
    /// </summary>
    public class DictionaryConfig
    {
        /// <summary>
        /// 
        /// </summary>
        public DictionaryConfig()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        [SugarColumn(IsPrimaryKey = true)]
        public System.Int64 Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String Type { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String TypeName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String Code { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String Value { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String Description { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.Boolean? Enabled { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.Int32? Sort { get; set; }

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
    }
}