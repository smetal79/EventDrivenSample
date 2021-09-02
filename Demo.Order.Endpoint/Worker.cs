using System;
using System.Threading;
using System.Threading.Tasks;
using Demo.Order.Domain;
using Demo.Order.Domain.Repositories;
using Demo.Order.ServiceContracts;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NServiceBus;

namespace Demo.Order.Endpoint
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IMessageSession messageSession;
        private readonly IOrderRepository orderRepository;

        public Worker(ILogger<Worker> logger, IMessageSession messageSession)
        {
            _logger = logger;
            this.messageSession = messageSession;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await this.messageSession.Send(new SubmitOrder {Key = Guid.Parse("5ce72080-72c5-4b9e-92fa-4c0d4db9046b") });
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}