using SqlSugar;

namespace Lear.CRS.Model
{
    /// <summary>
    /// 
    /// </summary>
    public class CRS_Period_VIEW
    {
        /// <summary>
        /// 
        /// </summary>
        public CRS_Period_VIEW()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        public System.String PPlant { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.Int32 PYEAR { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.Int32? PERIOD { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String DateRange { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.Int32? PStartDate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.Int32? PEndDate { get; set; }
    }
}