using System;
using Demo.NServiceBus.ServiceContracts;

namespace Demo.Order.ServiceContracts
{
    public class SubmitOrder : IAmCommand
    {
        public Guid Key { get; set; }
    }
}