using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Lear.CRS.Model
{
    /// <summary>
    /// 参数
    /// </summary>
    public class BaseInput
    {
        /// <summary>
        /// 0 日报    1周报   2月报
        /// </summary>
        public System.String Type { get; set; }

        public System.String Plant { get; set; }
        public System.String Depart { get; set; }
        public System.String Date { get; set; }
        public System.String Period { get; set; }
        public System.String Week { get; set; }

    }

   
}
