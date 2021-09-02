using System;
using Demo.NServiceBus.ServiceContracts;

namespace Demo.Order.ServiceContracts
{
    public class CreateOrder : IAmCommand
    {
        public Guid Key { get; set; }
        
        public decimal Total { get; set; }
    }
}