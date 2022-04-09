using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lear.CRS.Model.Dto
{
    public class TaskQzDto
    {
        /// <summary>
        /// 任务名称
        /// </summary>
        public int Id { get; set; }
        public string Name { get; set; }
        /// <summary>
        /// 任务描述
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// 任务分组
        /// </summary>
        public string jobGroup { get; set; }
        /// <summary>
        /// 参数
        /// </summary>
        public string JobParams { get; set; }
        public string Cron { get; set; }
        public string CronDesc { get; set; }
        /// <summary>
        /// 是否启动
        /// </summary>
        public bool IsStart { get; set; } = false;

        /// <summary>
        /// 触发器状态
        /// </summary>
        public string triggerStatus { get; set; }
    }
}
