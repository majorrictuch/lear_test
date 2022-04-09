using SqlSugar;

namespace Lear.CRS.Model
{
    /// <summary>
    /// 
    /// </summary>
    public class RoleMenu
    {
        /// <summary>
        /// 
        /// </summary>
        public RoleMenu()
        {
        }

        /// <summary>
        /// 主键
        /// </summary>
        [SugarColumn(IsPrimaryKey = true)]
        public System.Int64 Id { get; set; }

        /// <summary>
        /// 权限ID
        /// </summary>
        public System.Int64 MenuId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.Int64 RoleId { get; set; }

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