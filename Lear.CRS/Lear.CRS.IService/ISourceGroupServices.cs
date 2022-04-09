using Lear.CRS.Model;
using Lear.CRS.Model.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lear.CRS.IServices
{	
	/// <summary>
	/// RoleServices
	/// </summary>	
    public interface ISourceGroupServices
	{
		/// <summary>
		/// returns query page list
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		Task<dynamic> Page(QuerySourceGroupInput input);
		/// <summary>
		/// returns all list
		/// </summary>
		/// <returns></returns>
		Task<List<SourceGroupOutput>> All(QuerySourceGroupInput input);
		/// <summary>
		/// add new group
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>

		Task<long> Add(AddSourceGroupInput input);

		/// <summary>
		/// delete group by id
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>

		Task<bool> Del(long id);
		/// <summary>
		/// update 
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>

		Task<bool> Update(UpdateSourceGroupInput input);
		/// <summary>
		/// export to excel
		/// </summary>
		/// <returns></returns>

		Task<AssessExcelDto> ExportData();
		Task<List<string>> GetGroupIdsByUserId(long id);
		Task<List<GroupMaster>> GetGroupByUserId(long userId);


		Task<List<long>> GetUserIdsByGroupId(long gid);
		Task<List<long>> GetUserDataScopeIdList(long userId);
		/// <summary>
		/// get default group id
		/// </summary>
		/// <param name="code"></param>
		/// <returns></returns>
		Task<long> GetDefaultGroupId();
	}
}
