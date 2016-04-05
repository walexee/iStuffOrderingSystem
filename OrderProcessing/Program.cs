using Akka.Actor;
using Akka.Routing;
using Common.Helpers;
using OrderProcessing.Actors;
using System;

namespace OrderProcessing
{
    class Program
    {
        static void Main(string[] args)
        {
            ColorConsole.WriteYellow(">>> ORDER PROCESSING CONSOLE <<<");

            var system = ActorSystem.Create("iStuffOrderingSystem");

            var shipping = system.ActorOf(Props.Empty.WithRouter(FromConfig.Instance), "shipping");
            var inventory = system.ActorOf(Props.Empty.WithRouter(FromConfig.Instance), "inventory");

            system.ActorOf(Props.Create<OrderPlacement>(), "orderPlacement");
            system.ActorOf(Props.Create<StatusUpdate>(shipping), "statusUpdateReceiver");
            system.ActorOf(Props.Create<OrderProcessingFlow>(inventory), "orderProcessing");

            system.WhenTerminated.Wait();
        }
    }
}
