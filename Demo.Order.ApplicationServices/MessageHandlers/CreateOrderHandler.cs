using System.Threading.Tasks;
using Demo.Order.Domain;
using Demo.Order.Domain.Entities;
using Demo.Order.Domain.Repositories;
using Demo.Order.ServiceContracts;
using Microsoft.Extensions.Logging;
using NServiceBus;

namespace Demo.Order.ApplicationServices.MessageHandlers
{
    public sealed class CreateOrderHandler : IHandleMessages<CreateOrder>
    {
        private readonly ILogger<CreateOrderHandler> logger;
        private readonly IOrderRepository orderRepository;

        public CreateOrderHandler(
            ILogger<CreateOrderHandler> logger, 
            IOrderRepository orderRepository)
        {
            this.logger = logger;
            this.orderRepository = orderRepository;
        }
        
        public async Task Handle(CreateOrder message, IMessageHandlerContext context)
        {
            var order = new DraftOrder(message.Key, message.Total);
            await this.orderRepository.Save(order);
            
            this.logger.LogInformation($"Draft Order created key: {order.Key}", order);
        }
    }
}