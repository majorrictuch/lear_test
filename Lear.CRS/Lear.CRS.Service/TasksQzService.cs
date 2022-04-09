

using Lear.CRS.Common;
using Lear.CRS.Common.HttpContextUser;
using Lear.CRS.IServices;
using Lear.CRS.Model;
using Lear.CRS.Model.Permission;
using Lear.CRS.Tasks;
using Mapster;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yitter.IdGenerator;

namespace Lear.CRS.Services
{
    public partial class TasksQzService : ITasksQzService
    {

        private readonly ISqlSugarRepository<TasksQz> _taskRepository;
        private readonly ISqlSugarRepository<TaskQzLog> _taskLogRepository;
        private readonly ISchedulerCenter _schedulerCenter;


        public TasksQzService(ISqlSugarRepository<TasksQz> taskRepository,
             ISchedulerCenter schedulerCenter, ISqlSugarRepository<TaskQzLog> taskLogRepository)
        {
            this._taskRepository = taskRepository;

            this._schedulerCenter = schedulerCenter;
            this._taskLogRepository = taskLogRepository;
        }

        public async Task<bool> AddLog(TaskQzLog log)
        {
            return await _taskLogRepository.InsertAsync(log) > 0;

        }
        public async Task<List<TaskQzLog>> GetLog(int taskId)
        {
            return await _taskLogRepository.Where(c => c.TaskId == taskId).OrderBy(c => c.Id, OrderByType.Desc).ToListAsync();

        }
        public async Task<ApiResult<string>> Execute(string jobId)
        {
            var data = new ApiResult<string>();

            var model = _taskRepository.Single(jobId);


            if (model != null)
            {
                return await _schedulerCenter.ExecuteJobAsync(model);
            }
            else
            {
                data.msg = "任务不存在";
            }
            return data;
        }

        public async Task<List<TasksQz>> GetAllTasks()
        {

            var list = await _taskRepository.ToListAsync();

            //  var taskList = _mapper.Map<List<TasksQz>>(list);
            if (list.Count > 0)
            {
                foreach (var item in list)
                {
                    item.Triggers = await _schedulerCenter.GetTaskStaus(item);
                }
            }

            return list;

        }

     


        public async Task<ApiResult<string>> Start(string jobId)
        {
            var data = new ApiResult<string>();

            var model =  _taskRepository.Single(jobId);


            if (model != null)
            {
                
                    model.IsStart = true;
                    data.success = await _taskRepository.UpdateAsync(model)>0;
                    data.response = jobId.ToString();
                    if (data.success)
                    {
                        //data.msg = "启动成功";

                       // var task = this._mapper.Map<TasksQz>(model);
                        var ResuleModel = await _schedulerCenter.AddScheduleJobAsync(model);
                        data.success = ResuleModel.success;
                        if (ResuleModel.success)
                        {
                            data.msg = $"启动成功";

                        }
                        else
                        {
                            data.msg = ResuleModel.msg;
                        }
                    }
                    else
                    {
                        data.msg = "启动失败";
                    }
            }
            else
            {
                data.msg = "任务不存在";
            }
            return data;
        }

        /// <summary>
        /// 停止一个计划任务
        /// </summary>
        /// <param name="jobId"></param>
        /// <returns></returns>        

        public async Task<ApiResult<string>> Stop(string jobId)
        {
            var data = new ApiResult<string>();

            var model =  _taskRepository.Single(jobId);
            if (model != null)
            {
                model.IsStart = false;
                data.success = await _taskRepository.UpdateAsync(model)>0;
                data.response = jobId.ToString();
                if (data.success)
                {
                    data.msg = "停止成功";
                    var ResuleModel = await _schedulerCenter.StopScheduleJobAsync(model);

                    if (ResuleModel.success)
                    {
                        data.msg = $"停止成功";
                    }
                    else
                    {
                        data.msg = $"停止失败";
                    }
                }
                else
                {
                    data.msg = "停止失败";
                }
            }
            else
            {
                data.msg = "任务不存在";
            }
            return data;
        }

        public Task<TasksQz> GetTaskById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Update(TasksQz model)
        {
            throw new NotImplementedException();
        }



        //public async Task<bool> Update(TasksQz model)
        //{

        //    var task = await _taskRepository.QueryById(model.Id);

        //    task.run_times = model.RunTimes;
        //    task.remark = model.Remark;
        //    return await _taskRepository.Update(task);


        //}
    }
}
