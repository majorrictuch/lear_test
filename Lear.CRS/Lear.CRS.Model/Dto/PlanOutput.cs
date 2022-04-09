using System;

namespace Lear.CRS.Model
{
    /// <summary>
    /// 用户参数
    /// </summary>
    public class PlanOutput
    {
        public System.Int32 PSerial { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String PID { get; set; }

        public System.String PSeq { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String PFromDate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String PToDate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String PDesc { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public System.String PCategory { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public System.String PPlant { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public System.String Department { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public System.String PType { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public System.String PFormat { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public System.String PContent { get; set; }
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
    }
}
