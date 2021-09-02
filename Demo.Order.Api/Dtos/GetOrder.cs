using System;
using MediatR;

namespace Demo.Order.Api.Dtos
{
    internal sealed class GetOrder : IRequest<OrderResponse>
    {
        public Guid Key { get;  }

        public GetOrder(Guid key)
        {
            Key = key;
        }
    }
}