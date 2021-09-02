using System;
using Demo.Order.Domain.Events;
using Demo.Order.Domain.Infrastructure;

namespace Demo.Order.Domain.Entities
{
    public sealed class DraftOrder : Order
    {
        public DraftOrder(Guid key, decimal total)
        {
            Key = key;
            Total = total;
        }

        private DraftOrder()
        {}

        public SubmittedOrder Submit()
        {
            var submittedOrder = new SubmittedOrder(this);
            DomainEvents.Raise(new OrderSubmitted(submittedOrder));
            return submittedOrder;
        }
    }
}