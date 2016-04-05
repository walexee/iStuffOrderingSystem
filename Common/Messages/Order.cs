using System;

namespace Common.Messages
{
    public class Order
    {
        public Order(int productId, int count, int userId, double amount)
        {
            Id = Guid.NewGuid();
            ProductId = productId;
            Count = count;
            UserId = userId;
            Amount = amount;
            TimeStamp = DateTime.Now;
        }

        public Guid Id { get; private set; }

        public int ProductId { get; private set; }

        public int UserId { get; private set; }

        public double Amount { get; private set; }

        public DateTime TimeStamp { get; private set; }

        public int Count { get; private set; }
    }
}
