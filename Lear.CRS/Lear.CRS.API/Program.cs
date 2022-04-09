using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Lear.CRS.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //��ʼ��Ĭ������Builder
            Host.CreateDefaultBuilder(args)
             .UseServiceProviderFactory(new AutofacServiceProviderFactory())
             .ConfigureWebHostDefaults(webBuilder =>
             {
                 webBuilder

                 .UseStartup<Startup>()
                 
                 .ConfigureAppConfiguration((hostingContext, config) =>
                 {
                     config.Sources.Clear();
                     config.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

                 })
                 .ConfigureLogging((hostingContext, builder) =>
                 {
                     // 1.���˵�ϵͳĬ�ϵ�һЩ��־
                     builder.AddFilter("System", LogLevel.Error);
                     builder.AddFilter("Microsoft", LogLevel.Error);

                     // 2.Ҳ������appsettings.json�����ã�LogLevel�ڵ�

                     // 3.ͳһ����
                     builder.SetMinimumLevel(LogLevel.Error);

                     // Ĭ��log4net.confg
                    // builder.AddLog4Net(Path.Combine(Directory.GetCurrentDirectory(), "Log4net.config"));
                 })
                 ;
             })
            // ���ɳ��� web Ӧ�ó���� Microsoft.AspNetCore.Hosting.IWebHost��Build��WebHostBuilder���յ�Ŀ�ģ�������һ�������WebHost����������������
             .Build()
            // ���� web Ӧ�ó�����ֹ�����߳�, ֱ�������رա�
            // �������� ���쳣���鿴 Log �ļ����µ��쳣��־ ��������  
             .Run();
        }


    }
}
