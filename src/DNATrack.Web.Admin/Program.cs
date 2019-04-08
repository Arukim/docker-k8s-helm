using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DNATrack.Common.Core;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace DNATrack.Web.Admin
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }


        static IConfigurationRoot ConfigConfiguration(string[] args)
        {
            var environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")?.ToLowerInvariant();
            return new ConfigurationBuilder()
             .AddJsonFile("appsettings.json", optional: true)
             .AddJsonFile($"appsettings.{environmentName}.json", optional: true)
             .AddJsonFile(Constants.LinuxConfigPath, optional: true, reloadOnChange: true)
             .AddEnvironmentVariables()
             .AddUserSecrets<Program>()
             .AddCommandLine(args)
             .Build();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args)
        {
            var config = ConfigConfiguration(args);
            return WebHost.CreateDefaultBuilder(args)
                .UseConfiguration(config)
                .UseStartup<Startup>();
        }
    }
}
