using System.ComponentModel.DataAnnotations;

namespace Lear.CRS.Model
{
    /// <summary>
    /// API接口参数
    /// </summary>
    public class ModuleInput
    {
        /// <summary>
        /// 父Id
        /// </summary>
        public virtual long ParentId { get; set; }
        /// <summary>
        /// 编码
        /// </summary>
        public virtual string Code { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public virtual string Name { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public virtual int Sort { get; set; }
        /// <summary>
        /// action
        /// </summary>
        public virtual string Action { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public virtual string Description { get; set; }

    }

    public class AddModuleInput : ModuleInput
    {

    }

    public class UpdateModuleInput : ModuleInput
    {

    }


    public class QueryModuleInput : PageInputBase
    {
        /// <summary>
        /// 父Id
        /// </summary>
        public virtual long ParentId { get; set; }
        /// <summary>
        /// 编码
        /// </summary>
        public virtual string Code { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public virtual string Name { get; set; }

    }


}
