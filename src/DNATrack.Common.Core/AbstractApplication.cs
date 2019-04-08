using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Threading.Tasks;

namespace DNATrack.Common.Core
{
    public abstract class AbstractApplication<TProg>
        where TProg : class
    {
        protected IConfigurationRoot configuration;
        protected IServiceProvider serviceProvider;
        protected ILogger<TProg> logger;
        /// <summary>
        /// Where is our application running
        /// </summary>
        protected string environmentName;
        /// <summary>
        /// Name of the Host we are running on
        /// </summary>
        protected string hostName;

        #region Anchestors Contract
        protected abstract string Name { get; }
        protected abstract void BootstrapServices(IServiceCollection services);
        protected abstract Task DoWorkload();
        #endregion

        private IConfigurationRoot BuildConfiguration(string[] args)
        {
            environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")?.ToLowerInvariant();

            // for kubernetes we use current Pod name as host name
            hostName = Environment.GetEnvironmentVariable("HOST_NAME") ?? "default";

            return new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{environmentName}.json", optional: true)
                .AddJsonFile(Constants.LinuxConfigPath, optional: true, reloadOnChange: true)
                .AddEnvironmentVariables()
                .AddUserSecrets<TProg>()
                .AddCommandLine(args)
                .Build();
        }

        private IServiceProvider BuildServices()
        {
            var serviceCollection = new ServiceCollection()
               .AddLogging((logging) =>
               {
                   logging.AddConfiguration(configuration.GetSection("Logging"));
                   logging.AddConsole();
               })
               .AddOptions();

            BootstrapServices(serviceCollection);

            var services = serviceCollection.BuildServiceProvider();

            logger = services.GetService<ILogger<TProg>>();

            return services;
        }

        public async Task Run(string[] args)
        {
            Console.Title = Name;

            Console.WriteLine($"{Name}");

            configuration = BuildConfiguration(args);
            serviceProvider = BuildServices();

            logger.LogInformation($"Current environment is {environmentName}");
            logger.LogInformation($"Current host is {hostName}");

            await DoWorkload();
        }
    }
}
