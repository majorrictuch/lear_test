using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Lear.CRS.SqlSugarCore.Extensions
{
    public static class PagedQueryableExtensions
    {
        /// <summary>
        /// IList转成List<T>
        /// <param name="list"></param>
        /// <returns></returns>
        public static List<long?> IListToList(this List<long> list)
        {
            List<long?> array = new List<long?>();
            foreach (var item in list)
            {
                array.Add((long?)item);
            }
            return array;
        }
        private static Expression RightContainsLeft(this Expression expression, ParameterExpression param, PropertyInfo property, List<long> list)
        {
            if (property != null)
            {
                var left = Expression.Property(param, property);

                if (property.PropertyType == typeof(Nullable<long>))
                {
                    expression = Expression.Call(Expression.Constant(list.IListToList()), typeof(List<Nullable<long>>).GetMethod("Contains", new Type[] { typeof(Nullable<long>) }),
                                left);
                }
                else if (property.PropertyType == typeof(long))
                {
                    expression = Expression.Call(Expression.Constant(list), typeof(List<long>).GetMethod("Contains", new Type[] { typeof(long) }),
                        left);
                }
                else
                {
                    expression = Expression.Call(Expression.Constant(list), typeof(List<string>).GetMethod("Contains", new Type[] { typeof(string) }),
                        left);
                }
            }
            return expression;
        }
        /// <summary>
        /// 数据过滤
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="field">需要过滤的用户id字段,CloseDataFilter关闭过滤</param>
        /// <returns></returns>
        public static async Task<ISugarQueryable<TEntity>> ToDataFilter<TEntity>(this ISugarQueryable<TEntity> entity, string field = "")
        {
            //var _sysUserService = App.GetService<Service.ISysUserService>();
            //var _userManager = App.GetService<IUserManager>();
            var query = entity;
            Expression expression = null;
            var param = Expression.Parameter(typeof(TEntity), "a");
            //var dataScopes = await _sysUserService.GetDataScopeIdUserList(_userManager.UserId);
            //if ( field != "CloseDataFilter")
            //{
            //    if (string.IsNullOrEmpty(field))
            //    {
            //        PropertyInfo property = typeof(TEntity).GetProperty("CreatedUserId");
            //        if (property != null)
            //        {
            //            expression = expression.RightContainsLeft(param, property, dataScopes);
            //        }
            //    }
            //    else
            //    {
            //        PropertyInfo property = typeof(TEntity).GetProperty(field);
            //        if (property != null)
            //        {
            //            expression = expression.RightContainsLeft(param, property, dataScopes);
            //        }
            //    }
            //}
            if (expression != null )
            {
                query = query.Where((Expression<Func<TEntity, bool>>)Expression.Lambda(expression, param));
            }
            return query;
        }

        /// <summary>
        /// 分页拓展
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="field">需要过滤的用户id字段,CloseDataFilter关闭过滤</param>
        /// <returns></returns>
        public static async Task<SqlSugarPagedList<TEntity>> ToPagedListAsync<TEntity>(this ISugarQueryable<TEntity> entity, int pageIndex, int pageSize, string field = "")
        {
            RefAsync<int> totalCount = 0;
            var query = await entity.ToDataFilter(field);
            var items = await query.ToPageListAsync(pageIndex, pageSize, totalCount);
            var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);
            return new SqlSugarPagedList<TEntity>
            {
                PageIndex = pageIndex,
                PageSize = pageSize,
                Items = items,
                TotalCount = (int)totalCount,
                TotalPages = totalPages,
                HasNextPages = pageIndex < totalPages,
                HasPrevPages = pageIndex - 1 > 0
            };
        }
    }
}
