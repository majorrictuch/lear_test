
using Lear.CRS.Common;
using Lear.CRS.Common.CustomExceptions;
using Lear.CRS.Common.Helper;
using Lear.CRS.Common.HttpContextUser;
using Lear.CRS.IServices;
using Lear.CRS.Model;
using Lear.CRS.Model.Dto;
using Lear.CRS.SqlSugarCore.Extensions;
using Mapster;
using Microsoft.AspNetCore.Http;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Yitter.IdGenerator;

namespace Lear.CRS.Service
{
    /// <summary>
    /// RoleServices
    /// </summary>	
    public class UserServices : IUserServices
    {
        private readonly ISqlSugarRepository<UserMaster> _userRep;
        private readonly ISqlSugarRepository<UserGroup> _userGroupRep;
        private readonly ISqlSugarRepository<UserRole> _userRoleRep;
        private readonly ISqlSugarRepository<LoginLog> _loginLogRep;
        private readonly IUser _user;
        private readonly IUserRoleServices _userRoleServices;
        private readonly IRoleServices _roleServices;
        private readonly ISourceGroupServices _userGroupServices;
        private readonly IHttpContextAccessor _httpContextAccessor;


        public UserServices(ISqlSugarRepository<UserMaster> userRep, ISqlSugarRepository<UserRole> userRoleRep,
            IHttpContextAccessor httpContextAccessor, ISqlSugarRepository<LoginLog> loginLogRep, IUser user,
            IUserRoleServices userRoleServices, ISourceGroupServices userGroupServices, ISqlSugarRepository<UserGroup> userGroupRep,
           IRoleServices roleServices
            )
        {
            _userRep = userRep;
            this._user = user;
            this._userRoleServices = userRoleServices;
            this._loginLogRep = loginLogRep;
            this._userGroupServices = userGroupServices;
            this._userGroupRep = userGroupRep;
            this._userRoleRep = userRoleRep;
            this._httpContextAccessor = httpContextAccessor;

            this._roleServices = roleServices;
        }

        public async Task<long> Add(AddUserInput input)
        {

            if (input.EmpId.IsNotEmptyOrNull())
            {
                ActiveDirectory ad = new ActiveDirectory();
                var adInfo = ad.GetADUserProfileByEmpId(input.EmpId);
                if (adInfo == null)
                {
                    throw new BusinessException("此员工编号已存在！");
                }
            }
            else
            {
                throw new BusinessException("employeeID编号异常！");
            }



            var result = _userRep.Where(x => x.Account == input.Account || x.EmpId == input.EmpId).Any();
            if (result)
            {
                throw new BusinessException("员工编号或用户名已存在！");
            }



            var model = input.Adapt<UserMaster>();
            model.Id = YitIdHelper.NextId();
            model.CreateTime = DateTime.Now;
            model.CreateBy = _user.Name;
            model.Password = MD5Helper.MD5Encrypt32(model.Id.ToString().Substring(3, 9));
            await _userRep.InsertAsync(model);

            var roleDefaultId = await _roleServices.GetDefaultRoleId();
            var groupDefaultId = await _userGroupServices.GetDefaultGroupId();

            if (roleDefaultId > 0)
            {
                var ur = new UserRole
                {
                    CreateBy = _user.Name,
                    CreateTime = DateTime.Now,
                    Id = YitIdHelper.NextId(),
                    RoleId = roleDefaultId,
                    UserId = model.Id
                };
                await _userRoleRep.InsertAsync(ur);
            }
            else
            {
                throw new BusinessException("未设置默认角色！");
            }


            if (groupDefaultId > 0)
            {
                var ug = new UserGroup
                {
                    CreateBy = _user.Name,
                    CreateTime = DateTime.Now,
                    Id = YitIdHelper.NextId(),
                    GroupId = groupDefaultId,
                    UserId = model.Id
                };
                await _userGroupRep.InsertAsync(ug);
            }






            return model.Id;
        }



        public Task<UserOutput> Get(string userId)
        {
            throw new System.NotImplementedException();
        }

