using Lear.CRS.Model;
using Lear.CRS.Model.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lear.CRS.IServices
{
    /// <summary>
    /// RoleServices
    /// </summary>	
    public interface IRoleServices
    {
        Task<List<RoleOutput>> GetRoleList(QueryRoleInput queryRoleInput);

        Task<long> Add(AddRoleInput input);


        Task<bool> Del(long roleId);

        Task<bool> Update(UpdateRoleInput input);
        Task<dynamic> Page(QueryRoleInput query);

        Task<AssessExcelDto> ExportData();

        Task<long> GetDefaultRoleId();
    }
}
