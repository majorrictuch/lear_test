using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lear.CRS.SqlSugarCore.Extensions
{
    public class PageResult<T>
    {
        public int PageNo { get; set; }
        public int PageSize { get; set; }
        public int TotalPage { get; set; }
        public int TotalRows { get; set; }
        public ICollection<T> Rows { get; set; }
    }


    public static class XnPageResult
    {
        // <summary>
        /// 替换sqlsugar分页
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        public static dynamic XnPagedResult<T>(this SqlSugarPagedList<T> page)
        {
            return new
            {
                PageNo = page.PageIndex,
                PageSize = page.PageSize,
                TotalPage = page.TotalPages,
                TotalRows = page.TotalCount,
                Rows = page.Items
            };
        }
    }
}
