using System.Threading.Tasks;
using Demo.Order.Domain;
using Demo.Order.Domain.Entities;
using Demo.Order.Domain.Repositories;
using Demo.Payment.ServiceContracts;
using NServiceBus;

namespace Demo.Order.ApplicationServices.MessageHandlers
{
    public sealed class PaymentSucceededHandler : IHandleMessages<PaymentSucceeded>
    {
        private readonly IOrderRepository orderRepository;

        public PaymentSucceededHandler(IOrderRepository orderRepository)
        {
            this.orderRepository = orderRepository;
        }
        
        public async Task Handle(PaymentSucceeded message, IMessageHandlerContext _)
        {
            var order = await this.orderRepository.GetByKey<SubmittedOrder>(message.OrderKey);
            var processedOrder = order.PaymentProcessed();
            await this.orderRepository.SaveAs<SubmittedOrder, ProcessedOrder>(processedOrder);
        }
    }
}