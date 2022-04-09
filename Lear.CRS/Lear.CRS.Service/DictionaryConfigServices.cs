
using Lear.CRS.Common.Helper;
using Lear.CRS.Common.HttpContextUser;
using Lear.CRS.IServices;
using Lear.CRS.Model;
using Lear.CRS.SqlSugarCore.Extensions;
using Mapster;
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
    public class DictionaryConfigServices : IDictionaryConfigServices
    {
        private readonly ISqlSugarRepository<DictionaryConfig> _configRep;
        private readonly ISqlSugarRepository<CRS_Week_VIEW> _weekRep;
        private readonly ISqlSugarRepository<CRS_Period_VIEW> _periodRep;
        private readonly IUser _user;

        public DictionaryConfigServices(ISqlSugarRepository<DictionaryConfig> configRep, ISqlSugarRepository<CRS_Week_VIEW> weekRep, ISqlSugarRepository<CRS_Period_VIEW> periodRep, IUser user)
        {
            _configRep = configRep;
            _weekRep = weekRep;
            _periodRep = periodRep;
            this._user = user;
        }
        /// <summary>
        /// 新增字典表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<long> AddDictionaryConfig(AddDictionaryConfigInput input)
        {
            var model = input.Adapt<DictionaryConfig>();
            model.Id = YitIdHelper.NextId();
            model.CreateTime = DateTime.Now;
            model.CreateBy = this._user.Name;

            return await _configRep.InsertAsync(model);
        }

        /// <summary>
        /// 修改字典表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<long> UpdateDictionaryConfig(UpdateDictionaryConfigInput input)
        {
            var model = _configRep.Single(input.Id);

            model.Code = input.Code;
            model.Value = input.Value;
            model.Description = input.Description;
            model.Enabled = input.Enabled;
            model.Sort = input.Sort;
            model.UpdateBy = this._user.Name;
            model.UpdateTime = DateTime.Now;

            return await _configRep.UpdateAsync(model);
        }

        /// <summary>
        /// 获取字典表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<List<DictionaryConfigOutput>> GetDictionaryConfig(DictionaryConfigInput input)
        {
            var list = await _configRep.Where(x => x.Type == input.Type && x.Enabled == input.Enabled).ToListAsync();

            return list.Adapt<List<DictionaryConfigOutput>>();
        }

        /// <summary>
        /// 获取字典表类别
        /// </summary>
        /// <returns></returns>
        public async Task<List<DictionaryConfigKeyValueOutput>> GetDictionaryConfigKeyValue()
        {
            var list = await _configRep.Where(x => x.Enabled.HasValue && x.Enabled.Value).ToListAsync();

            return (list.GroupBy(x => new { Type = x.Type, TypeName = x.TypeName }).Select(x => new DictionaryConfigKeyValueOutput { Type = x.Key.Type, TypeName = x.Key.TypeName })).ToList();
        }
        public async Task<string> GetDefault(string code)
        {
            var res = await _configRep.FirstOrDefaultAsync(x => x.Enabled.HasValue && x.Enabled.Value && x.Type == "Default" && x.Code == code);

            return res?.Value;
        }

        /// <summary>
        /// 获取年周数字典表类别
        /// </summary>
        /// <returns></returns>
        public async Task<List<DictionaryConfigKeyValueOutput>> GetWeekValue()
        {
            var list = await _weekRep.ToListAsync();

            return (list.GroupBy(x => new { Type = x.DTWKRANGE, TypeName = x.DTYWK, FromDate = x.FromDate, ToDate = x.ToDate }).Select(x => new DictionaryConfigKeyValueOutput { Type = x.Key.Type, TypeName = x.Key.TypeName.ToString(), FromDate = (int?)x.Key.FromDate, ToDate = (int?)x.Key.ToDate })).OrderBy(x => x.TypeName).ToList();
        }

        /// <summary>
        /// 获取年月数字典表类别
        /// </summary>
        /// <returns></returns>
        public async Task<List<DictionaryConfigKeyValueOutput>> GetPeriodValue()
        {
            var list = await _periodRep.ToListAsync();

            return (list.GroupBy(x => new { Type = x.DateRange, TypeName = x.PERIOD, FromDate = x.PStartDate, ToDate = x.PEndDate }).Select(x => new DictionaryConfigKeyValueOutput { Type = x.Key.Type, TypeName = x.Key.TypeName.ToString(), FromDate = x.Key.FromDate, ToDate = x.Key.ToDate })).OrderBy(x => x.TypeName).ToList();
        }


    }
}
