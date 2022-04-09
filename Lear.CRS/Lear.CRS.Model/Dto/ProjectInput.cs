using Newtonsoft.Json;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Lear.CRS.Model
{
    /// <summary>
    /// 参数
    /// </summary>
    public class ProjectInput
    {
        /// <summary>
        /// ReportType
        /// </summary>
        [Required]
        [JsonIgnore]
        public System.String ReportType { get; set; }

        /// <summary>
        /// Plant
        /// </summary>
        [Required]
        [JsonIgnore]
        public System.String Plant { get; set; }

        /// <summary>
        /// Department
        /// </summary>
        [Required]
        [JsonIgnore]
        public System.String Department { get; set; }


    }
    /// <summary>
    /// 增加修改的参数
    /// </summary>
    public class ProjectAddInput
    {
        /// <summary>
        /// 
        /// </summary>
        public System.String IID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String ITYPE { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.Int32 ISeq { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.Int32? ISort { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String IPlant { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String IDepartment { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String IHead { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String ICategory { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String IDesc { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String IPriority { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.DateTime? IEffDate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.DateTime? IDisDate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String IHeadMail { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.DateTime? ICreate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String ICreateBy { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.DateTime? IUpdate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String IUpdateBy { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.Object IDateLine { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.Boolean? IPlanFlag { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String IStatType { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String IFormat { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String ICompare { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.Boolean? Icalculate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.Boolean? IShow { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String IName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String IFormula { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String IFormula2 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String IRemark { get; set; }

    }

}