        public async Task<dynamic> GetUsersList(QueryUserInput input)
        {


            var list = await _userRep.Context.Queryable<UserMaster>().OrderBy(x => x.FullName)
                .WhereIF(!string.IsNullOrWhiteSpace(input.Account), m => m.Account.Contains(input.Account.Trim()))
                .WhereIF(!string.IsNullOrWhiteSpace(input.EmpId), m => m.EmpId.Contains(input.EmpId.Trim()))
                .Select<UserOutput>()
                 .ToPagedListAsync(input.PageNo, input.PageSize);
            return list.XnPagedResult();


            //return list.Adapt<PageModel<UserOutput>>();

        }
        public async Task<List<UserOutput>> All(QueryUserAllInput input)
        {
            var list = await _userRep.Where(c => c.Active == input.Active).OrderBy(x => x.FullName)
                 .WhereIF(!string.IsNullOrWhiteSpace(input.Account), m => m.Account.Contains(input.Account.Trim()))
                 .WhereIF(!string.IsNullOrWhiteSpace(input.FullName), m => m.FullName.Contains(input.FullName.Trim()))
                .WhereIF(!string.IsNullOrWhiteSpace(input.EmpId), m => m.EmpId.Contains(input.EmpId.Trim()))
                .Select<UserOutput>().ToListAsync();

            return list.Adapt<List<UserOutput>>();

        }
        public async Task<UserOutput> Login(string userName, string passWord)
        {
            var userModel = await _userRep.FirstOrDefaultAsync(c => c.Account.Equals(userName) && c.Password == passWord);

            if (userModel == null)
            {
                throw new BusinessException("The user account is not found!");
            }

            if (userModel.Active == 0)
            {
                throw new BusinessException("The user account is inactive!");
            }
            DateTime date = DateTime.Now;
            userModel.LastLoginTime = date;

            var res = await _userRep.UpdateAsync(userModel);


            LoginLog log = new LoginLog()
            {
                CreateTime = date,
                IP = _httpContextAccessor.HttpContext?.Connection.RemoteIpAddress.ToString(),
                UserId = userModel.Id,
                Id = YitIdHelper.NextId()
            };

            await _loginLogRep.InsertAsync(log);
            return userModel?.Adapt<UserOutput>();
        }
        public async Task<UserOutput> Login(string userName)
        {
            var userModel = await _userRep.FirstOrDefaultAsync(c => c.Account.Equals(userName));

            if (userModel == null)
            {
                throw new BusinessException("The user account is not found!");
            }

            if (userModel.Active == 0)
            {
                throw new BusinessException("The user account is inactive!");
            }
            DateTime date = DateTime.Now;
            userModel.LastLoginTime = date;

            var res = await _userRep.UpdateAsync(userModel);


            LoginLog log = new LoginLog()
            {
                CreateTime = date,
                IP = _httpContextAccessor.HttpContext?.Connection.RemoteIpAddress.ToString(),
                UserId = userModel.Id,
                Id = YitIdHelper.NextId()
            };

            await _loginLogRep.InsertAsync(log);
            return userModel?.Adapt<UserOutput>();
        }
        public async Task<bool> Update(UpdateUserInput input)
        {
            if (string.IsNullOrEmpty(input.EmpId))
            {
                throw new BusinessException("The empId is error");
            }
            var model = _userRep.Single(input.Id);


            if (await _userRep.AnyAsync(c => c.Id != input.Id && c.EmpId == input.EmpId))
            {
                throw new BusinessException("The empId is exist");

            }


            model.Email = input.Email;
            model.FirstName = input.FirstName;
            model.LastName = input.LastName;
            model.FullName = input.FullName;
            model.UpdateBy = this._user.Name;
            model.UpdateTime = DateTime.Now;
            model.Active = input.Active;
            model.EmpId = input.EmpId;

            if (model.Active == 0)
            {
                await _userGroupRep.DeleteAsync(c => c.UserId == model.Id);
                await _userRoleRep.DeleteAsync(c => c.UserId == model.Id);
            }
            return await _userRep.UpdateAsync(model) > 0;

        }




        /// <summary>
        /// 获取用户拥有角色
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<List<long>> GetUserOwnRole(long userId)
        {
            return await _userRoleServices.GetUserRoleIdList(userId);
        }

        /// <summary>
        /// 获取用户拥有数据
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<List<long>> GetUserOwnData(long userId)
        {
            return await _userGroupServices.GetUserDataScopeIdList(userId);
        }

        public async Task AdSync(bool isAuto)
        {
            ActiveDirectory ad = new ActiveDirectory();


            var userList = await _userRep.Where(c => c.Active == 1).ToListAsync();

            foreach (var item in userList)
            {
                var adUser = await FindInAd(item.Account);
                if (adUser == null || adUser.Active == 0)
                {
                    item.Active = 0;
                    item.LastSyncTime = DateTime.Now;
                    item.UpdateTime = DateTime.Now;
                    item.UpdateBy = isAuto ? "Auto" : _user.Name;
                    await _userRoleRep.DeleteAsync(c => c.UserId == item.Id);
                    await _userGroupRep.DeleteAsync(c => c.UserId == item.Id);
                    await _userRep.UpdateAsync(item);
                }
            }

        }



        public async Task<ADUserProfileOutput> FindInAd(string name)
        {
            ActiveDirectory ad = new ActiveDirectory();
            var adInfo = ad.GetADUserProfileByLoginName(name);
            if (adInfo != null)
            {
                ADUserProfileOutput res = new ADUserProfileOutput()
                {
                    EmpId = adInfo.employeeID,
                    Account = adInfo.samAccountName,
                    FirstName = adInfo.givenName,
                    LastName = adInfo.sn,
                    Email = adInfo.mail,
                    FullName = adInfo.displayName,
                    Active = adInfo.ActiveUser ? 1 : 0

                };
                return res;
            }
            return null;



        }
    }
}
