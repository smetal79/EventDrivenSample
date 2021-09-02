namespace Demo.Order.Domain.Entities
{
    public sealed class ProcessedOrder : Order
    {
        private ProcessedOrder()
        {}
        
        public ProcessedOrder(SubmittedOrder submittedOrder)
        {
            Key = submittedOrder.Key;
            Total = submittedOrder.Total;
        }
    }
}