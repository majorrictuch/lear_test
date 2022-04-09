using SqlSugar;

namespace Lear.CRS.Model
{
    /// <summary>
    /// 
    /// </summary>
    public class CRS_Date
    {
        /// <summary>
        /// 
        /// </summary>
        public CRS_Date()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        [SugarColumn(IsPrimaryKey = true)]
        public System.Decimal DTYMD { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.Decimal? DTMDY { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String DTAYMD { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.Decimal? DTCENT { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.Decimal? DTYEAR { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.Decimal? DTMTH { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.Decimal? DTDAY { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.Decimal? DTJUL { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.Decimal? DTJULY { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.Decimal? DTJULD { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.Decimal? DTCDAY { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String DTTYPE { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String DTDDSC { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.Decimal? DTWDAY { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.Decimal? DTWEEK { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.Decimal? DTFISY { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.Decimal? DTFISQ { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.Decimal? DTFISP { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String DTEXT1 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String DTEXT2 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String DTEXT3 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String DTEXT4 { get; set; }
    }
}