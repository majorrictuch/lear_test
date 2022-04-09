using Lear.CRS.Common;
using Lear.CRS.Model;
using Lear.CRS.Model.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lear.CRS.IServices
{
    /// <summary>
    /// RoleServices
    /// </summary>	
    public interface IUserServices
    {
        Task<UserOutput> Login(string userName, string passWord);
        Task<UserOutput> Login(string userName);
        Task<long> Add(AddUserInput input);
        Task<bool> Update(UpdateUserInput input);
        Task<UserOutput> Get(string userId);
        Task<dynamic> GetUsersList(QueryUserInput input);

        Task<List<UserOutput>> All(QueryUserAllInput input);
        Task<List<long>> GetUserOwnRole(long userId);
        Task<List<long>> GetUserOwnData(long userId);


        Task AdSync(bool isAuto);

        Task<ADUserProfileOutput> FindInAd(string name);


    }
}
