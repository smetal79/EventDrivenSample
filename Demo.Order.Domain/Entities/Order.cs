using System;

namespace Demo.Order.Domain.Entities
{
    public abstract class Order : IEntity
    {
        public decimal Total { get; protected set; }
        public Guid Key { get; protected set; }
    }
}