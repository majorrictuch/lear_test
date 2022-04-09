
using Lear.CRS.Common.HttpContextUser;
using Lear.CRS.IServices;
using Lear.CRS.Model;
using Mapster;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Yitter.IdGenerator;

namespace Lear.CRS.Services
{
    /// <summary>
    /// ModuleServices
    /// </summary>	
    public class ModuleServices : IModuleServices
    {
        private readonly ISqlSugarRepository<ModuleMaster> _moduleRepository;
        private readonly IUser _user;

        public ModuleServices(ISqlSugarRepository<ModuleMaster> moduleRepository, IUser user)
        {

            this._moduleRepository = moduleRepository;
            this._user = user;
        }
        public async Task<long> Add(AddModuleInput input)
        {

            var module = input.Adapt<ModuleMaster>();

            //module.Create();

            return await _moduleRepository.InsertAsync(module);
        }

        public async Task<bool> DelById(long id)
        {


            return (await _moduleRepository.DeleteAsync(c => c.Id == id)) > 0;
        }

        public async Task<ModuleOutput> Get(long id)
        {
            var model = await _moduleRepository.FirstOrDefaultAsync(c => c.Id == id);

            return model.Adapt<ModuleOutput>();
        }
        public async Task<List<ModuleOutput>> GetByParentId(long parentId)
        {
            var list = await _moduleRepository.Where(c => c.ParentId == parentId).ToListAsync();

            return list.Adapt<List<ModuleOutput>>();
        }
        public async Task<List<ModuleOutput>> GetMoudulePageList(QueryModuleInput query)
        {

            var mouduleList = await _moduleRepository.ToListAsync();

            return mouduleList.Adapt<List<ModuleOutput>>();
        }

        public async Task<bool> Update(UpdateModuleInput input)
        {
            var model = input.Adapt<ModuleMaster>();
            model.UpdateTime = DateTime.Now;
            model.UpdateBy = this._user.Name;
            return (await _moduleRepository.UpdateAsync(model)) > 0;
        }
    }
}
