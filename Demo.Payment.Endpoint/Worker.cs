using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NServiceBus;

namespace Demo.Payment.Endpoint
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IMessageSession messageSession;

        public Worker(ILogger<Worker> logger, IMessageSession messageSession)
        {
            _logger = logger;
            this.messageSession = messageSession;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            return Task.CompletedTask;
            // var count = 0;
            // while (!stoppingToken.IsCancellationRequested)
            // {
            //     await this.messageSession.Send(new SendSms {Number = "123456789", Message = $"Message id {count}"});
            //     _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
            //     count++;
            //     await Task.Delay(1000, stoppingToken);
            // }
        }
    }
}