using System;

namespace Common.Messages
{
    public class BackOrder
    {
        public BackOrder(Order order)
        {
            Id = Guid.NewGuid();
            Order = order;
        }

        public Guid Id { get; private set; }

        public Order Order { get; private set; }
    }
}
