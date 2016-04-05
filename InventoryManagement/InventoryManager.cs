using Akka.Actor;
using Akka.Routing;
using System.Threading;
using System.Threading.Tasks;
using Common.Messages;

namespace InventoryManagement
{
    public class InventoryManager : ReceiveActor
    {
        private int _inventoryLevel = 100;
        private const int PRODUCT_ID = 567437;
        private readonly IActorRef _orderProcessing;
        private readonly IActorRef _shipping;
        private readonly IActorRef _statusUpdateReceiver;

        public InventoryManager(IActorRef orderProcessing, IActorRef shipping, IActorRef statusUpdateReceiver)
        {
            _orderProcessing = orderProcessing;
            _shipping = shipping;
            _statusUpdateReceiver = statusUpdateReceiver;

            Receive<FulfillmentRequest>(req =>
            {
                if(_inventoryLevel >= req.Order.Count)
                {
                    _inventoryLevel -= req.Order.Count;
                    _statusUpdateReceiver.Tell(new OrderFulfilled(req.Order.Id));
                }
                else 
                {
                    //Back ordered: Send back for reprocessing
                    _orderProcessing.Forward(req.Order);
                }

                if (_inventoryLevel >= 10)
                    return;

                TellInventoryLevel(new LowInventoryLevel(PRODUCT_ID, _inventoryLevel));

                ReplenishInventory(100);

                TellInventoryLevel(new InventoryReplenished(PRODUCT_ID, _inventoryLevel));
            });

            
        }

        private void ReplenishInventory(int requestedAmount)
        {
            _inventoryLevel += requestedAmount;
        }

        private void TellInventoryLevel<T>(T inventoryLevel) where T : InventoryLevel
        {
            _shipping.Tell(inventoryLevel);
            _orderProcessing.Tell(inventoryLevel);
        }
    }
}
