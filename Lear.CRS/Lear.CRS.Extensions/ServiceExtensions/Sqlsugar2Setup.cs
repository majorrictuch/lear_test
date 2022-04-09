using Lear.CRS.Common;
using Lear.CRS.Common.DB;
using Lear.CRS.Common.Helper;
using Lear.CRS.Common.LogHelper;
using Lear.CRS.Model;
using Microsoft.Extensions.DependencyInjection;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lear.CRS.Extensions
{
    /// <summary>
    /// SqlSugar 启动服务
    /// </summary>
    public static class Sqlsugar2Setup
    {
        public static void AddSqlsugar2Setup(this IServiceCollection services)
        {
            #region 配置sqlsuagr
            List<ConnectionConfig> connectConfigList = new List<ConnectionConfig>();
            //数据库序号从0开始,默认数据库为0
          //  _cfg.GetSection("Mysql").Get<MysqlConfig>()
            var config = Appsettings.app(new string[] { "ConnectionStrings", "DefaultDbString" });


            //默认数据库
            connectConfigList.Add(new ConnectionConfig
            {
                ConnectionString = config,
                DbType = (DbType)Convert.ToInt32(Enum.Parse(typeof(DbType), "1")),
                IsAutoCloseConnection = true,
                ConfigId = "AuthUser",
                InitKeyType = InitKeyType.Attribute
            });
            //业务数据库集合
            //foreach (var item in config.DbConfigs)
            //{
            //    connectConfigList.Add(new ConnectionConfig
            //    {
            //        ConnectionString = item.DbString,
            //        DbType = (DbType)Convert.ToInt32(Enum.Parse(typeof(DbType), item.DbType)),
            //        IsAutoCloseConnection = true,
            //        ConfigId = item.DbNumber,
            //        InitKeyType = InitKeyType.Attribute
            //    });
            //}
            services.AddSqlSugar(connectConfigList.ToArray()
                , db =>
                {
                    db.Aop.OnLogExecuting = (sql, pars) =>
                    {
                        if (sql.StartsWith("SELECT"))
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                        }
                        if (sql.StartsWith("UPDATE") || sql.StartsWith("INSERT"))
                        {
                            Console.ForegroundColor = ConsoleColor.White;
                        }
                        if (sql.StartsWith("DELETE"))
                        {
                            Console.ForegroundColor = ConsoleColor.Blue;
                        }
                        //App.PrintToMiniProfiler("SqlSugar", "Info", sql + "\r\n" + db.Utilities.SerializeObject(pars.ToDictionary(it => it.ParameterName, it => it.Value)));
                        Console.WriteLine(sql + "\r\n\r\n" + SqlProfiler.ParameterFormat(sql, pars));
                        //  App.PrintToMiniProfiler("SqlSugar", "Info", SqlProfiler.ParameterFormat(sql, pars));
                    };
                    //执行超时时间
                    db.Ado.CommandTimeOut = 30;
                    //配置多租户全局过滤器
                    //db.QueryFilter.Add(new TableFilterItem<SysUser>(FilterExpression<SysUser>()));
                    //db.QueryFilter.Add(new TableFilterItem<SysOrg>(FilterExpression<SysOrg>()));
                    //db.QueryFilter.Add(new TableFilterItem<SysPos>(FilterExpression<SysPos>()));
                    //db.QueryFilter.Add(new TableFilterItem<SysRole>(FilterExpression<SysRole>()));
                    //db.QueryFilter.Add(new TableFilterItem<OnlineUser>(FilterExpression()));

                    //// 配置加删除全局过滤器
                    //db.QueryFilter.Add(new TableFilterItem<SysApp>(it => it.IsDeleted == false));
                    //db.QueryFilter.Add(new TableFilterItem<SysCodeGen>(it => it.IsDeleted == false));
                    //db.QueryFilter.Add(new TableFilterItem<SysCodeGenConfig>(it => it.IsDeleted == false));
                    //db.QueryFilter.Add(new TableFilterItem<SysDictData>(it => it.IsDeleted == false));
                    //db.QueryFilter.Add(new TableFilterItem<SysDictType>(it => it.IsDeleted == false));
                    //db.QueryFilter.Add(new TableFilterItem<SysFile>(it => it.IsDeleted == false));
                    //db.QueryFilter.Add(new TableFilterItem<SysMenu>(it => it.IsDeleted == false));
                    //db.QueryFilter.Add(new TableFilterItem<SysNotice>(it => it.IsDeleted == false));
                    //db.QueryFilter.Add(new TableFilterItem<SysOauthUser>(it => it.IsDeleted == false));
                    //db.QueryFilter.Add(new TableFilterItem<SysOrg>(it => it.IsDeleted == false));
                    //db.QueryFilter.Add(new TableFilterItem<SysPos>(it => it.IsDeleted == false));
                    //db.QueryFilter.Add(new TableFilterItem<SysRole>(it => it.IsDeleted == false));
                    //db.QueryFilter.Add(new TableFilterItem<SysTimer>(it => it.IsDeleted == false));
                    //db.QueryFilter.Add(new TableFilterItem<SysUser>(it => it.IsDeleted == false));
                    //db.QueryFilter.Add(new TableFilterItem<SysTenant>(it => it.IsDeleted == false));
                });
            #endregion

        }
    }
}