
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Lear.CRS.Model
{
    /// <summary>
    /// 登录输入参数
    /// </summary>
 
    public class LoginInput
    {
       
        /// <summary>
        /// 用户名
        /// </summary>
        /// <example>superAdmin</example>
        [Required(ErrorMessage = "用户名不能为空"), MinLength(3, ErrorMessage = "用户名不能少于3位字符")]
        [JsonIgnore]
        public string Account { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        /// <example>123456</example>
        [Required(ErrorMessage = "密码不能为空"), MinLength(5, ErrorMessage = "密码不能少于5位字符")]
        [JsonIgnore]
        public string Password { get; set; }
    }
}
