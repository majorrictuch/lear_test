using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Lear.CRS.Model
{
    /// <summary>
    /// 字典参数
    /// </summary>
    public class DictionaryConfigInput 
    {
        /// <summary>
        /// 类别
        /// </summary>
        [Required(ErrorMessage = "类别不能为空")]
        [JsonIgnore]
        public virtual string Type { get; set; }

        /// <summary>
        /// 类别名称
        /// </summary>
        /// 
        [JsonIgnore]
        public virtual string TypeName { get; set; }


        /// <summary>
        /// 编码
        /// </summary>
        /// 
        [JsonIgnore]
        public virtual string Code { get; set; }

        /// <summary>
        /// 值
        /// </summary>
        /// 
        [JsonIgnore]
        public string Value { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        /// 
        [JsonIgnore]
        public string Description { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        /// 
        [JsonIgnore]
        public bool Enabled { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        /// 
        [JsonIgnore]
        public int Sort { get; set; }
    }
    /// <summary>
    /// 新增字典参数
    /// </summary>
    public class AddDictionaryConfigInput : DictionaryConfigInput
    {
       

        /// <summary>
        /// 编码
        /// </summary>
        [Required(ErrorMessage = "应用编码不能为空")]
        [JsonIgnore]
        public override string Code { get; set; }
    }
    /// <summary>
    /// 修改字典参数
    /// </summary>
    public class UpdateDictionaryConfigInput : DictionaryConfigInput
    {
        /// <summary>
        /// 应用Id
        /// </summary>
        [Required(ErrorMessage = "应用Id不能为空")]
        [JsonIgnore]
        public long Id { get; set; }
    }

   
}
