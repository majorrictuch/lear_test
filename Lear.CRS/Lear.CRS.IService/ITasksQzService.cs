



using Lear.CRS.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lear.CRS.IServices

{
    /// <summary>
    /// ITasksQzServices
    /// </summary>	
    public interface ITasksQzService
    {



        Task<TasksQz> GetTaskById(int id);
        Task<bool> Update(TasksQz model);
        Task<bool> AddLog(TaskQzLog log);

        Task<List<TaskQzLog>> GetLog(int taskId);
        Task<List<TasksQz>> GetAllTasks();

        Task<ApiResult<string>> Start(string jobId);
        Task<ApiResult<string>> Execute(string jobId);
        Task<ApiResult<string>> Stop(string jobId);
      


    }
}
