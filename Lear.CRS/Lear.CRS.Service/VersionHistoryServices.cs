
using Lear.CRS.Common.CustomExceptions;
using Lear.CRS.Common.HttpContextUser;
using Lear.CRS.IServices;
using Lear.CRS.Model;
using Lear.CRS.Model.Dto;
using Lear.CRS.SqlSugarCore.Extensions;
using Mapster;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yitter.IdGenerator;

namespace Lear.CRS.Service
{
    /// <summary>
    /// RoleServices
    /// </summary>	
    public class VersionHistoryServices : IVersionHistoryServices
    {
        private readonly ISqlSugarRepository<VersionHistory> _versionHistoryRepository;

        private readonly IUser _user;


        public VersionHistoryServices(ISqlSugarRepository<VersionHistory> versionHistoryRepository,
            IUser user

            )
        {

            _versionHistoryRepository = versionHistoryRepository;
            _user = user;

        }

        public async Task<long> Add(AddVersionHistoryInput input)
        {
            var model = input.Adapt<VersionHistory>();
            model.Id = YitIdHelper.NextId();

            return await _versionHistoryRepository.InsertAsync(model);

        }

        public async Task<List<VersionHistoryOutput>> All()
        {
            var list = await _versionHistoryRepository.Entities
                .OrderBy(c => c.Number)
                              .Select<VersionHistoryOutput>().ToListAsync();

            return list;
        }

        public async Task<bool> Del(DeleteVersionHistoryInput input)
        {



            return await _versionHistoryRepository.DeleteAsync(c => c.Id == input.Id) > 0;
        }

        public async Task<bool> Update(UpdateVersionHistoryInput input)
        {
            var model = _versionHistoryRepository.Single(input.Id);

            if (model == null)
            {
                throw new BusinessException("error");
            }
            var modelNew = input.Adapt<VersionHistory>();
            modelNew.Id = model.Id;

            return await _versionHistoryRepository.UpdateAsync(modelNew) > 0;
        }
    }
}
