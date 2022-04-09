using SqlSugar;

namespace Lear.CRS.Model
{
    /// <summary>
    /// 
    /// </summary>
    public class CRS_Content
    {
        /// <summary>
        /// 
        /// </summary>
        public CRS_Content()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public System.Int32 CSerial { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String CID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String CDate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String CType { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String CSeq { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.Int32? CSort { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String CPlant { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String CDepartment { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String CHead { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String CCategory { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String CDesc { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String CPriority { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String CContent { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String CContentPlan { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String CRemark { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.DateTime? CCreate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String CCreateBy { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.DateTime? CUpdate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String CUpdateBy { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String CFormat { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String CCompare { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.Boolean? CCalculate { get; set; }
    }
}