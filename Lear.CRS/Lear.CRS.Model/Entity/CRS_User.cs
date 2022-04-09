using SqlSugar;

namespace Lear.CRS.Model
{
    /// <summary>
    /// 
    /// </summary>
    public class CRS_User
    {
        /// <summary>
        /// 
        /// </summary>
        public CRS_User()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        [SugarColumn(IsPrimaryKey = true)]
        public System.Int32 USerial { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String UserID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String Plant { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String Department { get; set; }
    }
}