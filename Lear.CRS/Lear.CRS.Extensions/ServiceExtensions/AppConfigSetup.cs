using Lear.CRS.Common;
using Lear.CRS.Common.Helper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Text;

namespace Lear.CRS.Extensions
{
    /// <summary>
    /// 项目 启动服务
    /// </summary>
    public static class AppConfigSetup
    {
        public static void AddAppConfigSetup(this IServiceCollection services, IHostEnvironment env)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            if (Appsettings.app(new string[] { "Startup", "AppConfigAlert", "Enabled" }).ToBool())
            {
                if (env.IsDevelopment())
                {
                    Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
                    Console.OutputEncoding = Encoding.GetEncoding("GB2312");
                }

                Console.WriteLine("************ Lear.CRS Config Set *****************");

                ConsoleHelper.WriteSuccessLine("Current environment: " + Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT"));

                // 授权策略方案
                if (Permissions.IsUseIds4)
                {
                    ConsoleHelper.WriteSuccessLine($"Current authorization scheme: " + (Permissions.IsUseIds4 ? "Ids4" : "JWT"));
                }
                else
                {
                    Console.WriteLine($"Current authorization scheme: " + (Permissions.IsUseIds4 ? "Ids4" : "JWT"));
                }

                // Redis缓存AOP
                if (!Appsettings.app(new string[] { "AppSettings", "RedisCachingAOP", "Enabled" }).ToBool())
                {
                    Console.WriteLine($"Redis Caching AOP: False");
                }
                else
                {
                    ConsoleHelper.WriteSuccessLine($"Redis Caching AOP: True");
                }

                // 内存缓存AOP
                if (!Appsettings.app(new string[] { "AppSettings", "MemoryCachingAOP", "Enabled" }).ToBool())
                {
                    Console.WriteLine($"Memory Caching AOP: False");
                }
                else
                {
                    ConsoleHelper.WriteSuccessLine($"Memory Caching AOP: True");
                }

                // 服务日志AOP
                if (!Appsettings.app(new string[] { "AppSettings", "LogAOP", "Enabled" }).ToBool())
                {
                    Console.WriteLine($"Service Log AOP: False");
                }
                else
                {
                    ConsoleHelper.WriteSuccessLine($"Service Log AOP: True");
                }

                // 开启的中间件日志
                var requestResponseLogOpen = Appsettings.app(new string[] { "Middleware", "RequestResponseLog", "Enabled" }).ToBool();
                var ipLogOpen = Appsettings.app(new string[] { "Middleware", "IPLog", "Enabled" }).ToBool();
                var recordAccessLogsOpen = Appsettings.app(new string[] { "Middleware", "RecordAccessLogs", "Enabled" }).ToBool();
                ConsoleHelper.WriteSuccessLine($"OPEN Log: " +
                    (requestResponseLogOpen ? "RequestResponseLog √," : "") +
                    (ipLogOpen ? "IPLog √," : "") +
                    (recordAccessLogsOpen ? "RecordAccessLogs √," : "")
                    );

                // 事务AOP
                if (!Appsettings.app(new string[] { "AppSettings", "TranAOP", "Enabled" }).ToBool())
                {
                    Console.WriteLine($"Transaction AOP: False");
                }
                else
                {
                    ConsoleHelper.WriteSuccessLine($"Transaction AOP: True");
                }

                // 数据库Sql执行AOP
                if (!Appsettings.app(new string[] { "AppSettings", "SqlAOP", "OutToLogFile", "Enabled" }).ToBool())
                {
                    Console.WriteLine($"DB Sql AOP To LogFile: False");
                }
                else
                {
                    ConsoleHelper.WriteSuccessLine($"DB Sql AOP To LogFile: True");
                }

                // Sql执行日志输出到控制台
                if (!Appsettings.app(new string[] { "AppSettings", "SqlAOP", "OutToConsole", "Enabled" }).ToBool())
                {
                    Console.WriteLine($"DB Sql AOP To Console: False");
                }
                else
                {
                    ConsoleHelper.WriteSuccessLine($"DB Sql AOP To Console: True");
                }

                // SingnalR发送数据
                if (!Appsettings.app(new string[] { "Middleware", "SignalR", "Enabled" }).ToBool())
                {
                    Console.WriteLine($"SignalR send data: False");
                }
                else
                {
                    ConsoleHelper.WriteSuccessLine($"SignalR send data: True");
                }

                // IP限流
                if (!Appsettings.app("Middleware", "IpRateLimit", "Enabled").ToBool())
                {
                    Console.WriteLine($"IpRateLimiting: False");
                }
                else
                {
                    ConsoleHelper.WriteSuccessLine($"IpRateLimiting: True");
                }

                // 性能分析
                if (!Appsettings.app("Startup", "MiniProfiler", "Enabled").ToBool())
                {
                    Console.WriteLine($"MiniProfiler: False");
                }
                else
                {
                    ConsoleHelper.WriteSuccessLine($"MiniProfiler: True");
                }

                // CORS跨域
                if (!Appsettings.app("Startup", "Cors", "EnableAllIPs").ToBool())
                {
                    Console.WriteLine($"EnableAllIPs For CORS: False");
                }
                else
                {
                    ConsoleHelper.WriteSuccessLine($"EnableAllIPs For CORS: True");
                }

                // redis消息队列
                if (!Appsettings.app("Startup", "RedisMq", "Enabled").ToBool())
                {
                    Console.WriteLine($"Redis MQ: False");
                }
                else
                {
                    ConsoleHelper.WriteSuccessLine($"Redis MQ: True");
                }

                // RabbitMQ 消息队列
                if (!Appsettings.app("RabbitMQ", "Enabled").ToBool())
                {
                    Console.WriteLine($"RabbitMQ: False");
                }
                else
                {
                    ConsoleHelper.WriteSuccessLine($"RabbitMQ: True");
                }

                // Consul 注册服务
                if (!Appsettings.app("Middleware", "Consul", "Enabled").ToBool())
                {
                    Console.WriteLine($"Consul service: False");
                }
                else
                {
                    ConsoleHelper.WriteSuccessLine($"Consul service: True");
                }

                // EventBus 事件总线
                if (!Appsettings.app("EventBus", "Enabled").ToBool())
                {
                    Console.WriteLine($"EventBus: False");
                }
                else
                {
                    ConsoleHelper.WriteSuccessLine($"EventBus: True");
                }

                // 多库
                if (!Appsettings.app(new string[] { "MutiDBEnabled" }).ToBool())
                {
                    Console.WriteLine($"Is multi-DataBase: False");
                }
                else
                {
                    ConsoleHelper.WriteSuccessLine($"Is multi-DataBase: True");
                }

                // 读写分离
                if (!Appsettings.app(new string[] { "CQRSEnabled" }).ToBool())
                {
                    Console.WriteLine($"Is CQRS: False");
                }
                else
                {
                    ConsoleHelper.WriteSuccessLine($"Is CQRS: True");
                }

                Console.WriteLine();
            }

        }
    }
}
