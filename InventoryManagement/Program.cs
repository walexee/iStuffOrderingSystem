using Akka.Actor;
using Akka.Routing;
using Common.Helpers;
using System;

namespace InventoryManagement
{
    class Program
    {
        static void Main(string[] args)
        {
            ColorConsole.WriteYellow(">>> INVENTORY CONSOLE <<<");

            var system = ActorSystem.Create("iStuffOrderingSystem");
            var shipping = system.ActorOf(Props.Empty.WithRouter(FromConfig.Instance), "shipping");
            var orderProcessing = system.ActorOf(Props.Empty.WithRouter(FromConfig.Instance), "orderProcessing");
            var statusUpdateReceiver = system.ActorOf(Props.Empty.WithRouter(FromConfig.Instance), "statusUpdateReceiver");

            system.ActorOf(Props.Create(() => new InventoryManager(orderProcessing, shipping, statusUpdateReceiver)), "inventory");

            system.WhenTerminated.Wait();
        }
    }
}
