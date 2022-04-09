
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
    public class ContentServices : IContentServices
    {
        private readonly ISqlSugarRepository<CRS_Content> _contentRep;
        private readonly ISqlSugarRepository<CRS_Content_Period> _contentPeriodRep;
        private readonly ISqlSugarRepository<CRS_Content_Week> _contentWeekRep;
        private readonly ISqlSugarRepository<CRS_Content_Week_View> _contentWeekViewRep;
        private readonly ISqlSugarRepository<CRS_Daily_Meeting_Report> _dailyReportRep;
        private readonly ISqlSugarRepository<CRS_Plan> _planRep;
        private readonly ISqlSugarRepository<CRS_Item> _itemRep;
        private readonly ISqlSugarRepository<UserMaster> _masterRep;

        private readonly IUser _user;

        public ContentServices(ISqlSugarRepository<CRS_Content> contentRep,
            ISqlSugarRepository<CRS_Content_Period> contentPeriodRep, 
            ISqlSugarRepository<CRS_Content_Week> contentWeekRep, 
            ISqlSugarRepository<CRS_Content_Week_View>  contentWeekViewRep,
            ISqlSugarRepository<CRS_Daily_Meeting_Report> dailyReportRep, 
            ISqlSugarRepository<CRS_Plan> planRep, 
            ISqlSugarRepository<CRS_Item> itemRep,
            ISqlSugarRepository<UserMaster> masterRep,
        IUser user)
        {
            _contentRep = contentRep;
            _contentPeriodRep = contentPeriodRep;
            _contentWeekRep = contentWeekRep;
            _contentWeekViewRep = contentWeekViewRep;
            _dailyReportRep = dailyReportRep;
            _planRep = planRep;
            _itemRep = itemRep;
            _masterRep = masterRep;
            this._user = user;
        }

        public async Task<long> AddContent(List<ContentInput> input)
        {
            var model = input.Adapt<List<CRS_Content>>();
            return await _contentRep.InsertAsync(model);
        }
        public async Task<long> UpdateContent(List<UpdateContentInput> input)
        {
            var list = input.Adapt<List<CRS_Content>>();
            List<CRS_Content> reslist = new List<CRS_Content>();
            List<CRS_Content> resAddlist = new List<CRS_Content>();
            foreach (var item in list)
            {
                var it = _contentRep.Where(x => x.CSerial == item.CSerial).First();
                if (it != null)
                {
                    it.CUpdate = DateTime.Now;
                    it.CUpdateBy = this._user.Name;

                    it.CID = item.CID;
                    it.CContent = item.CContent;
                    it.CCategory = item.CCategory;
                    it.CRemark = item.CRemark;
                    it.CDesc = item.CDesc;
                    reslist.Add(it);
                }
                else {
                    var model = new CRS_Content()
                    {
                        CID = item.CID,
                        CContent = item.CContent,
                        CCategory = item.CCategory,
                        CRemark = item.CRemark,
                        CDesc = item.CDesc,
                        CCalculate = item.CCalculate,
                        CCompare = item.CCompare,
                        CContentPlan = item.CContentPlan,
                        CDate = item.CDate,
                        CDepartment = item.CDepartment,
                        CFormat = item.CFormat,
                        CHead = item.CHead,
                        CPlant = item.CPlant,
                        CPriority = item.CPriority,
                        CSeq = item.CSeq,
                        CSort = item.CSort,
                        CType = item.CType,
                        CUpdate = DateTime.Now,
                        CUpdateBy = this._user.Name,
                        CCreate = DateTime.Now,
                        CCreateBy = this._user.Name
                    };
                    resAddlist.Add(model);
                }
            }
            long updateResult = 0;
            long addResult = 0;
            if (reslist.Count > 0)
            {
                updateResult = await _contentRep.UpdateAsync(reslist);
            }
            if (resAddlist.Count > 0)
            {
                addResult = await _contentRep.InsertAsync(resAddlist);
            }
            var baseInput = new BaseInput()
            {
                Type="0",
                Depart= input[0].CDepartment,
                Plant= input[0].CPlant,
                Date= input[0].CDate
            };

            var baseS = new BaseServices(_masterRep, _user);
            baseS.SendViaThread(baseInput);

            return updateResult > 0 ? updateResult : addResult;
        }

        public async Task<List<ContentOutput>> GetContent(ContentInput input)
        {
            var list = await _contentRep.Where(x=>x.CType == input.CType && x.CDate == input.CDate && x.CPlant == input.CPlant && (x.CDepartment == input.CDepartment || input.CDepartment == "")).ToListAsync();

            var itemContent = list.Select(x=>x.CSeq).ToList();

            var itemList = await _itemRep.Where(x => x.IID == "IA" && x.ITYPE == input.CType && x.IPlant == input.CPlant && (x.IDepartment == input.CDepartment || input.CDepartment == "") && !itemContent.Contains(x.ISeq.ToString())).ToListAsync();

            var itemResult = (from a in itemList
                              select new ContentOutput {
                                  CID = a.IID == "IA" ? "CA" : "CZ",
                                  CType = a.ITYPE,
                                  CDate = input.CDate,
                                  CSeq = a.ISeq.ToString(),
                                  CSort = a.ISort,
                                  CDesc = a.IDesc,
                                  CPlant = a.IPlant,
                                  CDepartment = a.IDepartment,
                                  CHead = a.IHead,
                                  CPriority = a.IPriority,
                                  CCategory = a.ICategory,
                                  CFormat = a.IFormat,
                                  CCompare = a.ICompare,
                                  CCalculate = a.Icalculate
                                 } ).ToList();

            List<ContentOutput> result = list.Adapt<List<ContentOutput>>();
            result.AddRange(itemResult);

            return result;
        }



        public async Task<long> AddContentPeriod(List<ContentPeriodInput> input)
        {
            var model = input.Adapt<List<CRS_Content_Period>>();
            return await _contentPeriodRep.InsertAsync(model);
        }

        public async Task<long> UpdateContentPeriod(List<UpdateContentPeriodInput> input)
        {
            var list = input.Adapt<List<CRS_Content_Period>>();

            List<CRS_Content_Period> reslist = new List<CRS_Content_Period>();
            List<CRS_Content_Period> resAddlist = new List<CRS_Content_Period>();
            foreach (var item in list)
            {
                var it = _contentPeriodRep.Where(x => x.PSerial == item.PSerial).First();
                if (it != null)
                {
                    it.PUpdate = DateTime.Now;
                    it.PUpdateBy = this._user.Name;
                       
                    it.PID = item.PID;
                    it.PContentPlan = item.PContentPlan;
                    it.PContent = item.PContent;
                    it.PCategory = item.PCategory;
                    it.PRemark = item.PRemark;
                    it.PDesc = item.PDesc;
                    reslist.Add(it);
                }
                else
                {
                    var model = new CRS_Content_Period()
                    {
                        PID = item.PID,
                        PContent = item.PContent,
                        PCategory = item.PCategory,
                        PRemark = item.PRemark,
                        PDesc = item.PDesc,
                        PCalculate = item.PCalculate,
                        PCompare = item.PCompare,
                        PContentPlan = item.PContentPlan,
                        PDepartment = item.PDepartment,
                        PFormat = item.PFormat,
                        PHead = item.PHead,
                        PPlant = item.PPlant,
                        PPriority = item.PPriority,
                        PSeq = item.PSeq,
                        PSort = item.PSort,
                        PType = item.PType,
                        PUpdate = DateTime.Now,
                        PUpdateBy = this._user.Name,
                        PPeriod = item.PPeriod,
                        PCreate = DateTime.Now,
                        PCreateBy = this._user.Name,
                        PStatus = item.PStatus

                    };
                    resAddlist.Add(model);
                }
            }
            long updateResult = 0;
            long addResult = 0;
            if (reslist.Count > 0)
            {
                updateResult = await _contentPeriodRep.UpdateAsync(reslist);
            }
            if (resAddlist.Count > 0)
            {
                addResult = await _contentPeriodRep.InsertAsync(resAddlist);
            }
            var baseInput = new BaseInput()
            {
                Type = "2",
                Depart = input[0].PDepartment,
                Plant = input[0].PPlant,
                Period = input[0].PPeriod
            };

            var baseS = new BaseServices(_masterRep, _user);
            baseS.SendViaThread(baseInput);

            return updateResult > 0 ? updateResult : addResult;

        }

        public async Task<List<ContentPeriodOutput>> GetContentPeriod(ContentPeriodInput input)
        {
            var list = await _contentPeriodRep.Where(x=> x.PType == input.PType && x.PPeriod == input.PYearPeriod && x.PPlant == input.PPlant && (x.PDepartment == input.PDepartment || input.PDepartment == "")).ToListAsync();

            var itemContent = list.Select(x => x.PSeq).ToList();

            var itemList = await _itemRep.Where(x => x.IID == "IA" && x.ITYPE == "T03" && x.ITYPE == input.PType && x.IPlant == input.PPlant && (x.IDepartment == input.PDepartment || input.PDepartment == "") && !itemContent.Contains(x.ISeq.ToString())).ToListAsync();

            var itemResult = (from a in itemList
                              select new ContentPeriodOutput
                              {
                                  PID = a.IID == "IA" ? "PA" :"PZ",
                                  PType = a.ITYPE,
                                  PPeriod = input.PYearPeriod,
                                  PSeq = a.ISeq.ToString(),
                                  PSort = a.ISort,
                                  PDesc = a.IDesc,
                                  PPlant = a.IPlant,
                                  PDepartment = a.IDepartment,
                                  PHead = a.IHead,
                                  PPriority = a.IPriority,
                                  PCategory = a.ICategory,
                                  PFormat = a.IFormat,
                                  PCompare = a.ICompare,
                                  PCalculate = a.Icalculate,
                                  PStatus = a.IPriority == "Status" ? "ON GOING" : null,
                                  PContent = a.IPriority == "Status" ? "ON GOING" : "",
                                  PContentPlan = ""

                              }).ToList();

            List<ContentPeriodOutput> result = list.Adapt<List<ContentPeriodOutput>>();
            result.AddRange(itemResult);

            return result;
        }



        public async Task<long> AddContentWeek(List<ContentWeekInput> input)
        {
            var model = input.Adapt<List<CRS_Content_Week>>();
            return await _contentWeekRep.InsertAsync(model);
        }
        public async Task<long> UpdateContentWeek(List<UpdateContentWeekInput> input)
        {
            var list = input.Adapt<List<CRS_Content_Week>>();

            List<CRS_Content_Week> reslist = new List<CRS_Content_Week>();
            List<CRS_Content_Week> resAddlist = new List<CRS_Content_Week>();
            foreach (var item in list)
            {
                var it = _contentWeekRep.Where(x => x.WSerial == item.WSerial).First();
                if (it != null)
                {
                    it.WUpdate = DateTime.Now;
                    it.WUpdateBy = this._user.Name;

                    it.WID = item.WID;
                    it.WContent = item.WContent;
                    it.WContentPlan = item.WContentPlan;
                    it.WCategory = item.WCategory;
                    it.WRemark = item.WRemark;
                    it.WDesc = item.WDesc;
                    reslist.Add(it);
                }
                else
                {
                    var model = new CRS_Content_Week()
                    {
                        WID = item.WID,
                        WContent = item.WContent,
                        WCategory = item.WCategory,
                        WRemark = item.WRemark,
                        WDesc = item.WDesc,
                        WCalculate = item.WCalculate,
                        WCompare = item.WCompare,
                        WContentPlan = item.WContentPlan,
                        WDepartment = item.WDepartment,
                        WFormat = item.WFormat,
                        WHead = item.WHead,
                        WPlant = item.WPlant,
                        WPriority = item.WPriority,
                        WSeq = item.WSeq,
                        WSort = item.WSort,
                        WType = item.WType,
                        WUpdate = DateTime.Now,
                        WUpdateBy = this._user.Name,
                        WCreate = DateTime.Now,
                        WCreateBy = this._user.Name,
                        WWeek = item.WWeek

                    };
                    resAddlist.Add(model);
                }
            }

            long updateResult = 0;
            long addResult = 0;
            if (reslist.Count > 0)
            {
                updateResult = await _contentWeekRep.UpdateAsync(reslist);
            }
            if (resAddlist.Count > 0)
            {
                addResult = await _contentWeekRep.InsertAsync(resAddlist);
            }
            var baseInput = new BaseInput()
            {
                Type = "1",
                Depart = input[0].WDepartment,
                Plant = input[0].WPlant,
                Week = input[0].WWeek
            };

            var baseS = new BaseServices(_masterRep, _user);
            baseS.SendViaThread(baseInput);

            return updateResult > 0 ? updateResult : addResult;
        }

        public async Task<List<ContentWeekOutput>> GetContentWeek(ContentWeekInput input)
        {
            var list = await _contentWeekRep.Where(x=>x.WType == input.WType && x.WWeek == input.WYearWeek && x.WPlant == input.WPlant && x.WDepartment == input.WDepartment).ToListAsync();

            var itemContent = list.Select(x => x.WSeq).ToList();

            var itemList = await _itemRep.Where(x => x.IID == "IA" && ((x.ITYPE == "T01" && x.IStatType == "Manual") || x.ITYPE == "T02") && x.ITYPE == input.WType && x.IPlant == input.WPlant && (x.IDepartment == input.WDepartment || input.WDepartment == "") && !itemContent.Contains(x.ISeq.ToString())).ToListAsync();

            var itemResult = (from a in itemList
                              select new ContentWeekOutput
                              {
                                  WID = a.IID == "IA" ? "WA" : "WZ",
                                  WType = a.ITYPE,
                                  WSeq = a.ISeq.ToString(),
                                  WSort = a.ISort,
                                  WDesc = a.IDesc,
                                  WPlant =a.IPlant,
                                  WWeek = input.WYearWeek,
                                  WDepartment = a.IDepartment,
                                  WHead = a.IHead,
                                  WPriority =a.IPriority,
                                  WCategory = a.ICategory,
                                  WFormat = a.IFormat,
                                  WCompare = a.ICompare,
                                  WCalculate = a.Icalculate,
                                  WContentPlan = a.ITYPE == "T02" ? (a.IPlanFlag.HasValue ? (a.IPlanFlag.Value ? "1" : "0" ) : null) : ""
                              }).ToList();

            List<ContentWeekOutput> result = list.Adapt<List<ContentWeekOutput>>();
            result.AddRange(itemResult);

            return result;
        }


        public async Task<long> AddContentWeekView(List<ContentWeekViewInput> input)
        {
            var model = input.Adapt<List<CRS_Content_Week_View>>();
            return await _contentWeekViewRep.InsertAsync(model);
        }
        public async Task<long> UpdateContentWeekView(List<ContentWeekViewInput> input)
        {
            var model = input.Adapt<List<CRS_Content_Week_View>>();
            return await _contentWeekViewRep.UpdateAsync(model);
        }

        public async Task<List<ContentWeekViewOutput>> GetContentWeekView(ContentWeekViewInput input)
        {
            var list = await _contentWeekViewRep.ToListAsync();

            return list.Adapt<List<ContentWeekViewOutput>>();
        }

        public async Task<long> AddDailyReport(List<DailyReportInput> input)
        {
            var model = input.Adapt<List<CRS_Daily_Meeting_Report>>();
            return await _dailyReportRep.InsertAsync(model);
        }
        public async Task<long> UpdateDailyReport(List<DailyReportInput> input)
        {
            var model = input.Adapt<List<CRS_Daily_Meeting_Report>>();
            return await _dailyReportRep.UpdateAsync(model);
        }

        public async Task<List<DailyReportOutput>> GetDailyReport(DailyReportInput input)
        {
            var list = await _dailyReportRep.ToListAsync();
            return list.Adapt<List<DailyReportOutput>>();
        }


        public async Task<long> AddPlan(List<AddPlanInput> input)
        {
            var list = input.Adapt<List<CRS_Plan>>();

            foreach (var item in list) {
                item.PCreate = DateTime.Now;
                item.PCreateBy = this._user.Name;
            }


            return await _planRep.InsertAsync(list);
        }

        public async Task<long> UpdatePlan(List<UpdatePlanInput> input)
        {
            var list = input.Adapt<List<CRS_Plan>>();

            List<CRS_Plan> reslist = new List<CRS_Plan>();
            List<CRS_Plan> resAddlist = new List<CRS_Plan>();
            foreach (var item in list)
            {
                var it = _planRep.Where(x => x.PSerial == item.PSerial).First();
                if (it != null)
                {
                    it.PUpdate = DateTime.Now;
                    it.PUpdateBy = this._user.Name;

                    it.PID = item.PID;
                    it.PFromDate = item.PFromDate;
                    it.PToDate = item.PToDate;
                    it.PCategory = item.PCategory;
                    it.PDesc = item.PDesc;
                    it.PContent = item.PContent;
                    reslist.Add(it);
                }
                else {
                    var model = new CRS_Plan()
                    {
                        PID = item.PID,
                        PFromDate = item.PFromDate,
                        PToDate = item.PToDate,
                        PDesc = item.PDesc,
                        PCategory = item.PCategory,
                        PPlant = item.PPlant,
                        PContent = item.PContent,
                       
                        PFormat = item.PFormat,
                        PType = item.PType,
                        PSort = item.PSort,
                        PSeq = item.PSeq,
                        PCreate = DateTime.Now,
                        PCreateBy = this._user.Name

                    };

                    resAddlist.Add(model);
                }
            }

            long updateResult = 0;
            long addResult = 0;
            if (reslist.Count > 0) {
                updateResult = await _planRep.UpdateAsync(reslist);
            }
            if (resAddlist.Count > 0) {
                addResult = await _planRep.InsertAsync(resAddlist);
            }
            

            return updateResult > 0 ? updateResult : addResult;
        }

        public async Task<List<PlanOutput>> GetPlan(PlanInput input)
        {

            var projectlist = await _itemRep.Where(x => x.IID == "IA" && x.ITYPE == input.PType && x.IPlant == input.PPlant && (x.IDepartment == input.Department || input.Department == "")).ToListAsync();

            var list = await _planRep.Where(x => ((Convert.ToDateTime(x.PFromDate) >= Convert.ToDateTime(input.PFromDate) && Convert.ToDateTime(x.PFromDate) <= Convert.ToDateTime(input.PToDate))
                                               || Convert.ToDateTime(x.PToDate) >= Convert.ToDateTime(input.PFromDate) && Convert.ToDateTime(x.PToDate) >= Convert.ToDateTime(input.PToDate))
                                               ).ToListAsync();
            var resultlist = (
                              from p in list
                              join s in projectlist on p.PSeq equals s.ISeq.ToString()
                              select new PlanOutput
                              {
                                  PSerial = p.PSerial,
                                  PID = p.PID,
                                  PSeq = p.PSeq,
                                  PCategory = p.PCategory,
                                  PPlant = p.PPlant,
                                  Department = s.IDepartment,
                                  PType = p.PType,
                                  PFormat = p.PFormat,
                                  PContent = p.PContent,
                                  PDesc = p.PDesc,
                                  PFromDate = p.PFromDate,
                                  PToDate = p.PToDate,
                                  PCreate = p.PCreate,
                                  PCreateBy = p.PCreateBy,
                                  PUpdate = p.PUpdate,
                                  PUpdateBy = p.PUpdateBy
                              }).ToList();


            var itemContent = list.Where(x => x.PID == "PA" && x.PType == input.PType).Select(x => x.PSeq).ToList();

            var itemList = await _itemRep.Where(x => x.IID == "IA" && x.ITYPE == input.PType && x.IPlant == input.PPlant && (x.IDepartment == input.Department || input.Department == "") && (x.IPlanFlag == input.PLantFlage || input.PLantFlage == false) && !itemContent.Contains(x.ISeq.ToString())).ToListAsync();



             resultlist.AddRange((
                              from p in itemList
                              select new PlanOutput
                              {
                                  PID = p.IID == "IA" ? "PA" : "PZ",
                                  PSeq = p.ISeq.ToString(),
                                  PCategory = p.ICategory,
                                  PPlant = p.IPlant,
                                  Department = p.IDepartment,
                                  PType = p.ITYPE,
                                  PFormat = p.IFormat,
                                  PDesc = p.IDesc,
                                  PFromDate = input.PFromDate,
                                  PToDate = input.PToDate,
                                  PCreate = p.ICreate,
                                  PCreateBy = p.ICreateBy,
                                  PUpdate = p.IUpdate,
                                  PUpdateBy = p.IUpdateBy
                              }).ToList());
            return resultlist;
        }
    }
}
