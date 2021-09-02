using System.Threading;
using System.Threading.Tasks;
using Demo.Order.Api.Dtos;
using Demo.Order.Api.Queries;
using MediatR;

namespace Demo.Order.Api.RequestHandlers
{
    internal sealed class GetOrderHandler : IRequestHandler<GetOrder, OrderResponse>
    {
        private readonly IOrderQuery orderQuery;

        public GetOrderHandler(IOrderQuery orderQuery)
            => this.orderQuery = orderQuery;

        public async Task<OrderResponse> Handle(GetOrder request, CancellationToken cancellationToken)
            => await this.orderQuery.Get(request.Key);
    }
}