using System;

namespace Common.Messages
{
    public class OrderShipped
    {
        public OrderShipped(Guid orderId)
        {
            var shippers = new string[] { "FedEx", "UPS", "USPS", "DHL" };

            Id = Guid.NewGuid();
            OrderId = orderId;
            Shipper = shippers[new Random().Next(4)];
        }

        public Guid Id { get; private set; }

        public Guid OrderId { get; private set; }

        public string Shipper { get; private set; }
    }
}
