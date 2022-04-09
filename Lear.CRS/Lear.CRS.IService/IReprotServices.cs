using Lear.CRS.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lear.CRS.IServices
{
	/// <summary>
	/// IReprotServices
	/// </summary>	
	public interface IReprotServices
	{
		List<PeriodReportOutput> PeriodReport(PeriodReportInput input);
		List<WeeklyReportOutput> WeeklyReport(WeeklyReportInput input);

	}
}
