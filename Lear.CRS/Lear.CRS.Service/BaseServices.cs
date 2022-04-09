
using Lear.CRS.Common.Helper;
using Lear.CRS.Common.HttpContextUser;
using Lear.CRS.IServices;
using Lear.CRS.Model;
using Lear.CRS.SqlSugarCore.Extensions;
using Mapster;
using Microsoft.Data.SqlClient;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Yitter.IdGenerator;

namespace Lear.CRS.Service
{
    /// <summary>
    /// RoleServices
    /// </summary>	
    public class BaseServices
    {
        private readonly ISqlSugarRepository<UserMaster> _reportRep;
        private readonly IUser _user;

        public BaseServices(ISqlSugarRepository<UserMaster> reportRep, IUser user)
        {
            _reportRep = reportRep;
            this._user = user;
        }
      
        //通过线程池发送邮件
        public void SendViaThread(BaseInput mailobj)
        {
            try
            {
                //System.Threading.ThreadPool.QueueUserWorkItem(EMailHelper.SendApprovalMail, mailobj);
                System.Threading.ThreadPool.QueueUserWorkItem(Storage, mailobj);

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public  void Storage(object mailobject)
        {
            BaseInput mailobj = (BaseInput)mailobject;
            if (mailobj.Type == "0") {
                var Plan = new SugarParameter("@Plant", mailobj.Plant);
                var Department = new SugarParameter("@Department", mailobj.Depart);
                var DateFrom = new SugarParameter("@DateFrom", mailobj.Date.Replace("-",""));
                var DateTo = new SugarParameter("@DateTo", mailobj.Date.Replace("-", ""));


                var model = _reportRep.Ado.UseStoredProcedure().GetString("SP_Z_KPI_Calculate", new List<SugarParameter> { Plan, Department, DateFrom, DateTo });

            }
            if (mailobj.Type == "1") {
                var Plan = new SugarParameter("@Plant", mailobj.Plant);
                var Department = new SugarParameter("@Department", mailobj.Depart);
                var Week = new SugarParameter("@Week", mailobj.Week);

                var model = _reportRep.Ado.UseStoredProcedure().SqlQuery<PeriodReportOutput>("SP_Z_KPI_Calculate_Week", new List<SugarParameter> { Plan, Department, Week });

            }
            if (mailobj.Type == "2") {
                var Plan = new SugarParameter("@Plant", mailobj.Plant);
                var Department = new SugarParameter("@Department", mailobj.Depart);
                var Period = new SugarParameter("@Period", mailobj.Period);

                var model = _reportRep.Ado.UseStoredProcedure().SqlQuery<PeriodReportOutput>("SP_Z_KPI_Calculate_Period", new List<SugarParameter> { Plan, Department, Period });

            }

        }

        public List<PeriodReportOutput> PeriodReport(PeriodReportInput input)
        {
            var Plan = new SugarParameter("@Plant", input.Plant);
            var Period = new SugarParameter("@Period", input.Period);
            var Year = new SugarParameter("@Year", input.Year);

            var model = _reportRep.Ado.UseStoredProcedure().SqlQuery<PeriodReportOutput>("SP_Z_KPI_Period_Report", new List<SugarParameter> { Plan, Period, Year });

            return model.Adapt<List<PeriodReportOutput>>();
        }

        public List<WeeklyReportOutput> WeeklyReport(WeeklyReportInput input)
        {
            var Plan = new SugarParameter("@Plant", input.Plant);
            var CurrentYear = new SugarParameter("@CurrentYear", input.CurrentYear);
            var CurrentWeek = new SugarParameter("@CurrentWeek", input.CurrentWeek);

            var model = _reportRep.Ado.UseStoredProcedure().SqlQuery<WeeklyReportOutput>("SP_Z_KPI_Weekly_Operation_Report", new List<SugarParameter> { Plan, CurrentYear, CurrentWeek });

            return model.Adapt<List<WeeklyReportOutput>>();
        }
    }
}
