using Lear.CRS.Model;
using Lear.CRS.Model.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lear.CRS.IServices
{
    /// <summary>
    /// RoleServices
    /// </summary>	
    public interface IVersionHistoryServices
    {
        Task<List<VersionHistoryOutput>> All();

        Task<long> Add(AddVersionHistoryInput input);


        Task<bool> Del(DeleteVersionHistoryInput input);

        Task<bool> Update(UpdateVersionHistoryInput input);
      

    }
}
