using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Amazon.Lambda.Core;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]

namespace ParameterStoreDemo.Lambda
{
    public class Function
    {
        private readonly IHost _host;

        public Function(IHost host)
        {
            _host = host;
        }

        public Function() : this(Startup.Build()) { }

        public string FunctionHandler(string input, ILambdaContext context)
        {
            
            var service = _host.Services.GetService<IConfigReader>();

            return service.Read();
        }
    }
}