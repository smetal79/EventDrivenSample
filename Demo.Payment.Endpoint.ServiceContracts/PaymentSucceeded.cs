using System;
using Demo.NServiceBus.ServiceContracts;

namespace Demo.Payment.ServiceContracts
{
    public sealed class PaymentSucceeded : IAmEvent
    {
        public Guid OrderKey { get; set; }
    }
}