using System;

namespace Lear.CRS.Model
{
    /// <summary>
    /// 用户参数
    /// </summary>
    public class UserOutput
    {
        /// <summary>
        /// Id
        /// </summary>
        public virtual long Id { get; set; }

        /// <summary>
        /// 账号
        /// </summary>
        public virtual string Account { get; set; }



        public String FirstName { get; set; }


        /// <summary>
        /// FirstName
        /// </summary>

        public String EmpId { get; set; }

        /// <summary>
        /// LastName
        /// </summary>

        public String LastName { get; set; }

        /// <summary>
        /// 邮箱地址
        /// </summary>

        public String Email { get; set; }

        /// <summary>
        /// FullName
        /// </summary>

        public String FullName { get; set; }

        /// <summary>
        /// 状态-正常_0、停用_1、删除_2
        /// </summary>
        public virtual int Active { get; set; }
        public string UpdateBy { get; set; }
        public DateTime CreateTime { get; set; }
        public string CreateBy { get; set; }
        public DateTime? UpdateTime { get; set; }
        public DateTime? LastLoginTime { get; set; }
        /// <summary>
        /// 最后同步时间
        /// </summary>
        public DateTime? LastSyncTime { get; set; }



    }
}
