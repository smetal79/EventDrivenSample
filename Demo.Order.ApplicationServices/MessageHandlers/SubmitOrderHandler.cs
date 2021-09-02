using System.Threading.Tasks;
using Demo.Order.Domain;
using Demo.Order.Domain.Entities;
using Demo.Order.Domain.Repositories;
using Demo.Order.ServiceContracts;
using Microsoft.Extensions.Logging;
using NServiceBus;

namespace Demo.Order.ApplicationServices.MessageHandlers
{
    public sealed class SubmitOrderHandler : IHandleMessages<SubmitOrder>
    {
        private readonly ILogger<SubmitOrderHandler> logger;
        private readonly IOrderRepository orderRepository;

        public SubmitOrderHandler(
            ILogger<SubmitOrderHandler> logger,
            IOrderRepository orderRepository)
        {
            this.logger = logger;
            this.orderRepository = orderRepository;
        }

        public async Task Handle(SubmitOrder message, IMessageHandlerContext context)
        {
            var draftOrder = await this.orderRepository.GetByKey<DraftOrder>(message.Key);
            var submittedOrder = draftOrder.Submit();
            await orderRepository.SaveAs<DraftOrder, SubmittedOrder>(submittedOrder);
            
            this.logger.LogInformation($"Draft Order submitted key: {submittedOrder.Key}", submittedOrder);
        }
    }
}