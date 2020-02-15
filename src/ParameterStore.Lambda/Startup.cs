using System;
using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace ParameterStoreDemo.Lambda
{
    public static class Startup
    {
        public static IHost Build()
        {
            var host = new HostBuilder();
                
            host.ConfigureAppConfiguration(c =>
                c.AddSystemsManager(configureSource =>
                {
                    configureSource.Path = "/parameterstoredemo";
                    configureSource.Optional = false;
                }));
                
            host.ConfigureServices((c, s) =>
            {
                s.Configure<CustomConfig>(c.Configuration.GetSection("Secret"));
                
                s.AddScoped<IConfigReader,ConfigReader>();
                s.AddLogging(x =>
                {
                    x.AddConsole();
                    x.AddAWSProvider();
                    x.SetMinimumLevel(LogLevel.Information);
                });
            });

            return host.Build();

        }
    
        
    }
}