
using Lear.CRS.Common.Helper;
using Lear.CRS.Common.HttpContextUser;
using Lear.CRS.IServices;
using Lear.CRS.Model;
using Lear.CRS.SqlSugarCore.Extensions;
using Mapster;
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
    public class ReportServices : IReprotServices
    {
        private readonly ISqlSugarRepository<UserMaster> _reportRep;
        private readonly IUser _user;

        public ReportServices(ISqlSugarRepository<UserMaster> reportRep, IUser user)
        {
            _reportRep = reportRep;
            this._user = user;
        }

        public List<PeriodReportOutput> PeriodReport(PeriodReportInput input)
        {
            var Plan = new SugarParameter("@Plant", input.Plant);
            var Period = new SugarParameter("@Period",input.Period);
            var Year = new SugarParameter("@Year",input.Year);

            var model= _reportRep.Ado.UseStoredProcedure().SqlQuery<PeriodReportOutput>("SP_Z_KPI_Period_Report", new List<SugarParameter> { Plan, Period, Year });

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
