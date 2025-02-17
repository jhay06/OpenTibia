﻿using OpenTibia.Common.Structures;
using OpenTibia.Data.Models;
using OpenTibia.Game.Commands;
using OpenTibia.Network.Packets.Outgoing;
using System;

namespace OpenTibia.Game.CommandHandlers
{
    public class UnBanPlayerHandler : CommandHandler<PlayerSayCommand>
    {
        public override Promise Handle(Func<Promise> next, PlayerSayCommand command)
        {
            if (command.Message.StartsWith("/unban ") && command.Player.Rank == Rank.Gamemaster)
            {
                string name = command.Message.Substring(7);

                DbPlayer dbPlayer = Context.Database.PlayerRepository.GetPlayerByName(name);

                if (dbPlayer != null)
                {
                    DbBan dbBan = Context.Database.BanRepository.GetBanByPlayerId(dbPlayer.Id);

                    if (dbBan != null)
                    {
                        Context.Database.BanRepository.RemoveBan(dbBan);

                        Context.Database.Commit();
                    }

                    Context.AddPacket(command.Player.Client.Connection, new ShowWindowTextOutgoingPacket(TextColor.GreenCenterGameWindowAndServerLog, dbPlayer.Name + " has been unbanned.") );

                    return Promise.Completed;
                }

                return Context.AddCommand(new ShowMagicEffectCommand(command.Player.Tile.Position, MagicEffectType.Puff) );
            }

            return next();
        }
    }
}