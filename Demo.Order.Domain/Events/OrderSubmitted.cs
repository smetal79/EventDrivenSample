using System;
using Demo.Order.Domain.Entities;
using Demo.Order.Domain.Infrastructure;

namespace Demo.Order.Domain.Events
{
    public class OrderSubmitted : IDomainEvent
    {
        public OrderSubmitted(SubmittedOrder submittedOrder)
        {
            OrderKey = submittedOrder.Key;
            Total = submittedOrder.Total;
        }
        
        public Guid OrderKey { get; }
        
        public decimal Total { get; }
    }
}