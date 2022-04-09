using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lear.CRS.AuthHelper.OverWrite;
using Lear.CRS.Common.Helper;
using Lear.CRS.Common.HttpContextUser;
using Lear.CRS.IServices;
using Lear.CRS.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Lear.CRS.API.Controllers

{
    /// <summary>
    /// User module
    /// </summary>
    [Route("[controller]/[action]")]
    [ApiController]
    [Authorize]
    
    public class ProjectController : BaseController
    {
        readonly IProjectServices _projectServices;
        private readonly IUser _user;
        private readonly ILogger<UserController> _logger;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="projectServices"></param>
        /// <param name="user"></param>
        /// <param name="logger"></param>
        public ProjectController(IProjectServices projectServices,  IUser user, ILogger<UserController> logger)
        {
            _projectServices = projectServices;
            _user = user;
            _logger = logger;
        }





        /// <summary>
        /// 新增项目
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult> AddProject(List<ProjectAddInput> input) => Success((await _projectServices.AddProject(input)).ToString());


        /// <summary>
        /// 更新项目
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<ApiResult> UpdateProject(List<ProjectAddInput> input) => Success((await _projectServices.UpdateProject(input)).ToString());

        /// <summary>
        /// 获取项目明细
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<List<ProjectOutput>>> GetProject([FromQuery] ProjectInput input)
        {
            var data = await _projectServices.GetProject(input);
            return Success(data);

        }



    }
}
