using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Lear.CRS.Model
{
    /// <summary>
    /// 用户参数
    /// </summary>
    public class UserInput
    {
        /// <summary>
        /// 账号
        /// </summary>
        public virtual string Account { get; set; }

        /// <summary>
        /// 员工编号
        /// </summary>
        public virtual string EmpId { get; set; }

        public String FirstName { get; set; }

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
        /// 状态
        /// </summary>
        public virtual int Active { get; set; }
        /// <summary>
        /// 同步时间
        /// </summary>
        public virtual DateTime LastSyncTime { get; set; }


    }

    public class AddUserInput
    {

        [Required]
        [DisplayName("用户名")]
        public String Account { get; set; }

        /// <summary>
        /// 员工编号
        /// </summary>
        [Required]
        [DisplayName("员工编号")]
        public String EmpId { get; set; }


        /// <summary>
        /// 状态
        /// </summary>
        /// 
        [JsonIgnore]
        public virtual int Active { get; set; }

        /// <summary>
        /// FirstName
        /// </summary>

        [DisplayName("FirstName")]
        [JsonIgnore]
        public String FirstName { get; set; }

        /// <summary>
        /// LastName
        /// </summary>

        [DisplayName("LastName")]
        [JsonIgnore]
        public String LastName { get; set; }

        /// <summary>
        /// 邮箱地址
        /// </summary>

        [DisplayName("邮箱地址")]
        [JsonIgnore]
        public String Email { get; set; }

        /// <summary>
        /// FullName
        /// </summary>

        [DisplayName("FullName")]
        [JsonIgnore]
        public String FullName { get; set; }




    }

    public class DeleteUserInput
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        [Required(ErrorMessage = "用户Id不能为空")]
        public long Id { get; set; }
    }

    public class UpdateUserInput : UserInput
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        [Required(ErrorMessage = "用户Id不能为空")]
        [JsonIgnore]
        public long Id { get; set; }

    }

    public class QueryUserInput : PageInputBase
    {
        /// <summary>
        /// 账号
        /// </summary>
        /// 
        [JsonIgnore]
        public virtual string Account { get; set; }

        /// <summary>
        /// 员工编号
        /// </summary>
        /// 
        [JsonIgnore]
        public virtual string EmpId { get; set; }
    }

    public class QueryUserAllInput 
    {
        /// <summary>
        /// 账号
        /// </summary>
        /// 
        [JsonIgnore]
        public virtual string Account { get; set; }
        /// <summary>
        /// FullName
        /// </summary>
        /// 
        [JsonIgnore]
        public virtual string FullName { get; set; }

        /// <summary>
        /// 员工编号
        /// </summary>
        /// 
        [JsonIgnore]
        public virtual string EmpId { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        /// 
        [JsonIgnore]
        public virtual int Active { get; set; }
    }


}
