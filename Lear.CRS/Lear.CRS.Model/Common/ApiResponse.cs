using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lear.CRS.Model
{
    public class ApiResponse
    {
        public int Status { get; set; } = 200;
        public string Value { get; set; } = "";
        public ApiResult<string> ApiResult = new ApiResult<string>() { };

        public ApiResponse(StatusCode apiCode, string msg = null)
        {

            Value = msg;
            switch (apiCode)
            {
                case StatusCode.CODE401:
                    {
                        Status = 401;
                        Value = "未登录!";
                    }
                    break;
                case StatusCode.CODE403:
                    {
                        Status = 403;
                        Value = "没有权限访问!";
                    }
                    break;
                case StatusCode.CODE404:
                    {
                        Status = 404;
                        Value = "资源不存在!";
                    }
                    break;
                case StatusCode.CODE500:
                    {
                        Status = 500;
                        Value = msg;
                    }
                    break;
            }

            ApiResult = new ApiResult<string>()
            {
                status = Status,
                msg = Value,
                success = apiCode != StatusCode.CODE200
            };
        }
    }

    public enum StatusCode
    {
        CODE200,
        CODE401,
        CODE403,
        CODE404,
        CODE500
    }
}
