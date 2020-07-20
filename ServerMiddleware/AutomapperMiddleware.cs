using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using AutoMapper;
using Microcervices.Core.AutomapperProfiles;
using Microsoft.Extensions.DependencyInjection;

namespace Microcervices.Core.ServerMiddleware
{
    public static class AutomapperMiddleware
    {
        public static void AddScheduleAutomapper(this IServiceCollection services)
        {
            var conf = new MapperConfiguration(p =>
            {
                p.AddProfile(new ScheduleModuleProfile());
            });

            IMapper mapper = conf.CreateMapper();

            services.AddSingleton(mapper);
        }

        public static void AddAutomapper(this IServiceCollection services)
        {

            var conf = new MapperConfiguration(p =>
            {
                p.AddMaps(Assembly.GetExecutingAssembly());
            });

            IMapper mapper = conf.CreateMapper();

            services.AddSingleton(mapper);
        }
    }
}
