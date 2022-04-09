using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Lear.CRS.Model
{
    /// <summary>
    /// 参数
    /// </summary>
    public class ContentWeekViewInput
    {
        [JsonIgnore]
        public System.String WWEEK { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// 
        [JsonIgnore]
        public System.String WDesc { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// 
        [JsonIgnore]
        public System.String WContent { get; set; }

    }

}
