using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace DNATrack.Common.Core
{
    public abstract class BaseServiceApplication<TProg, TService> : AbstractApplication<TProg>
        where TProg : class
        where TService : IService
    {
        // AutoResetEvent to signal when to exit the application.
        private readonly AutoResetEvent waitHandle = new AutoResetEvent(false);

        private TService app;

        protected override async Task DoWorkload()
        {
            // Handle Control+C or Control+Break (before StartAsync)
            Console.CancelKeyPress += (o, e) =>
            {
                e.Cancel = true;
                waitHandle.Set();
            };

            logger.LogInformation("Application is starting");

            app = serviceProvider.GetService<TService>();
            await app.StartAsync();
            logger.LogInformation("Application is ready");

            Console.WriteLine("Press CTRL+C to exit application");
            // Wait for cancellation
            waitHandle.WaitOne();

            logger.LogInformation("Application is stopping");
            await app.StopAsync();
            logger.LogInformation("Application is exiting");
        }
    }
}
