using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Lear.CRS.Extensions;
using Autofac;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.AspNetCore.Mvc.Controllers;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using Microsoft.AspNetCore.Server.Kestrel.Core;

using System.Reflection;
using Lear.CRS.Common;
using Lear.CRS.Middlewares;
using Lear.CRS.Filter;
using System.IdentityModel.Tokens.Jwt;
using Yitter.IdGenerator;
using Lear.CRS.IServices;
using Lear.CRS.Tasks;

namespace Lear.CRS.API
{
    public class Startup
    {


        private IServiceCollection _services;

        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            Env = env;
        }

        public IConfiguration Configuration { get; }
        public IWebHostEnvironment Env { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {



            services.AddSingleton(new Appsettings(Configuration));
            //  services.AddSingleton(new LogLock(Env.ContentRootPath));


            Permissions.IsUseIds4 = Appsettings.app(new string[] { "Startup", "IdentityServer4", "Enabled" }).ToBool();

            // ȷ������֤���ķ��ص�ClaimType�������ģ���ʹ��Mapӳ��
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

            services.AddMemoryCacheSetup();
            //   services.AddRedisCacheSetup();

            
            services.AddSqlsugar2Setup();  //lear ����
            services.AddDbSetup();
            services.AddMapsterSetup();
            services.AddCorsSetup();
            //  services.AddMiniProfilerSetup();
            services.AddSwaggerSetup();
            services.AddJobSetup();

            services.AddHttpContextSetup();
            services.AddAppConfigSetup(Env);
            // services.AddHttpApi();
            //   services.AddRedisInitMqSetup();


            // services.AddEventBusSetup();

            // services.AddJwt<JwtHandler>(enableGlobalAuthorize: true);

            // ��Ȩ+��֤ (jwt or ids4)
            services.AddAuthorizationSetup();
            services.AddAuthentication_JWTSetup();


            //services.AddIpPolicyRateLimitSetup(Configuration);

            services.Configure<KestrelServerOptions>(x => x.AllowSynchronousIO = true)
                 .Configure<IISServerOptions>(x => x.AllowSynchronousIO = true);


            services.AddControllers(o =>
            {
                // ȫ���쳣����
                o.Filters.Add(typeof(GlobalExceptionsFilter));
                // ȫ��·��Ȩ�޹�Լ
                o.Conventions.Insert(0, new GlobalRouteAuthorizeConvention());
                // ȫ��·��ǰ׺��ͳһ�޸�·��
                o.Conventions.Insert(0, new GlobalRoutePrefixFilter(new RouteAttribute(RoutePrefix.Name)));
            })
                       // ����д��Ҳ����
                       //.AddJsonOptions(options =>
                       //{
                       //    options.JsonSerializerOptions.PropertyNamingPolicy = null;
                       //})
                       //MVCȫ������Json���л�����
                       .AddNewtonsoftJson(options =>
                       {
                           //����ѭ������
                           options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                           //��ʹ���շ���ʽ��key
                           // options.SerializerSettings.ContractResolver = new DefaultContractResolver();
                           //����ʱ���ʽ
                           options.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
                           //����Model��Ϊnull������
                           //options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                           //���ñ���ʱ�����UTCʱ��
                           options.SerializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.Local;
                       });


            // ����ѩ��id��workerId��ȷ��ÿ��ʵ��workerId��Ӧ��ͬ
            var workerId = ushort.Parse(Appsettings.app(new string[] { "SnowId", "WorkerId" }));
            YitIdHelper.SetIdGenerator(new IdGeneratorOptions { WorkerId = workerId });


            services.Replace(ServiceDescriptor.Transient<IControllerActivator, ServiceBasedControllerActivator>());

            _services = services;
            //֧�ֱ����ȫ ����:֧�� System.Text.Encoding.GetEncoding("GB2312")  System.Text.Encoding.GetEncoding("GB18030") 
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        }


        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterModule(new AutofacModuleRegister());
            builder.RegisterModule<AutofacPropertityModuleReg>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IHostApplicationLifetime lifetime, ITasksQzService tasksQzServices, ISchedulerCenter schedulerCenter)
        {
            // Ip����,�����Źܵ����
            // app.UseIpLimitMildd();
            // ��¼�����뷵������ 
            app.UseReuestResponseLog();
            // �û����ʼ�¼(����ŵ���㣬��Ȼ��������쳣���ᱨ����Ϊ���ܷ�����)
            // app.UseRecordAccessLogsMildd();
            // signalr 
            // app.UseSignalRSendMildd();
            // ��¼ip����
            // app.UseIPLogMildd();
            // �鿴ע������з���
            app.UseAllServicesMildd(_services);

            if (env.IsDevelopment())
            {
                // �ڿ��������У�ʹ���쳣ҳ�棬�������Ա�¶�����ջ��Ϣ�����Բ�Ҫ��������������
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");

                // �ڷǿ��������У�ʹ��HTTP�ϸ�ȫ����(or HSTS) ���ڱ���web��ȫ�Ƿǳ���Ҫ�ġ�
                // ǿ��ʵʩ HTTPS �� ASP.NET Core����� app.UseHttpsRedirection
                //app.UseHsts();
            }

            // ��װSwaggerչʾ

            app.UseSwaggerMildd(() => GetType().GetTypeInfo().Assembly.GetManifestResourceStream("Lear.CRS.API.index.html"));

            // ������������ ע���±���Щ�м����˳�򣬺���Ҫ ������������

            // CORS����
            app.UseCors(Appsettings.app(new string[] { "Startup", "Cors", "PolicyName" }));
            // ��תhttps
            //app.UseHttpsRedirection();
            // ʹ�þ�̬�ļ�
            app.UseStaticFiles();
            // ʹ��cookie
            app.UseCookiePolicy();
            // ���ش�����
            app.UseStatusCodePages();
            // Routing
            app.UseRouting();
            // �����Զ�����Ȩ�м�������Գ��ԣ������Ƽ�
            // app.UseJwtTokenAuth();

            // �����û�������ͨ����Ȩ
            //if (Configuration.GetValue<bool>("AppSettings:UseLoadTest"))
            //{
            //    app.UseMiddleware<ByPassAuthMidd>();
            //}
            // �ȿ�����֤
            app.UseAuthentication();
            // Ȼ������Ȩ�м��
            app.UseAuthorization();
            //�������ܷ���
            //  app.UseMiniProfilerMildd();
            // �����쳣�м����Ҫ�ŵ����
            app.UseExceptionHandlerMidd();
            //  app.UseLogDashboardMildd();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

                //    endpoints.MapHub<ChatHub>("/api2/chatHub");
            });
            app.UseQuartzJobMildd(tasksQzServices, schedulerCenter);
            // ������������
            //  app.UseSeedDataMildd(myContext, Env.WebRootPath);
            // ����QuartzNetJob���ȷ���

            // ����ע��
            // app.UseConsulMildd(Configuration, lifetime);
            // �¼����ߣ����ķ���
            // app.ConfigureEventBus();

        }

    }
}
