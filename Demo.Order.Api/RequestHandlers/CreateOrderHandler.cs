using System;
using System.Threading;
using System.Threading.Tasks;
using Demo.Order.Api.Dtos;
using Demo.Order.ServiceContracts;
using MediatR;
using NServiceBus;

namespace Demo.Order.Api.RequestHandlers
{
    public class CreateOrderHandler : IRequestHandler<CreateOrderRequest, Guid>
    {
        private readonly IMessageSession messageSession;

        public CreateOrderHandler(IMessageSession messageSession)
        {
            this.messageSession = messageSession;
        }
        
        public async Task<Guid> Handle(CreateOrderRequest request, CancellationToken cancellationToken)
        {
            var key = Guid.NewGuid();
            await this.messageSession.Send(new CreateOrder {Key = key, Total = request.Total});
            return key;
        }
    }
}