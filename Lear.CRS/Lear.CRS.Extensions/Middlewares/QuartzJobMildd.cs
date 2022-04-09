using Lear.CRS.Common;
using Lear.CRS.IServices;
using Lear.CRS.Tasks;
using log4net;
using Microsoft.AspNetCore.Builder;
using System;

namespace Lear.CRS.Middlewares

{
    /// <summary>
    /// Quartz 启动服务
    /// </summary>
    public static class QuartzJobMildd
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(QuartzJobMildd));
        public static void UseQuartzJobMildd(this IApplicationBuilder app, ITasksQzService tasksQzServices, ISchedulerCenter schedulerCenter)
        {
            if (app == null) throw new ArgumentNullException(nameof(app));

            try
            {
                if (Appsettings.app("Middleware", "QuartzNetJob", "Enabled").ToBool())
                {

                    var allQzServices = tasksQzServices.GetAllTasks().Result;
                    foreach (var item in allQzServices)
                    {
                        if ((bool)item.IsStart)
                        {
                            var ResuleModel = schedulerCenter.AddScheduleJobAsync(item).Result;
                            if (ResuleModel.success)
                            {
                                Console.WriteLine($"QuartzNetJob{item.Name}启动成功！");
                            }
                            else
                            {
                                Console.WriteLine($"QuartzNetJob{item.Name}启动失败！错误信息：{ResuleModel.msg}");
                            }
                        }
                    }

                }
            }
            catch (Exception e)
            {
                log.Error($"An error was reported when starting the job service.\n{e.Message}");
                throw;
            }
        }
    }
}