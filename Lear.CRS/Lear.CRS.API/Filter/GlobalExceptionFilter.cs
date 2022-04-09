﻿using Lear.CRS.Common.CustomExceptions;
using Lear.CRS.Common.Helper;
using Lear.CRS.Common.LogHelper;
using Lear.CRS.Model;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;

namespace Lear.CRS.Filter
{
    /// <summary>
    /// 全局异常错误日志
    /// </summary>
    public class GlobalExceptionsFilter : IExceptionFilter
    {
        private readonly IWebHostEnvironment _env;
        private readonly ILogger<GlobalExceptionsFilter> _loggerHelper;

        public GlobalExceptionsFilter(IWebHostEnvironment env, ILogger<GlobalExceptionsFilter> loggerHelper)
        {
            _env = env;
            _loggerHelper = loggerHelper;
        }

        public void OnException(ExceptionContext context)
        {
            var json = new ApiResult<string>();



            if (context.Exception.GetType() == typeof(BusinessException))
            {
                json.status = 200;
                json.success = false;

                json.msg = context.Exception.Message;//错误信息
            }
            else
            {
                json.status = 500;
                json.success = false;

                json.msg = context.Exception.Message;//错误信息
                if (_env.EnvironmentName.ToString().Equals("Development"))
                {
                    json.msgDev = context.Exception.StackTrace;//堆栈信息
                }
            }
            
            
            var errorAudit = "Unable to resolve service for";
            if (!string.IsNullOrEmpty(json.msg) && json.msg.Contains(errorAudit))
            {
                json.msg = json.msg.Replace(errorAudit, $"（若新添加服务，需要重新编译项目）{errorAudit}");
            }

            
            var res = new ContentResult();
            res.Content = JsonHelper.GetJSON<ApiResult<string>>(json);

            context.Result = res;

           // MiniProfiler.Current.CustomTiming("Errors：", json.msg);


            //采用log4net 进行错误日志记录
            _loggerHelper.LogError(json.msg + WriteLog(json.msg, context.Exception));


        }

        /// <summary>
        /// 自定义返回格式
        /// </summary>
        /// <param name="throwMsg"></param>
        /// <param name="ex"></param>
        /// <returns></returns>
        public string WriteLog(string throwMsg, Exception ex)
        {
            return string.Format("\r\n【自定义错误】：{0} \r\n【异常类型】：{1} \r\n【异常信息】：{2} \r\n【堆栈调用】：{3}", new object[] { throwMsg,
                ex.GetType().Name, ex.Message, ex.StackTrace });
        }

    }
    public class InternalServerErrorObjectResult : ObjectResult
    {
        public InternalServerErrorObjectResult(object value) : base(value)
        {
            StatusCode = StatusCodes.Status500InternalServerError;
        }
    }
    //返回错误信息
    public class JsonErrorResponse
    {
        /// <summary>
        /// 生产环境的消息
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// 开发环境的消息
        /// </summary>
        public string DevelopmentMessage { get; set; }
    }

}
