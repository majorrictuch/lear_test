using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Lear.CRS.Model
{
    /// <summary>
    /// 参数
    /// </summary>
    public class DailyReportInput
    {
        /// <summary>
        /// 
        /// </summary>
        /// 
        [JsonIgnore]
        public System.String WSeq { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// 
        [JsonIgnore]
        public System.String WContent { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// 
        [JsonIgnore]
        public System.String WContentPlan { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// 
        [JsonIgnore]
        public System.String WRemark { get; set; }

    }

}
