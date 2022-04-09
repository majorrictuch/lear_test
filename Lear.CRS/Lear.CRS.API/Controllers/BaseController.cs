using Lear.CRS.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lear.CRS.API.Controllers
{
    public class BaseController : ControllerBase
    {



        [NonAction]
        public ApiResult Success((string, bool) data, string msg = "成功")
        {
            return new ApiResult()
            {
                success = data.Item2,
                msg = msg,
                response = data.Item1,
            };
        }
        [NonAction]
        public ApiResult<T> Success<T>(T data, string msg = "成功")
        {
            return new ApiResult<T>()
            {
                success = true,
                msg = msg,
                response = data,
            };
        }
        [NonAction]
        public ApiResult Result((bool, string) data)
        {
            return new ApiResult()
            {
                success = data.Item1,
                msg = data.Item2
            };
        }
        [NonAction]
        public ApiResult Success(string msg = "成功")
        {
            return new ApiResult()
            {
                success = true,
                msg = msg,
                response = null,
            };
        }
        [NonAction]
        public ApiResult<string> Failed(string msg = "失败", int status = 500)
        {
            return new ApiResult<string>()
            {
                success = false,
                status = status,
                msg = msg,
                response = null,
            };
        }
        [NonAction]
        public ApiResult<T> Failed<T>(string msg = "失败", int status = 500)
        {
            return new ApiResult<T>()
            {
                success = false,
                status = status,
                msg = msg,
                response = default,
            };
        }

        
        



    }

}
