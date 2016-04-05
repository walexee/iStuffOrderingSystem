﻿using Akka.Actor;
using Common.Helpers;
using System;
using Common.Messages;

namespace OrderProcessing.Actors
{
    public class OrderProcessingFlow : ReceiveActor
    {
        private readonly IActorRef _orderPlacement;
        private readonly IActorRef _inventory;

        public OrderProcessingFlow(IActorRef inventory)
        {
            _inventory = inventory;
            _orderPlacement = Context.ActorOf(Props.Create<OrderPlacement>(), "orderPlacement");

            Receive<Order>(order =>
            {
                _orderPlacement.Tell(order);
            });

            Receive<FulfillmentRequest>(f =>
            {
                _inventory.Tell(f);
            });

            Receive<LowInventoryLevel>(evt =>
            {
                _orderPlacement.Tell(evt);
            });

            Receive<InventoryReplenished>(evt =>
            {
                _orderPlacement.Tell(evt);
            });

            Receive<Status.Failure>(f => {
                Console.WriteLine("Error contacting other nodes. Cause is: {0}", f.Cause.Message);
            });
        }
    }
}