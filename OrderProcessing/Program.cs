﻿using System;
using Akka.Actor;
using Akka.Routing;
using Common.Helpers;
using Common.Messages;
using OrderProcessing.Actors;

namespace OrderProcessing
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Order Processing Service";
            //ColorConsole.WriteYellow(">>> ORDER PROCESSING CONSOLE <<<");

            var system = ActorSystem.Create("iStuffOrderingSystem");

            var shipping = system.ActorOf(Props.Empty.WithRouter(FromConfig.Instance), "shipping");
            var inventory = system.ActorOf(Props.Empty.WithRouter(FromConfig.Instance), "inventory");
            //var client = system.ActorOf(Props.Empty.WithRouter(FromConfig.Instance), "client");

            system.ActorOf(Props.Create<OrderPlacement>(), "orderPlacement");
            system.ActorOf(Props.Create<StatusUpdate>(shipping), "statusUpdateReceiver");
            system.ActorOf(Props.Create<OrderProcessingFlow>(inventory), "orderProcessing");

            system.WhenTerminated.Wait();
        }
    }
}
