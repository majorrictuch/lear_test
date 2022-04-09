

using Lear.CRS.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lear.CRS.IServices
{	
	/// <summary>
	/// ModuleServices
	/// </summary>	
    public interface IModuleServices 
	{
		Task<long> Add(AddModuleInput input);

		Task<bool> Update(UpdateModuleInput input);

		Task<bool> DelById(long id);

		Task<ModuleOutput> Get(long id);
		Task<List<ModuleOutput>> GetMoudulePageList(QueryModuleInput query);

		Task<List<ModuleOutput>> GetByParentId(long parentId);
	}
}
