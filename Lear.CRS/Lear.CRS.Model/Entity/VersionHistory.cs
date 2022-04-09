using SqlSugar;

namespace Lear.CRS.Model
{
    /// <summary>
    /// 版本记录表
    /// </summary>
    public class VersionHistory
    {
        /// <summary>
        /// 
        /// </summary>
        public VersionHistory()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        [SugarColumn(IsPrimaryKey = true)]
        public System.Int64 Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String Version { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String Number { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String RevisionDate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String Developer { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String CompanyName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String RevisionNotes { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String Tickets { get; set; }
    }
}
