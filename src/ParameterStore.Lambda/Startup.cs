using System;
using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace ParameterStoreDemo.Lambda
{
    public static class Startup
    {
        public static IServiceCollection Container => ConfigureServices(Configuration); 
               
        private static IConfigurationRoot Configuration => new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddSystemsManager("/parameterstoredemo/configuration",optional: true)
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .AddEnvironmentVariables()
            .Build();
        
        private static IServiceCollection ConfigureServices(IConfigurationRoot root)
        {
            var services = new ServiceCollection();
            services.Configure<CustomConfig>(root.Bind);
                    
            services.AddScoped<IConfigReader,ConfigReader>();

            services.AddLogging(x =>
            {
                x.AddConsole();
                x.AddAWSProvider();
                x.SetMinimumLevel(LogLevel.Information);
            });
           
            return services;
        
        }
    }
}