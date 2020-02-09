using Microsoft.Extensions.Options;

namespace ParameterStoreDemo.Lambda
{
    public class ConfigReader : IConfigReader
    {
        private readonly IOptions<CustomConfig> _options;

        public ConfigReader(IOptions<CustomConfig> options)
        {
            _options = options;
        }
        public string Read()
        {
            return $"{_options.Value.FirstName} {_options.Value.Lastname}";
        }
    }
}