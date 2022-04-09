using Microsoft.Extensions.Logging;
using Quartz;
using Quartz.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Lear.CRS.Tasks.QuartzNet
{
    public class CustomSchedulerListener : ISchedulerListener
    {


       
        public async Task JobAdded(IJobDetail jobDetail, CancellationToken cancellationToken = default)
        {

            await Task.Run(() =>
            {
                Console.WriteLine($"{DateTime.Now}：JobAdded");
            });
        }

        public async Task JobDeleted(JobKey jobKey, CancellationToken cancellationToken = default)
        {
            await Task.Run(() =>
            {
                Console.WriteLine($"{DateTime.Now}：JobDeleted");
            });
        }

        public async Task JobInterrupted(JobKey jobKey, CancellationToken cancellationToken = default)
        {
            await Task.Run(() =>
            {
                Console.WriteLine($"{DateTime.Now}：JobInterrupted");
            });
        }

        public async Task JobPaused(JobKey jobKey, CancellationToken cancellationToken = default)
        {
            await Task.Run(() =>
            {
                Console.WriteLine($"{DateTime.Now}：JobPaused");
            });
        }

        public async Task JobResumed(JobKey jobKey, CancellationToken cancellationToken = default)
        {
            await Task.Run(() =>
            {
                Console.WriteLine($"{DateTime.Now}：JobResumed");
            });
        }

        public async Task JobScheduled(ITrigger trigger, CancellationToken cancellationToken = default)
        {
            await Task.Run(() =>
            {
                Console.WriteLine($"{DateTime.Now}：JobScheduled");
            });
        }

        public async Task JobsPaused(string jobGroup, CancellationToken cancellationToken = default)
        {
            await Task.Run(() =>
            {
                Console.WriteLine($"{DateTime.Now}：JobsPaused");
            });
        }

        public async Task JobsResumed(string jobGroup, CancellationToken cancellationToken = default)
        {
            await Task.Run(() =>
            {
                Console.WriteLine($"{DateTime.Now}：JobsResumed");
            });
        }

        public async Task JobUnscheduled(TriggerKey triggerKey, CancellationToken cancellationToken = default)
        {
            await Task.Run(() =>
            {
                Console.WriteLine($"{DateTime.Now}：JobUnscheduled");
            });
        }

        public async Task SchedulerError(string msg, SchedulerException cause, CancellationToken cancellationToken = default)
        {
            await Task.Run(() =>
            {
                Console.WriteLine($"{DateTime.Now}：SchedulerError");
            });
        }

        public async Task SchedulerInStandbyMode(CancellationToken cancellationToken = default)
        {
            await Task.Run(() =>
            {
                Console.WriteLine($"{DateTime.Now}：SchedulerInStandbyMode");
            });
        }

        public async Task SchedulerShutdown(CancellationToken cancellationToken = default)
        {
            await Task.Run(() =>
            {
                Console.WriteLine($"{DateTime.Now}：SchedulerShutdown");
            });
        }

        public async Task SchedulerShuttingdown(CancellationToken cancellationToken = default)
        {
            await Task.Run(() =>
            {
                Console.WriteLine($"{DateTime.Now}：SchedulerShuttingdown");
            });
        }

        public async Task SchedulerStarted(CancellationToken cancellationToken = default)
        {
            await Task.Run(() =>
            {
                Console.WriteLine($"{DateTime.Now}：SchedulerStarted");
            });
        }

        public async Task SchedulerStarting(CancellationToken cancellationToken = default)
        {
            await Task.Run(() =>
            {
                Console.WriteLine($"{DateTime.Now}：SchedulerStarting");
            });
        }

        public async Task SchedulingDataCleared(CancellationToken cancellationToken = default)
        {
            await Task.Run(() =>
            {
                Console.WriteLine($"{DateTime.Now}：SchedulingDataCleared");
            });
        }

        public async Task TriggerFinalized(ITrigger trigger, CancellationToken cancellationToken = default)
        {
            await Task.Run(() =>
            {
                Console.WriteLine($"{DateTime.Now}：TriggerFinalized");
            });
        }

        public async Task TriggerPaused(TriggerKey triggerKey, CancellationToken cancellationToken = default)
        {
            await Task.Run(() =>
            {
                Console.WriteLine($"{DateTime.Now}：TriggerPaused");
            });
        }

        public async Task TriggerResumed(TriggerKey triggerKey, CancellationToken cancellationToken = default)
        {
            await Task.Run(() =>
            {
                Console.WriteLine($"{DateTime.Now}：TriggerResumed");
            });
        }

        public async Task TriggersPaused(string triggerGroup, CancellationToken cancellationToken = default)
        {
            await Task.Run(() =>
            {
                Console.WriteLine($"{DateTime.Now}：TriggersPaused");
            });
        }

        public async Task TriggersResumed(string triggerGroup, CancellationToken cancellationToken = default)
        {
            await Task.Run(() =>
            {
                Console.WriteLine($"{DateTime.Now}：TriggersResumed");
            });
        }

    }
}
