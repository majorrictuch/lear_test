using System.ComponentModel.DataAnnotations;

namespace Lear.CRS.Model
{
    /// <summary>
    /// API接口参数
    /// </summary>
    public class PlanDepartmentOutput
    {
        /// <summary>
        /// 主键
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// 父Id
        /// </summary>
        public long ParentId { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string ResourceName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ResourceDesc { get; set; }
    }

   


}
