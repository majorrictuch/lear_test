using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Lear.CRS.Model
{
    /// <summary>
    /// 参数
    /// </summary>
    public class ContentPeriodInput
    {
        /// <summary>
        /// 
        /// </summary>
        [Required]
        [JsonIgnore]
        public System.String PYearPeriod { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Required]
        [JsonIgnore]
        public System.String PType { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Required]
        [JsonIgnore]
        public System.String PPlant { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Required]
        [JsonIgnore]
        public System.String PDepartment { get; set; }

    }
    /// <summary>
    /// 保存参数
    /// </summary>
    public class UpdateContentPeriodInput
    {

        public System.Int32 PSerial { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public System.String PID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String PContent { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public System.String PCategory { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String PRemark { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String PDesc { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public System.String PPeriod { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String PType { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String PSeq { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.Int32? PSort { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String PPlant { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String PDepartment { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String PHead { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String PPriority { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String PContentPlan { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String PFormat { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String PCompare { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.Boolean? PCalculate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.DateTime? PCreate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String PCreateBy { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.DateTime? PUpdate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String PUpdateBy { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String PStatus { get; set; }

    }
}
