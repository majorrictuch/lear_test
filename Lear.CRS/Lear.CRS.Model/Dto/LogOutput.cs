using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Lear.CRS.Model

{
    /// <summary>
    /// 日志
    /// </summary>
    public class LogOutput 
    {
    

        /// <summary>
        /// 用户名
        /// </summary>
        public string Account { get; set; }
        /// <summary>
        /// 员工编号
        /// </summary>
        public string EmpId { get; set; }
        /// <summary>
        /// FullName
        /// </summary>
        public string FullName { get; set; }
        /// <summary>
        /// 登录IP
        /// </summary>
        public string IP { get; set; }
        /// <summary>
        /// 登录时间
        /// </summary>
        public string CreateTime { get; set; }

        
    }

   
}
