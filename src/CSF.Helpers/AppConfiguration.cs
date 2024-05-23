using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace CSF.Helpers
{
    public class AppConfiguration
    {
        public static IConfigurationRoot Configuration { get; set; }

        public static string GetConnectionString()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            Configuration = builder.Build();

            return Configuration["ConnectionStrings:DefaultConnection"];
        }
    }

}