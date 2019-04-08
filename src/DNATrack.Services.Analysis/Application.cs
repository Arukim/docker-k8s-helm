using DNATrack.Common.Core;
using DNATrack.Common.Messaging;
using DNATrack.Persistence;
using DNATrack.Services.Analysis.Consumers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Configuration;

namespace DNATrack.Services.Analysis
{
    class Application : BaseServiceApplication<Program, AnalysisService>
    {
        protected override string Name => "Analysis Service";

        protected override void BootstrapServices(IServiceCollection services)
        {
            var mongoSection = configuration.GetSection(Constants.ConfigSections.Mongo);
            if (!mongoSection.Exists())
                throw new ConfigurationErrorsException($"Required section '{mongoSection.Path}' not found in configuration");

            var rmqSection = configuration.GetSection(Constants.ConfigSections.Rabbit);
            if (!rmqSection.Exists())
                throw new ConfigurationErrorsException($"Required section '{rmqSection.Path}' not found in configuration");

            services
                .AddScoped<NewTraceConsumer>()
                .AddSingleton<AnalysisService>()
                .Configure<MongoDbConfiguration>(mongoSection)
                .Configure<RabbitMQConfiguration>(rmqSection);
        }
    }
}
