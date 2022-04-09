using SqlSugar;

namespace Lear.CRS.Model
{
    /// <summary>
    /// 
    /// </summary>
    public class CRS_Period
    {
        /// <summary>
        /// 
        /// </summary>
        public CRS_Period()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        public System.String PPlant { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.Int32 PYear { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.Int32 PPeriod { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.Int32? PStartDate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.Int32? PEndDate { get; set; }

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
        public System.Boolean POpenFlag { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.Int32? PCutOffDate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.DateTime? PCutOffDateTime { get; set; }
    }
}