using SqlSugar;

namespace Lear.CRS.Model
{
    /// <summary>
    /// 
    /// </summary>
    public class LoginLog
    {
        /// <summary>
        /// 
        /// </summary>
        public LoginLog()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        [SugarColumn(IsPrimaryKey = true)]
        public System.Int64 Id { get; set; }

        /// <summary>
        /// 用户ID
        /// </summary>
        public System.Int64 UserId { get; set; }

        /// <summary>
        /// 访问IP
        /// </summary>
        public System.String IP { get; set; }

        /// <summary>
        /// 访问时间
        /// </summary>
        public System.DateTime CreateTime { get; set; }
    }
}