using ScmDesignli.Api.Services;
using ScmDesignli.Api.Filters;
using ScmDesignli.Application.Interfaces;
using ScmDesignli.Application.Commands.Employee.CreateEmployee;
using ScmDesignli.Application.Behaviors;
using ScmDesignli.Infrastructure.Persistence;
using ScmDesignli.Infrastructure.Persistence.Repositories;
using ScmDesignli.Application.Interfaces.Repositories;
using ScmDesignli.Domain.Entities;
using FluentValidation;
using MediatR;
using System.Reflection;

namespace ScmDesignli.Api
{
    public static class Extensions
    {
        public static IServiceCollection AddAppConfiguration(this IServiceCollection services)
        {
            // Register MediatR
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(typeof(CreateEmployeeCommand).Assembly);
                cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            });

            // Register FluentValidation validators
            services.AddValidatorsFromAssembly(typeof(CreateEmployeeCommand).Assembly);

            // Register repositories
            // Register the concrete EmployeeRepository as singleton first
            services.AddSingleton<EmployeeRepository>();
            
            // Register interfaces pointing to the same singleton instance
            services.AddSingleton<IEmployeeRepository>(sp => sp.GetRequiredService<EmployeeRepository>());
            services.AddSingleton<IRepository<Employee>>(sp => sp.GetRequiredService<EmployeeRepository>());
            
            // Register generic repository for other entities
            services.AddSingleton(typeof(IRepository<>), typeof(Repository<>));

            services.AddSingleton<IDataSeeder, DataSeeder>();
            services.AddHostedService<AppInitializer>();
            return services;
        }

        /// <summary>
        /// Add Swagger configuration
        /// </summary>
        /// <param name="services">services</param>
        /// <returns></returns>
        internal static IServiceCollection AddSwaggerGenConfig(this IServiceCollection services)
        {
            services.AddSwaggerGen(config =>
            {
                config.SwaggerDoc("v1", new()
                {
                    Version = "v1",
                    Title = "Api-scm-Designli",
                    Description = "API for Designli"
                });
                
                // Add XML comments from API
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                if (File.Exists(xmlPath))
                {
                    config.IncludeXmlComments(xmlPath);
                }
                
                // Add XML comments from Domain
                var domainXmlFile = "ScmDesignli.Domain.xml";
                var domainXmlPath = Path.Combine(AppContext.BaseDirectory, domainXmlFile);
                if (File.Exists(domainXmlPath))
                {
                    config.IncludeXmlComments(domainXmlPath);
                }
                
                // Use custom enum filter to show descriptions
                config.SchemaFilter<EnumSchemaFilter>();
            });
            return services;
        }


    }
}
