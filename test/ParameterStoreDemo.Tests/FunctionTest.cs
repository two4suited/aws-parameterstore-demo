using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using Amazon.Lambda.Core;
using Amazon.Lambda.TestUtilities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ParameterStoreDemo.Lambda;

namespace ParameterStoreDemo.Tests
{
    public class FunctionTest
    {
        private IHost _host;
        private CustomConfig _customConfig;
        public FunctionTest()
        {
            var host = new HostBuilder();

            _customConfig = new CustomConfig()
            {
                FirstName = "Test",
                Lastname = "Class",
                Birthdate = DateTime.Today.AddYears(-10)
            };
                
            host.ConfigureServices((c, s) =>
            {
                s.Configure<CustomConfig>(options =>
                {
                    options.FirstName = _customConfig.FirstName;
                    options.Lastname = _customConfig.Lastname;
                    options.Birthdate = _customConfig.Birthdate;
                });
                
                s.AddScoped<IConfigReader,ConfigReader>();
               
            });

             _host= host.Build();
        }
        
        [Fact]
        public void TestHandler()
        {
            // Invoke the lambda function and confirm the string was upper cased.
            var function = new Function(_host);
            var context = new TestLambdaContext();
            var functionResponse = function.FunctionHandler(context);

            var options = Options.Create(new CustomConfig()
            {
                Birthdate = _customConfig.Birthdate,
                Lastname = _customConfig.Lastname,
                FirstName = _customConfig.FirstName
            });
            var service = new ConfigReader(options);
            var response = service.Read();

            Assert.Equal(response, functionResponse);
        }
    }
}