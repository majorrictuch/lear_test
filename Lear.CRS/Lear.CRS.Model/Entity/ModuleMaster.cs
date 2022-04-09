using SqlSugar;

namespace Lear.CRS.Model
{
    /// <summary>
    /// 
    /// </summary>
    public class ModuleMaster
    {
        /// <summary>
        /// 
        /// </summary>
        public ModuleMaster()
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
        public System.String Name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String Action { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.Int32 Sort { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String Description { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.Int64? ParentId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String CreateBy { get; set; }

        /// <summary>
        /// 创建时间
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