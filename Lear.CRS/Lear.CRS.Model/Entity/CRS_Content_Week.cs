using SqlSugar;

namespace Lear.CRS.Model
{
    /// <summary>
    /// 
    /// </summary>
    public class CRS_Content_Week
    {
        /// <summary>
        /// 
        /// </summary>
        public CRS_Content_Week()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public System.Int32 WSerial { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String WID { get; set; }

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
        public System.String WCategory { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String WDesc { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String WPriority { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String WContent { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String WContentPlan { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String WRemark { get; set; }

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