using System;
using Demo.NServiceBus.ServiceContracts;

namespace Demo.Payment.ServiceContracts
{
    public sealed class MakePayment : IAmCommand
    {
        public Guid OrderKey { get; set; }
        
        public decimal Total { get; set; }
    }
}