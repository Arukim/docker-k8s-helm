using DNATrack.Common.Core;
using DNATrack.Common.Messaging;
using DNATrack.Services.Analysis.Consumers;
using MassTransit;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;

namespace DNATrack.Services.Analysis
{
    class AnalysisService : IService
    {
        private readonly RabbitMQConfiguration rmqConfig;
        private readonly IServiceProvider provider;

        private IBusControl busControl;

        public AnalysisService(IOptions<RabbitMQConfiguration> rmqConfig, IServiceProvider provider)
        {
            this.rmqConfig = rmqConfig.Value;
            this.provider = provider;
        }

        public async Task StartAsync()
        {
            busControl = Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                var host = cfg.Host(new Uri(rmqConfig.Endpoint), h =>
                {
                    h.Username(rmqConfig.Username);
                    h.Password(rmqConfig.Password);
                });

                cfg.ReceiveEndpoint(host, Constants.Queues.NewTrace, ep =>
                {
                    ep.PrefetchCount = (ushort)Environment.ProcessorCount;

                    ep.Consumer<NewTraceConsumer>(provider);
                });
            });

            await busControl.StartAsync();
        }

        public async Task StopAsync()
        {
            await busControl.StopAsync();
        }
    }
}
