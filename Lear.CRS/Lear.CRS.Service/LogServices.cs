
using Lear.CRS.Common.CustomExceptions;
using Lear.CRS.Common.HttpContextUser;
using Lear.CRS.IServices;
using Lear.CRS.Model;
using Lear.CRS.SqlSugarCore.Extensions;
using Mapster;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Yitter.IdGenerator;

namespace Lear.CRS.Service
{
    /// <summary>
    /// RoleServices
    /// </summary>	
    public class LogServices : ILogServices
    {
        private readonly ISqlSugarRepository<LoginLog> _loginLogRep;  // 文件信息表仓储 

        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IUser _user;
        public LogServices(ISqlSugarRepository<LoginLog> loginLogRep, IConfiguration configuration, IWebHostEnvironment webHostEnvironment, IUser user)
        {
            _loginLogRep = loginLogRep;
            _configuration = configuration;
            _webHostEnvironment = webHostEnvironment;
            this._user = user;
        }

        public async Task<dynamic> QueryJobLogList(LogInput input)
        {
            var list = await _loginLogRep.Context.Queryable<LoginLog, UserMaster>((l, u) => new JoinQueryInfos(
          JoinType.Left, l.UserId == u.Id

          ))
                .WhereIF(!string.IsNullOrEmpty(input.FullName), (l, u) => u.FullName.Contains(input.FullName))
                .WhereIF(!string.IsNullOrEmpty(input.Account), (l, u) => u.Account.Contains(input.Account))
                .WhereIF(!string.IsNullOrEmpty(input.EmpId), (l, u) => u.EmpId.Contains(input.EmpId))
                .WhereIF(!string.IsNullOrEmpty(input.SearchBeginTime), (l, u) => l.CreateTime >= Convert.ToDateTime(input.SearchBeginTime))
                .WhereIF(!string.IsNullOrEmpty(input.SearchEndTime), (l, u) => l.CreateTime <= Convert.ToDateTime(input.SearchEndTime))
                .OrderBy("l.CreateTime desc")
                             .Select<LogOutput>("u.Account,u.FullName,u.EmpId,l.IP,l.CreateTime")
                            .ToPagedListAsync(input.PageNo, input.PageSize);


            return list.XnPagedResult();
        }

    }
}
