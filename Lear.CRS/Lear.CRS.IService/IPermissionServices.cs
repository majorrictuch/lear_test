
using Lear.CRS.Model;
using Lear.CRS.Model.Dto;
using Lear.CRS.Model.Permission;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lear.CRS.IServices
{
    public partial interface IPermissionServices
    {


        Task<List<MenuCurrentOutput>> GetMenus();
        Task<List<PlanDepartmentOutput>> GetPlanDepartment();

        Task<bool> Assign(AssignInput assign);
        Task<List<long>> GetAssignByRole(long roleId);
        Task<List<long>> GetResourceByGroup(long groupId);
        Task<bool> UserBind(UserBindInput input);


    }
}