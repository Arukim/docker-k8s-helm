using DNATrack.Common.Core;
using DNATrack.Common.Messaging;
using DNATrack.Common.Messaging.Commands;
using DNATrack.Web.Admin.Models;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace DNATrack.Web.Admin.Controllers
{
    public class HomeController : Controller
    {
        private readonly IBusControl busControl;
        private readonly RabbitMQConfiguration rmqConfig;
        private readonly ILogger logger;

        public HomeController(ILogger<HomeController> logger, IBusControl busControl,
            IOptions<RabbitMQConfiguration> rmqOptions)
        {
            this.logger = logger;
            this.busControl = busControl;
            rmqConfig = rmqOptions.Value;
        }

        public IActionResult Index()
        {
            return View(new BatchViewModel { Count = 1000 });
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("Count")]BatchViewModel batch)
        {
            var batchId = Guid.NewGuid();
            using (var _ = logger.BeginScope($"Sending batch {batchId}"))
            {
                var ep = await busControl.GetSendEndpoint(ConfigurationBuilder.GetEndpoint(rmqConfig, Constants.Queues.NewTrace));
                for (int i = 0; i < batch.Count; i++)
                {
                    await ep.Send(new NewTrace { BatchId = batchId, TraceNumber = i });
                    logger.LogInformation($"Trace #{i} sent");
                }
            }
            return RedirectToAction(nameof(Index));
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
