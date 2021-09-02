using System.Threading.Tasks;
using Demo.Payment.ServiceContracts;
using Microsoft.Extensions.Logging;
using NServiceBus;

namespace Demo.Payment.Endpoint.Handlers
{
    public sealed class MakePaymentHandler : IHandleMessages<MakePayment>
    {
        private readonly ILogger<MakePaymentHandler> logger;

        public MakePaymentHandler(ILogger<MakePaymentHandler> logger)
            => this.logger = logger;
        
        public async Task Handle(MakePayment message, IMessageHandlerContext context)
        {
            await context.Publish(new PaymentSucceeded {OrderKey = message.OrderKey});
            this.logger.LogInformation($"Handling {nameof(MakePayment)}", message);
        }
    }
}