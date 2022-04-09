namespace Lear.CRS.Model
{
    /// <summary>
    /// 系统应用参数
    /// </summary>
    public class DictionaryConfigOutput
    {
        /// <summary>
        /// id
        /// </summary>
        public System.Int64 Id { get; set; }

        /// <summary>
        /// 类别
        /// </summary>
        public System.String Type { get; set; }

        /// <summary>
        /// 类别名称
        /// </summary>
        public System.String TypeName { get; set; }


        /// <summary>
        /// code
        /// </summary>
        public System.String Code { get; set; }

        /// <summary>
        /// value
        /// </summary>
        public System.String Value { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public System.String Description { get; set; }

        /// <summary>
        /// 是否有效
        /// </summary>
        public System.Boolean? Enabled { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public System.Int32? Sort { get; set; }

        /// <summary>
        /// 创建者
        /// </summary>
        public System.String CreateBy { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public System.DateTime? CreateTime { get; set; }

        /// <summary>
        /// 更新者
        /// </summary>
        public System.String UpdateBy { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        public System.DateTime? UpdateTime { get; set; }
    }

    /// <summary>
    /// type-Description
    /// </summary>
    public class DictionaryConfigKeyValueOutput
    {

        /// <summary>
        /// 类别
        /// </summary>
        public System.String Type { get; set; }

        /// <summary>
        /// 类别名称
        /// </summary>
        public System.String TypeName { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public System.Int32? FromDate { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public System.Int32? ToDate { get; set; }




    }










}
