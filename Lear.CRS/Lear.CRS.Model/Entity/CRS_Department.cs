using SqlSugar;

namespace Lear.CRS.Model
{
    /// <summary>
    /// 
    /// </summary>
    public class CRS_Department
    {
        /// <summary>
        /// 
        /// </summary>
        public CRS_Department()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        public System.String DID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String DDepart { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String DName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String DHead { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String DHeadMail { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.DateTime? DCreate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String DCreateBy { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.DateTime? DUpdate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String DUpdateBy { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String DPlant { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.Int32? DLine { get; set; }
    }
}