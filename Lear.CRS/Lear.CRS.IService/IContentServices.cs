using Lear.CRS.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lear.CRS.IServices
{
	/// <summary>
	/// IContentServices
	/// </summary>	
	public interface IContentServices
	{
		Task<long> AddContent(List<ContentInput> input);
		Task<long> UpdateContent(List<UpdateContentInput> input);
		Task<List<ContentOutput>> GetContent(ContentInput input);

		Task<long> AddContentPeriod(List<ContentPeriodInput> input);
		Task<long> UpdateContentPeriod(List<UpdateContentPeriodInput> input);
		Task<List<ContentPeriodOutput>> GetContentPeriod(ContentPeriodInput input);

		Task<long> AddContentWeek(List<ContentWeekInput> input);
		Task<long> UpdateContentWeek(List<UpdateContentWeekInput> input);
		Task<List<ContentWeekOutput>> GetContentWeek(ContentWeekInput input);

		Task<long> AddContentWeekView(List<ContentWeekViewInput> input);
		Task<long> UpdateContentWeekView(List<ContentWeekViewInput> input);
		Task<List<ContentWeekViewOutput>> GetContentWeekView(ContentWeekViewInput input);

		Task<long> AddDailyReport(List<DailyReportInput> input);
		Task<long> UpdateDailyReport(List<DailyReportInput> input);
		Task<List<DailyReportOutput>> GetDailyReport(DailyReportInput input);




		Task<long> AddPlan(List<AddPlanInput> input);
		Task<long> UpdatePlan(List<UpdatePlanInput> input);
		Task<List<PlanOutput>> GetPlan(PlanInput input);



	}
}
