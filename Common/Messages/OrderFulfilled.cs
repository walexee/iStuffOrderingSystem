
using System;

namespace Common.Messages
{
    public class OrderFulfilled
    {
        public OrderFulfilled(Guid orderId)
        {
            Id = Guid.NewGuid();
            OrderId = orderId;
            TimeStamp = DateTime.Now;
        }

        public Guid OrderId { get; private set; }

        public DateTime TimeStamp { get; private set; }

        public Guid Id { get; private set; }
    }
}
