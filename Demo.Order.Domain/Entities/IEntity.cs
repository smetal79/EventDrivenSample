using System;

namespace Demo.Order.Domain.Entities
{
    public interface IEntity
    {
        Guid Key { get;  }
    }
}