using Lear.CRS.Model;
using Lear.CRS.Model.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lear.CRS.IServices
{	
	/// <summary>
	/// RoleServices
	/// </summary>	
    public interface IUserRoleServices
	{
		Task<List<long>> GetRoleIdsByUserId(long id);
		Task<List<RoleMaster>> GetRoleByUserId(long userId);
		Task<bool> BindToUser(BindToUserInput input);
		Task<bool> BindToRole(BindToRoleInput input);
		Task<List<long>> GetUserIdsByRoleId(long rid);

		Task<List<long>> GetUserRoleIdList(long userId);
		Task<AssessExcelDto> ExportData();


	}
}
