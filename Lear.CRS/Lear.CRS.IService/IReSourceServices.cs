using Lear.CRS.Model;
using Lear.CRS.Model.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lear.CRS.IServices
{
    /// <summary>
    /// RoleServices
    /// </summary>	
    public interface IReSourceServices
    {
        Task<dynamic> Page(QueryResourceInput input);
        Task<List<ResourceOutput>> All();

        Task<long> Add(AddResourceInput input);

        Task<bool> Del(long id);

        Task<bool> Update(UpdateResourceInput input);


    }
}
