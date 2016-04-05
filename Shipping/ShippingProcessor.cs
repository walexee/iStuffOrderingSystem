using Akka.Actor;
using Common.Messages;

namespace Shipping
{
    public class ShippingProcessor : ReceiveActor
    {
        private readonly IActorRef _statusUpdateReceiver;

        public ShippingProcessor(IActorRef statusUpdateReceiver)
        {
            _statusUpdateReceiver = statusUpdateReceiver;

            Receive<ShippingRequest>(req =>
            {
                _statusUpdateReceiver.Tell(new OrderShipped(req.OrderId));
            });
        }
    }
}
