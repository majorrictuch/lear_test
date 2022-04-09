using SqlSugar;

namespace Lear.CRS.Model
{
    /// <summary>
    /// 
    /// </summary>
    public class CRS_Plan
    {
        /// <summary>
        /// 
        /// </summary>
        public CRS_Plan()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public System.Int32 PSerial { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String PID { get; set; }

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
        public System.String PType { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String PSeq { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String PDesc { get; set; }

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

        /// <summary>
        /// 
        /// </summary>
        public System.String PCategory { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String PFormat { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String PPlant { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.Int32? PSort { get; set; }
    }
}