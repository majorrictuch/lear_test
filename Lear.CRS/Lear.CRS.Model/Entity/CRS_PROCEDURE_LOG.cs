using SqlSugar;

namespace Lear.CRS.Model
{
    /// <summary>
    /// 
    /// </summary>
    public class CRS_PROCEDURE_LOG
    {
        /// <summary>
        /// 
        /// </summary>
        public CRS_PROCEDURE_LOG()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public System.Int32 ID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String PROCEDURE { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.DateTime RUNTIME { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String RUNTYPE { get; set; }
    }
}