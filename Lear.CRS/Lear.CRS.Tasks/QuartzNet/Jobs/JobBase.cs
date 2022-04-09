using Lear.CRS.IServices;
using Lear.CRS.Model;
using Microsoft.Extensions.Logging;
using Quartz;
using System;
using System.Diagnostics;
using System.Threading.Tasks;


namespace Lear.CRS.Tasks
{
    public class JobBase
    {
        public ITasksQzService _tasksQzServices;
        /// <summary>
        /// 执行指定任务
        /// </summary>
        /// <param name="context"></param>
        /// <param name="action"></param>
        public async Task<string> ExecuteJob(IJobExecutionContext context, Func<Task> func)
        {
            //记录Job时间
            Stopwatch stopwatch = new Stopwatch();
            //JOBID
            string jobid = context.JobDetail.Key.Name;
            //JOB组名
            string groupName = context.JobDetail.Key.Group;

            //日志
            string jobHistory = $"【{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}】【执行开始】";
            //耗时
            double taskSeconds = 0;
            int result = 1;
            try
            {
                stopwatch.Start();
                await func();//执行任务
                stopwatch.Stop();

                jobHistory += $"，【{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}】【执行成功】";
            }
            catch (Exception ex)
            {
                result = 0;
                JobExecutionException e2 = new JobExecutionException(ex);
                //true  是立即重新执行任务 
                e2.RefireImmediately = true;
                jobHistory += $"，【{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}】【执行失败:{ex.Message}】";
            }
            finally
            {
                taskSeconds = Math.Round(stopwatch.Elapsed.TotalSeconds, 3);
                jobHistory += $"，【{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}】【执行结束】(耗时:{taskSeconds}秒)";
                if (_tasksQzServices != null)
                {
                    var model = new TaskQzLog()
                    {
                        ExecContent = jobHistory,
                        TaskId = Convert.ToInt64(jobid),
                        Result = result,
                        TaskSeconds = taskSeconds,
                        GroupName = groupName,


                    };
                    bool res = await _tasksQzServices.AddLog(model);

                }
            }

            Console.Out.WriteLine(jobHistory);
            return jobHistory;
        }
    }

}
