using System;
using Akka.Actor;
using Common.Messages;
using System.Threading;
using System.Threading.Tasks;
using Common.Helpers;

namespace InventoryManagement
{
    public class InventoryManager : ReceiveActor
    {
        private int _inventoryLevel = 100;
        private const int ProductId = 567437;
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
                    _orderProcessing.Tell(new BackOrder(req.Order));
                }

                if (_inventoryLevel < 10)
                {
                    TellInventoryLevel(new LowInventoryLevel(ProductId, _inventoryLevel));
                }
            });

            Receive<InventoryReplenished>(message =>
            {
                ColorConsole.WriteBlue("Inventory replenished by " + message.StockCount);
                _inventoryLevel += message.StockCount;
                TellInventoryLevel(message);
            });
        }

        private void TellInventoryLevel<T>(T inventoryLevel) where T : InventoryLevel
        {
            _shipping.Tell(inventoryLevel);
            _orderProcessing.Tell(inventoryLevel);
        }
    }
}
