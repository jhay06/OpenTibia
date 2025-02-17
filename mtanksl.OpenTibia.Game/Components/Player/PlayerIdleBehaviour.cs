﻿using OpenTibia.Common.Objects;
using OpenTibia.Common.Structures;
using OpenTibia.Game.Commands;
using OpenTibia.Game.Events;
using OpenTibia.Network.Packets.Outgoing;
using System;

namespace OpenTibia.Game.Components
{
    public class PlayerIdleBehaviour : Behaviour
    {
        private DateTime lastActionResponse = DateTime.UtcNow;

        public void SetLastActionResponse()
        {
            lastActionResponse = DateTime.UtcNow;
        }

        private Guid globalRealClockTick;

        public override void Start()
        {
            Player player = (Player)GameObject;

            globalRealClockTick = Context.Server.EventHandlers.Subscribe<GlobalRealClockTickEventArgs>( (context, e) =>
            {
                var totalMinutes = (DateTime.UtcNow - lastActionResponse).TotalMinutes;

                if (totalMinutes >= 16)
                {
                    return Context.AddCommand(new ParseLogOutCommand(player) );

                }
                else if (totalMinutes >= 15)
                {
                    Context.AddPacket(player.Client.Connection, new ShowWindowTextOutgoingPacket(TextColor.RedCenterGameWindowAndServerLog, "There was no variation in your behaviour for 15 minutes. You will be disconnected in one minute if there is no change in your actions until then.") );
                }

                return Promise.Completed;
            } );
        }

        public override void Stop()
        {
            Context.Server.EventHandlers.Unsubscribe<GlobalPingEventArgs>(globalRealClockTick);
        }
    }
}