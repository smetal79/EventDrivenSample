using System;
using MediatR;

namespace Demo.Order.Api.Dtos
{
    internal sealed class SubmitOrder : IRequest<SubmitOrderStatus>
    {
        public Guid Key { get;  }

        public SubmitOrder(Guid key)
        {
            Key = key;
        }
    }
}