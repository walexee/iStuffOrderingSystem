using Akka.Actor;
using Common.Helpers;
using Common.Messages;

namespace Client
{
    public class StatusUpdateActor : ReceiveActor
    {
        public StatusUpdateActor()
        {
            Receive<OrderFulfilled>(message => 
            {
                ColorConsole.WriteGreen("Order ({0}) has been FULFILLED.", message.OrderId);
            });

            Receive<OrderShipped>(message =>
            {
                ColorConsole.WriteGreen("Order ({0}) has been SHIPPED.", message.OrderId);
            });

            Receive<BackOrder>(msg =>
            {
                ColorConsole.WriteYellow("order ({0}) is BACK ORDERED", msg.Order.Id);
            });
        }
    }
}
