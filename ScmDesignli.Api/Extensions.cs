using Microsoft.OpenApi.Models;
using ScmDesignli.Api.Services;
using ScmDesignli.Application.Interfaces;
using ScmDesignli.Application.Commands.Employee.CreateEmployee;
using ScmDesignli.Infrastructure.Persistence;
using ScmDesignli.Infrastructure.Persistence.Repositories;
using ScmDesignli.Application.Interfaces.Repositories;
using System.Reflection;

namespace ScmDesignli.Api
{
    public static class Extensions
    {
        public static IServiceCollection AddAppConfiguration(this IServiceCollection services)
        {

            services.AddMediatR(cfg =>
                 cfg.RegisterServicesFromAssembly(typeof(CreateEmployeeCommand).Assembly));

            // Register repositories
            services.AddSingleton(typeof(IRepository<>), typeof(Repository<>));

            services.AddScoped<IDataSeeder, DataSeeder>();
            services.AddHostedService<AppInitializer>();
            return services;
        }

        internal static IServiceCollection AddSwaggerGenConfig(this IServiceCollection services)
        {
            services.AddSwaggerGen(config =>
            {
                config.SwaggerDoc("v1", new()
                {
                    Version = "v1",
                    Title = "Api-scm-Deisgnli",
                    Description = "API for Designli"
                });
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                config.IncludeXmlComments(xmlPath);
                config.UseInlineDefinitionsForEnums();
            });
            return services;
        }


    }
}
