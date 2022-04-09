using Lear.CRS.Model.Dto;
using SqlSugar;
using System.Collections.Generic;

namespace Lear.CRS.Model
{
    /// <summary>
    /// 
    /// </summary>
    public class TasksQz
    {
        /// <summary>
        /// 
        /// </summary>
        public TasksQz()
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
        public System.String Name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String JobGroup { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String Cron { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String AssemblyName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String ClassName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String Remark { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.DateTime? BeginTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.DateTime? EndTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.Int32? TriggerType { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.Int32? IntervalSecond { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.Int32? CycleRunTimes { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.Boolean? IsStart { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String JobParams { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String CreateBy { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.DateTime? CreateTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String UpdateBy { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.DateTime? UpdateTime { get; set; }

        /// <summary>
        /// 任务内存中的状态
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        public List<TaskInfoDto> Triggers { get; set; }
    }
}