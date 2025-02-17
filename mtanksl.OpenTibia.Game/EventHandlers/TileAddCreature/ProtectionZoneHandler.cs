﻿using OpenTibia.Common.Objects;
using OpenTibia.Common.Structures;
using OpenTibia.Game.Commands;
using OpenTibia.Game.EventHandlers;
using OpenTibia.Game.Events;
using OpenTibia.Network.Packets.Outgoing;

namespace OpenTibia.Game.CommandHandlers
{
    public class ProtectionZoneHandler : EventHandler<TileAddCreatureEventArgs>
    {
        public override Promise Handle(TileAddCreatureEventArgs e)
        {
            if (e.Creature is Player player)
            {
                if (e.Tile.ProtectionZone)
                {
                    if ( !player.HasSpecialCondition(SpecialCondition.ProtectionZone) )
                    {
                        player.AddSpecialCondition(SpecialCondition.ProtectionZone);

                        Context.AddPacket(player.Client.Connection, new SetSpecialConditionOutgoingPacket(player.SpecialConditions) );
                    }
                }
                else
                {
                    if (player.HasSpecialCondition(SpecialCondition.ProtectionZone) )
                    {
                        player.RemoveSpecialCondition(SpecialCondition.ProtectionZone);

                        Context.AddPacket(player.Client.Connection, new SetSpecialConditionOutgoingPacket(player.SpecialConditions) );
                    }
                }
            }

            return Promise.Completed;
        }
    }
}