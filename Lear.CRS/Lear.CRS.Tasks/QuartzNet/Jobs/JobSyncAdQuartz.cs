using Lear.CRS.IServices;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lear.CRS.Tasks
{
    public class JobSyncAdQuartz : JobBase, IJob
    {
        private readonly IUserServices _userServices;


        public JobSyncAdQuartz(IUserServices userServices, ITasksQzService tasksQzServices)
        {
            _userServices = userServices;
            _tasksQzServices = tasksQzServices;
        }
        public async Task Execute(IJobExecutionContext context)
        {
            var executeLog = await ExecuteJob(context, async () => await Run(context));
        }
        public async Task Run(IJobExecutionContext context)
        {

            await _userServices.AdSync(true);


        }
    }
}
