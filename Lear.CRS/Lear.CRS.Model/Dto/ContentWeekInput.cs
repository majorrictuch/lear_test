using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Lear.CRS.Model
{
    /// <summary>
    /// 参数
    /// </summary>
    public class ContentWeekInput
    {
        /// <summary>
        /// 
        /// </summary>
        [Required]
        [JsonIgnore]
        public System.String WYearWeek { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Required]
        [JsonIgnore]
        public System.String WType { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Required]
        [JsonIgnore]
        public System.String WPlant { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Required]
        [JsonIgnore]
        public System.String WDepartment { get; set; }

    }
    /// <summary>
    /// 保存参数
    /// </summary>
    public class UpdateContentWeekInput
    {

        public System.Int32 WSerial { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public System.String WID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String WContent { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public System.String WCategory { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String WRemark { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String WDesc { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String WWeek { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String WType { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String WSeq { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.Int32? WSort { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String WPlant { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String WDepartment { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String WHead { get; set; }

      
        /// <summary>
        /// 
        /// </summary>
        public System.String WPriority { get; set; }

       
        /// <summary>
        /// 
        /// </summary>
        public System.String WContentPlan { get; set; }

      
        /// <summary>
        /// 
        /// </summary>
        public System.DateTime? WCreate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String WCreateBy { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.DateTime? WUpdate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String WUpdateBy { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String WFormat { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String WCompare { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.Boolean? WCalculate { get; set; }

    }

}
