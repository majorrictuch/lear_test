using SqlSugar;

namespace Lear.CRS.Model
{
    /// <summary>
    /// 
    /// </summary>
    public class UserRole
    {
        /// <summary>
        /// 
        /// </summary>
        public UserRole()
        {
        }

        /// <summary>
        /// 主键
        /// </summary>
        [SugarColumn(IsPrimaryKey = true)]
        public System.Int64 Id { get; set; }

        /// <summary>
        /// 用户ID
        /// </summary>
        public System.Int64 UserId { get; set; }

        /// <summary>
        /// 角色ID
        /// </summary>
        public System.Int64 RoleId { get; set; }

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