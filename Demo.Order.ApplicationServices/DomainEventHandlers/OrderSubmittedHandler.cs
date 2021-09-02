using System.Threading.Tasks;
using Demo.Order.Domain.Events;
using Demo.Order.Domain.Infrastructure;
using Demo.Payment.ServiceContracts;
using Microsoft.Extensions.Logging;
using NServiceBus;

namespace Demo.Order.ApplicationServices.DomainEventHandlers
{
    public sealed class OrderSubmittedHandler 
        : IHandleDomainEvent<OrderSubmitted>
    {
        private readonly ILogger<OrderSubmittedHandler> logger;
        private readonly IMessageSession messageSession;

        public OrderSubmittedHandler(ILogger<OrderSubmittedHandler> logger, IMessageSession messageSession)
        {
            this.logger = logger;
            this.messageSession = messageSession;
        }
        
        public async Task Handle(OrderSubmitted @event)
        {
            await this.messageSession.Send(new MakePayment {OrderKey = @event.OrderKey, Total = @event.Total});
            this.logger.LogInformation($"Order submitted {@event.OrderKey}", @event);
        }
    }
}