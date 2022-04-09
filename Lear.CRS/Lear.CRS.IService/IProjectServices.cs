using Lear.CRS.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lear.CRS.IServices
{
	/// <summary>
	/// IProjectServices
	/// </summary>	
	public interface IProjectServices
	{

		Task<long> AddProject(List<ProjectAddInput> input);
		Task<long> UpdateProject(List<ProjectAddInput> input);
		Task<List<ProjectOutput>> GetProject(ProjectInput input);

	}
}
