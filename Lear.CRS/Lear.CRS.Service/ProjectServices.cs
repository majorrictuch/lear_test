
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
    public class ProjectServices : IProjectServices
    {
        private readonly ISqlSugarRepository<CRS_Item> _itemRep;
        private readonly IUser _user;

        public ProjectServices(ISqlSugarRepository<CRS_Item> itemRep,  IUser user)
        {
            _itemRep = itemRep;
            this._user = user;
        }


        public async Task<long> AddProject(List<ProjectAddInput> input)
        {
            var list = input.Adapt<List<CRS_Item>>();
           
            foreach (var item in list) {
                item.ICreate = DateTime.Now;
                item.ICreateBy = this._user.Name;
            }

            return await _itemRep.InsertAsync(list);
        }


        public async Task<long> UpdateProject(List<ProjectAddInput> input)
        {
            var list = input.Adapt<List<CRS_Item>>();

            var addList = list.Where(x => x.ISeq == 0).ToList();

            long addResult = 0;
            long updateResult = 0;
            if (addList.Count > 0) {
                foreach (var item in addList)
                {
                    item.ICreate = DateTime.Now;
                    item.ICreateBy = this._user.Name;
                }
                addResult = await _itemRep.InsertAsync(list);
            }

            var updateList = list.Where(x => x.ISeq != 0).ToList();
            if (updateList.Count > 0)
            {
                foreach (var item in updateList)
                {
                    item.IUpdate = DateTime.Now;
                    item.IUpdateBy = this._user.Name;
                }
                updateResult = await _itemRep.UpdateAsync(list);
            }
          

            return addResult > updateResult ? addResult : updateResult;
        }

        public async Task<List<ProjectOutput>> GetProject(ProjectInput input)
        {
            var list = await _itemRep.Where(x=>x.ITYPE == input.ReportType && x.IPlant == input.Plant && x.IDepartment == input.Department).ToListAsync();

            return list.Adapt<List<ProjectOutput>>();
        }

      
    }
}
