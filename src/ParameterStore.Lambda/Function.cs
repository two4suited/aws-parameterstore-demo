using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Amazon.Lambda.Core;
using Microsoft.Extensions.DependencyInjection;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]

namespace ParameterStoreDemo.Lambda
{
    public class Function
    {
        private readonly IServiceProvider _serviceProvider;

        public Function(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public Function() 
        {
           

        }
        
        public string FunctionHandler(string input, ILambdaContext context)
        {
            var serviceProvider = Startup.Container.BuildServiceProvider();
            var service = serviceProvider.GetService<IConfigReader>();

            return service.Read();
        }
    }
}