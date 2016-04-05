using Akka.Actor;
using Akka.Routing;
using Common.Helpers;

namespace Shipping
{
    class Program
    {
        static void Main(string[] args)
        {
            ColorConsole.WriteYellow(">>> SHIPPING CONSOLE <<<");

            var system = ActorSystem.Create("iStuffOrderingSystem");
            var statusUpdateReceiver = system.ActorOf(Props.Empty.WithRouter(FromConfig.Instance), "statusUpdateReceiver");

            system.ActorOf(Props.Create(() => new ShippingProcessor(statusUpdateReceiver)), "shipping");

            system.WhenTerminated.Wait();
        }
    }
}
