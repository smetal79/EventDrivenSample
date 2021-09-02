using System.Threading;
using System.Threading.Tasks;
using Demo.Order.Api.Dtos;
using Demo.Order.Api.Queries;
using MediatR;
using NServiceBus;

namespace Demo.Order.Api.RequestHandlers
{
    internal sealed class SubmitOrderHandler : IRequestHandler<SubmitOrder, SubmitOrderStatus>
    {
        private readonly IOrderQuery orderQuery;
        private readonly IMessageSession messageSession;

        public SubmitOrderHandler(IOrderQuery orderQuery, IMessageSession messageSession)
        {
            this.orderQuery = orderQuery;
            this.messageSession = messageSession;
        }

        public async Task<SubmitOrderStatus> Handle(SubmitOrder request, CancellationToken cancellationToken)
        {
            var order = await this.orderQuery.Get(request.Key);
            if (order == default)
            {
                return SubmitOrderStatus.NotFound;
            }

            if (!order.IsDraft())
            {
                return SubmitOrderStatus.InvalidState;
            }

            await this.messageSession.Send(new Demo.Order.ServiceContracts.SubmitOrder {Key = request.Key});
            return SubmitOrderStatus.Success;
        }
    }
}