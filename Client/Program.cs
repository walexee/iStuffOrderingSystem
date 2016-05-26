using Akka.Actor;
using Akka.Routing;
using Common.Helpers;
using System;
using Common.Messages;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Client Console";
            //ColorConsole.WriteYellow(">>> CLIENT CONSOLE <<<");

            var system = ActorSystem.Create("iStuffOrderingSystem");
            var orderProcessing = system.ActorOf(Props.Empty.WithRouter(FromConfig.Instance), "orderProcessing");
            
            system.ActorOf(Props.Create<StatusUpdateActor>(), "statusUpdateReceiver");

            ColorConsole.WriteYellow("Start ordering your iStuff by entering the number of iStuffs you want:");

            while (true)
            {
                var commnd = Console.ReadLine();

                if (string.IsNullOrEmpty(commnd) || commnd.ToLower() == "exit")
                {
                    break;
                }

                var itemCount = int.Parse(commnd);
                var order = new Order(123, itemCount, 123, 78);

                orderProcessing.Tell(order);
            }

            system.Terminate();
            system.WhenTerminated.Wait();
        }
    }
}
