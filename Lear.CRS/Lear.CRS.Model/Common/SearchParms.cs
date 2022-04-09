using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Lear.CRS.Model
{
    public abstract class SearchParms
    {

        private List<Conditions> _conditions = new List<Conditions>();
        /// <summary>
        /// 页码
        /// </summary>
        public int page { get; set; } = 1;
        /// <summary>
        /// 每页大小
        /// </summary>
        public int size { get; set; } = 20;

        /// <summary>
        /// 排序字段(例如:id desc,time asc)
        /// </summary>
        public string orderBy { get; set; } = "create_time desc";
        //public List<Conditions> conditions { get; set; }

        public List<Conditions> GetConditions()
        {
            return _conditions;
        }

        public void AddCondition(Conditions condition)
        {
            _conditions.Add(condition);
        }

    }
    public class SearchParms<T>
    {

        /// <summary>
        /// 页码
        /// </summary>
        public int page { get; set; } = 1;
        /// <summary>
        /// 每页大小
        /// </summary>
        public int size { get; set; } = 20;

        /// <summary>
        /// 排序字段(例如:id desc,time asc)
        /// </summary>
        public string orderBy { get; set; } = "create_time desc";
        public Expression<Func<T, bool>> conditions { get; set; }


    }

}
