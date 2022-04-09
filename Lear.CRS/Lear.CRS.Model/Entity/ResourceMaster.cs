using SqlSugar;

namespace Lear.CRS.Model
{
    /// <summary>
    /// 
    /// </summary>
    public class ResourceMaster
    {
        /// <summary>
        /// 
        /// </summary>
        public ResourceMaster()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        [SugarColumn(IsPrimaryKey = true)]
        public System.Int64 Id { get; set; }

        /// <summary>
        /// 部门名称
        /// </summary>
        public System.String ResourceName { get; set; }

        /// <summary>
        /// 父级ID
        /// </summary>
        public System.Int64? ParentId { get; set; }

        /// <summary>
        /// 负责人
        /// </summary>
        public System.Int64? ManagerId { get; set; }

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

        /// <summary>
        /// 
        /// </summary>
        public System.String ResourceDesc { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.Int32? Sort { get; set; }
    }
}