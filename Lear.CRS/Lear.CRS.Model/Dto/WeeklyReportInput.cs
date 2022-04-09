using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Lear.CRS.Model
{
    /// <summary>
    /// 参数
    /// </summary>
    public class WeeklyReportInput
    {
        /// <summary>
        ///工厂
        /// </summary>
        /// 
        [JsonIgnore]
        public  string Plant { get; set; }
        /// <summary>
        /// Period
        /// </summary>
        /// 
        [JsonIgnore]
        public  int CurrentYear { get; set; }
        /// <summary>
        /// Year
        /// </summary>
        /// 
        [JsonIgnore]
        public  int CurrentWeek { get; set; }

    }

}
