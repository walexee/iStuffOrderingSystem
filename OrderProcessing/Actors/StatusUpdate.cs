using Akka.Actor;
using Common.Helpers;
using Common.Messages;

namespace OrderProcessing.Actors
{
    public class StatusUpdate : ReceiveActor
    {
        private readonly IActorRef _shipping;

        public StatusUpdate(IActorRef shipping)
        {
            _shipping = shipping;

            Receive<OrderFulfilled>(evt =>
            {
                ColorConsole.WriteGreen("Received OrderFulfilled for order ({0}).", evt.OrderId);
                _shipping.Tell(new ShippingRequest(evt.OrderId));
            });

            Receive<OrderShipped>(evt =>
            {
                ColorConsole.WriteGreen("Received OrderShipped for order ({0}).", evt.OrderId);
            });
        }
    }
}
