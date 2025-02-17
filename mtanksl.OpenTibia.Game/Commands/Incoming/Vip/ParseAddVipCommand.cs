﻿using OpenTibia.Common.Objects;
using OpenTibia.Data.Models;
using OpenTibia.Network.Packets.Outgoing;

namespace OpenTibia.Game.Commands
{
    public class ParseAddVipCommand : Command
    {
        public ParseAddVipCommand(Player player, string name)
        {
            Player = player;

            Name = name;
        }

        public Player Player { get; set; }

        public string Name { get; set; }

        public override Promise Execute()
        {
            DbPlayer dbPlayer = Context.Database.PlayerRepository.GetPlayerByName(Name);

            if (dbPlayer != null && dbPlayer.Id != Player.DatabasePlayerId)
            {
                if (Player.Client.Vips.AddVip(dbPlayer.Id, dbPlayer.Name) )
                {
                    Context.AddPacket(Player.Client.Connection, new VipOutgoingPacket( (uint)dbPlayer.Id, dbPlayer.Name, false) );

                    return Promise.Completed;
                }
            }

            return Promise.Break;
        }
    }
}