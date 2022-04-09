using SqlSugar;

namespace Lear.CRS.Model
{
    /// <summary>
    /// 
    /// </summary>
    public class CRS_Week_VIEW
    {
        /// <summary>
        /// 
        /// </summary>
        public CRS_Week_VIEW()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        public System.Decimal? DTFISY { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.Decimal? DTWEEK { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.Decimal? DTYWK { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String DTWKRANGE { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.Decimal? FromDate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.Decimal? ToDate { get; set; }
    }
}