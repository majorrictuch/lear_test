using Lear.CRS.Model;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Lear.CRS.Model
{
    /// <summary>
    /// 角色参数
    /// </summary>
    public class RoleInput 
    {
        /// <summary>
        /// 名称
        /// </summary>
        public virtual string Name { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public virtual string Description { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public virtual bool Enabled { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int Sort { get; set; }


        //public List<long> UserIds { get; set; }
        public List<long> MenuIds { get; set; }

        

    }

    public class AddRoleInput : RoleInput
    {
        /// <summary>
        /// 名称
        /// </summary>
        [Required(ErrorMessage = "角色名称不能为空")]
        [JsonIgnore]
        public  string Name { get; set; }

       
    }

    public class DeleteRoleInput
    {
        /// <summary>
        /// 角色Id
        /// </summary>
        [Required(ErrorMessage = "角色Id不能为空")]
        public long Id { get; set; } 
    }

    public class UpdateRoleInput : RoleInput
    {
        /// <summary>
        /// 角色Id
        /// </summary>
        [Required(ErrorMessage = "角色Id不能为空")]
        [JsonIgnore]
        public long Id { get; set; }
    }

    public class QueryRoleInput : PageInputBase
    {
        /// <summary>
        /// 名称
        /// </summary>
        [JsonIgnore]

        public virtual string Name { get; set; }


        /// <summary>
        /// 是否启用
        /// </summary>
        /// 
        [JsonIgnore]
        public virtual bool? Enabled { get; set; }


    }

    public class GrantRoleMenuInput : RoleInput
    {
        /// <summary>
        /// 角色Id
        /// </summary>
        [Required(ErrorMessage = "角色Id不能为空")]
        public long Id { get; set; }
    }

    public class GrantRoleDataInput : GrantRoleMenuInput
    {

    }
    
}