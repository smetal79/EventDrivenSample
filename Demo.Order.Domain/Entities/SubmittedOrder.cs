namespace Demo.Order.Domain.Entities
{
    public sealed class SubmittedOrder : Order
    {

        private SubmittedOrder()
        {}

        public SubmittedOrder(DraftOrder draftOrder)
        {
            this.Total = draftOrder.Total;
            this.Key = draftOrder.Key;
        }

        public ProcessedOrder PaymentProcessed()
            => new ProcessedOrder(this);
    }
}