
using Lear.CRS.Model;
using Lear.CRS.Model.Permission;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lear.CRS.IServices
{
    public partial interface IMenuServices
    {


        Task<List<MenuOutput>> All();
        Task<long> Add(AddMenuInput input);
        Task<bool> Update(UpdateMenuInput input);

        Task<bool> Delete(long menuId);

        Task<List<MenuTreeOutput>> TreeList();

    }
}