﻿using OpenTibia.Common.Objects;
using OpenTibia.Game.Commands;
using OpenTibia.Game.Events;
using OpenTibia.Network.Packets.Outgoing;
using System;

namespace OpenTibia.Game.Components
{
    public class PlayerPingBehaviour : Behaviour
    {
        private DateTime lastPingRequest = DateTime.UtcNow;

        private DateTime lastPingResponse = DateTime.UtcNow;

        public void SetLastPingResponse()
        {
            DateTime now = DateTime.UtcNow;

            if (now > lastPingRequest)
            {
                lastPingResponse = now;
            }
        }

        public int GetLatency()
        {
            return (int)(lastPingResponse - lastPingRequest).TotalMilliseconds;
        }

        private Guid globalPing;

        public override void Start()
        {
            Player player = (Player)GameObject;

            globalPing = Context.Server.EventHandlers.Subscribe<GlobalPingEventArgs>( (context, e) =>
            {
                if ( (DateTime.UtcNow - lastPingResponse).TotalMinutes >= 1)
                {
                    return Context.AddCommand(new ParseLogOutCommand(player) );
                }
                else
                {
                    lastPingRequest = DateTime.UtcNow;

                    Context.AddPacket(player.Client.Connection, new PingOutgoingPacket() );
                }

                return Promise.Completed;
            } );
        }

        public override void Stop()
        {
            Context.Server.EventHandlers.Unsubscribe<GlobalPingEventArgs>(globalPing);
        }
    }
}