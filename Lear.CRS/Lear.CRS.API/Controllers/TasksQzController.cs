using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Lear.CRS.Common;
using Lear.CRS.Common.HttpContextUser;
using Lear.CRS.IServices;
using Lear.CRS.Model;
using Lear.CRS.Model.Dto;
using Lear.CRS.Tasks;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;

namespace Lear.CRS.API.Controllers

{
    [Route("[controller]/[action]")]
    [ApiController]
    [Authorize]
    
    public class TasksQzController : BaseController
    {
        private readonly ITasksQzService _tasksQzService;
        private readonly ISchedulerCenter _schedulerCenter;

        public TasksQzController(ITasksQzService tasksQzService, ISchedulerCenter schedulerCenter
            )
        {
            _schedulerCenter = schedulerCenter;
            _tasksQzService = tasksQzService;
        }

        /// <summary>
        /// 所有服务列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<List<TaskQzDto>>> GetAll()
        {

            var list = await _tasksQzService.GetAllTasks();
            List<TaskQzDto> nList = new List<TaskQzDto>();

            foreach (var c in list)
            {
                var nModel = new TaskQzDto
                {
                    CronDesc = CronUtil.translateToChinese(c.Cron),
                    Id = Convert.ToInt32(c.Id),
                    Cron = c.Cron,
                    IsStart = c.IsStart.Value,
                    jobGroup = c.JobGroup,
                    JobParams = c.JobParams,
                    Name = c.Name,
                    Remark = c.Remark,
                    triggerStatus = c.Triggers[0].triggerStatus
                };
                nList.Add(nModel);
            }

            return Success(nList);
        }

        /// <summary>
        /// 执行历史
        /// </summary>
        /// <param name="taskId"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<List<TaskHistoryDto>>> GetLogList(int taskId)
        {

            var list = await _tasksQzService.GetLog(taskId);

            var listResult = list.Adapt<List<TaskHistoryDto>>();



            return Success(listResult);
        }

        /// <summary>
        /// 启动计划任务
        /// </summary>
        /// <param name="jobId"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<string>> Start(string jobId)
        {



            return await _tasksQzService.Start(jobId);

        }
        /// <summary>
        /// 指定执行一个计划任务
        /// </summary>
        /// <param name="jobId"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<string>> Execute(string jobId)
        {

            return await _tasksQzService.Execute(jobId);

        }
        /// <summary>
        /// 停止计划任务
        /// </summary>
        /// <param name="jobId"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<string>> Stop(string jobId)
        {

            return await _tasksQzService.Stop(jobId);

        }

    }
}
