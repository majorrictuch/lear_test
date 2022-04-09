using SqlSugar;

namespace Lear.CRS.Model
{
    /// <summary>
    /// 
    /// </summary>
    public class CRS_Content_Week_View
    {
        /// <summary>
        /// 
        /// </summary>
        public CRS_Content_Week_View()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        public System.String WWEEK { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String WDesc { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String WContent { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public System.Int32 WSerial { get; set; }
    }
}