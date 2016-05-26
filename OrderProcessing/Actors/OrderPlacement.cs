using Akka.Actor;
using Common.Helpers;
using System.Collections.Generic;
using Akka.Routing;
using Common.Messages;

namespace OrderProcessing.Actors
{
    public class OrderPlacement : ReceiveActor
    {
        private readonly Queue<Order> _backOrders;

        public OrderPlacement()
        {
            _backOrders = new Queue<Order>();

            ProcessOrder();
        }

        private void AlwaysProcessing()
        {
            Receive<LowInventoryLevel>(message =>
            {
                Become(ProcessAsBackOrder);
            });

            Receive<InventoryReplenished>(message =>
            {

                Become(ProcessOrder);

                while (_backOrders.Count > 0)
                {
                    Self.Tell(_backOrders.Dequeue());
                }
            });

            Receive<BackOrder>(message =>
            {
                ColorConsole.WriteYellow("Order {0} is back ordered.", message.Order.Id);
                _backOrders.Enqueue(message.Order);
            });
        }

        private void ProcessAsBackOrder()
        {
            AlwaysProcessing();

            Receive<Order>(order =>
            {
                ColorConsole.WriteYellow("Order {0} is back ordered.", order.Id);
                _backOrders.Enqueue(order);
            });
        }

        private void ProcessOrder()
        {
            AlwaysProcessing();

            Receive<Order>(order =>
            {
                ColorConsole.WriteYellow("Order {0} has been placed.", order.Id);

                Sender.Tell(new FulfillmentRequest(order));
            });
        }
    }
}
