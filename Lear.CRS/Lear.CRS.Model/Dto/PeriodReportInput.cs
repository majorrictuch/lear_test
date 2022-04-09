using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Lear.CRS.Model
{
    /// <summary>
    /// 参数
    /// </summary>
    public class PeriodReportInput
    {
        /// <summary>
        ///工厂
        /// </summary>
        /// 
        [JsonIgnore]
        public virtual string Plant { get; set; }
        /// <summary>
        /// Period
        /// </summary>
        /// 
        [JsonIgnore]
        public virtual int Period { get; set; }
        /// <summary>
        /// Year
        /// </summary>
        /// 

        [JsonIgnore]
        public virtual int Year { get; set; }



    }

}
