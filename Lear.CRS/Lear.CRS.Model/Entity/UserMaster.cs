using SqlSugar;

namespace Lear.CRS.Model
{
    /// <summary>
    /// 
    /// </summary>
    public class UserMaster
    {
        /// <summary>
        /// 
        /// </summary>
        public UserMaster()
        {
        }

        /// <summary>
        /// 主键
        /// </summary>
        [SugarColumn(IsPrimaryKey = true)]
        public System.Int64 Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String EmpId { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public System.String Account { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public System.String Password { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String FirstName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String LastName { get; set; }

        /// <summary>
        /// 邮箱地址
        /// </summary>
        public System.String Email { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String FullName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String ADGuid { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.Int32 Active { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.DateTime? LastLoginTime { get; set; }

        /// <summary>
        /// 所属部门
        /// </summary>
        public System.String DepartmentId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.DateTime? LastSyncTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String Location { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String OUCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String CreateBy { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.DateTime CreateTime { get; set; }

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