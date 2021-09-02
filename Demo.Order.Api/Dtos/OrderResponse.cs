using System;

namespace Demo.Order.Api.Dtos
{
    public sealed class OrderResponse
    {
        public Guid Key { get; set; }
        
        public decimal Total { get; set; }

        public string Status => 
            Discriminator == default 
                ? Discriminator 
                : Discriminator.Replace("Order", string.Empty);

        internal string Discriminator { get; set; }

        internal bool IsDraft()
            => Discriminator.StartsWith("Draft", StringComparison.OrdinalIgnoreCase);

    }
}