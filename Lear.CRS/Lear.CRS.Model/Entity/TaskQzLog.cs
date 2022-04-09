using SqlSugar;

namespace Lear.CRS.Model
{
    /// <summary>
    /// 
    /// </summary>
    public class TaskQzLog
    {
        /// <summary>
        /// 
        /// </summary>
        public TaskQzLog()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public System.Int32 Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.Int64? TaskId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.Int32? Result { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.Double? TaskSeconds { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String ExecContent { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String ExecServer { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String GroupName { get; set; }
    }
}