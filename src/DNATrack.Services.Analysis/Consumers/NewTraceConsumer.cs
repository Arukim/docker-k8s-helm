using DNATrack.Common.Messaging.Commands;
using DNATrack.Persistence;
using DNATrack.Persistence.Entities;
using MassTransit;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DNATrack.Services.Analysis.Consumers
{
    class NewTraceConsumer : IConsumer<NewTrace>
    {
        private readonly MongoDbConfiguration dbConfig;
        private readonly ILogger logger;
        public NewTraceConsumer(IOptions<MongoDbConfiguration> dbConfig, ILogger<NewTraceConsumer> logger)
        {
            this.dbConfig = dbConfig.Value;
            this.logger = logger;
        }

        public async Task Consume(ConsumeContext<NewTrace> context)
        {
            var msg = context.Message;
            var sw = new System.Diagnostics.Stopwatch();
            sw.Start();

            using (var scope = logger.BeginScope($"consuming {msg.BatchId}:{msg.TraceNumber}")) {

                logger.LogTrace("started");

                var data = new byte[0];
                using (var sha256Hash = SHA256.Create())
                {
                    do
                    {
                        var tData = Encoding.UTF8.GetBytes(DateTime.UtcNow.Ticks.ToString());

                        data = data.Concat(tData).ToArray();
                        data = sha256Hash.ComputeHash(data);

                    } while (sw.ElapsedMilliseconds < 1000);
                }

                var client = new MongoClient(dbConfig.Endpoint);
                var database = client.GetDatabase(dbConfig.Database);
                var collection = database.GetCollection<Trace>("traces");


                await collection.InsertOneAsync(new Trace { DNA = data, BatchId = msg.BatchId, TraceNumber = msg.TraceNumber });

                logger.LogInformation("completed");
            }
        }
    }
}
