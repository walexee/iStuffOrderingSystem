using Akka.Actor;
using Akka.Routing;
using Common.Helpers;
using System;
using Common.Messages;

namespace InventoryManagement
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Inventory Management Service";
            //ColorConsole.WriteYellow(">>> INVENTORY CONSOLE <<<");

            var system = ActorSystem.Create("iStuffOrderingSystem");
            var shipping = system.ActorOf(Props.Empty.WithRouter(FromConfig.Instance), "shipping");
            var orderProcessing = system.ActorOf(Props.Empty.WithRouter(FromConfig.Instance), "orderProcessing");
            var statusUpdateReceiver = system.ActorOf(Props.Empty.WithRouter(FromConfig.Instance), "statusUpdateReceiver");
            var inventoryMgr = system.ActorOf(Props.Create(() => new InventoryManager(orderProcessing, shipping, statusUpdateReceiver)), "inventory");
            
            while (true)
            {
                var commnd = Console.ReadLine();

                if (string.IsNullOrEmpty(commnd) || commnd.ToLower() == "exit")
                {
                    break;
                }

                var stockCoount = int.Parse(commnd);
                var inventoryReplenished = new InventoryReplenished(1, stockCoount);

                inventoryMgr.Tell(inventoryReplenished);
            }

            system.WhenTerminated.Wait();
        }
    }
}
