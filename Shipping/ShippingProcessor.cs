using Akka.Actor;
using Common.Helpers;
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

            Receive<LowInventoryLevel>(msg =>
            {
                ColorConsole.WriteBlue("Inventory is low with count of {0}", msg.StockCount);
            });

            Receive<InventoryReplenished>(msg =>
            {
                ColorConsole.WriteBlue("Inventory replenished by {0}", msg.StockCount);
            });
        }
    }
}
