using System;

namespace Common.Messages
{
    public class FulfillmentRequest
    {
        public FulfillmentRequest(Order order)
        {
            Id = Guid.NewGuid();
            TimeStamp = DateTime.Now;
            Order = order;
        }

        public Order Order { get; private set; }

        public Guid Id { get; private set; }

        public DateTime TimeStamp { get; private set; }
    }
}
