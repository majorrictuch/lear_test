using Mapster;
using MapsterMapper;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Lear.CRS.Extensions
{
    /// <summary>
    /// Automapper 启动服务
    /// </summary>
    public static class MapsterSetup
    {
        public static void AddMapsterSetup(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));


            var config = new TypeAdapterConfig();
            services.AddSingleton(config);
           // services.AddScoped<IMapper, ServiceMapper>();

        }
    }
}
