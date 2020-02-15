using System;
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
            
            return $"{_options.Value.FirstName} {_options.Value.Lastname} is {CalculateAge(_options.Value.Birthdate)} years old";
        }
        
        private int CalculateAge(DateTime birthDay)
        {
            int years = DateTime.Now.Year - birthDay.Year;

            if((birthDay.Month > DateTime.Now.Month) || (birthDay.Month == DateTime.Now.Month && birthDay.Day > DateTime.Now.Day))
                years--;

            return years;
        }
    }
}