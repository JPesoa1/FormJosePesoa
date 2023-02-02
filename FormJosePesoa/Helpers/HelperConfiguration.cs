using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormJosePesoa.Helpers
{
    public class HelperConfiguration
    {
        public static string GetConnectionString()
        {
            IConfigurationBuilder builder = new ConfigurationBuilder().AddJsonFile("Configuration.json", true, true);
            IConfigurationRoot config = builder.Build();
            string connectionString = config["SqlPracticaado"];
            return connectionString;
        }

    }
}
