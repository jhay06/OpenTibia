﻿using OpenTibia.Common.Objects;
using OpenTibia.Common.Structures;
using OpenTibia.Game.Events;
using OpenTibia.Network.Packets.Outgoing;

namespace OpenTibia.Game.Commands
{
    public class PlayerWhisperCommand : Command
    {
        public PlayerWhisperCommand(Player player, string message)
        {
            Player = player;

            Message = message;
        }

        public Player Player { get; set; }

        public string Message { get; set; }

        public override Promise Execute()
        {
            ShowTextOutgoingPacket showTextOutgoingPacket = new ShowTextOutgoingPacket(Context.Server.Channels.GenerateStatementId(Player.DatabasePlayerId, Message), Player.Name, Player.Level, TalkType.Whisper, Player.Tile.Position, Message);

            ShowTextOutgoingPacket showTextOutgoingPacket2 = new ShowTextOutgoingPacket(0, Player.Name, Player.Level, TalkType.Whisper, Player.Tile.Position, "pspsps");

            foreach (var observer in Context.Server.Map.GetObserversOfTypeCreature(Player.Tile.Position) )
            {
                if (observer.Tile.Position.CanHearWhisper(Player.Tile.Position) )
                {
                    if (observer is Player player)
                    {
                        Context.AddPacket(player.Client.Connection, showTextOutgoingPacket);
                    }

                    Context.AddEvent(observer, new PlayerWhisperEventArgs(Player, Message) );
                }
                else if (observer.Tile.Position.CanHearSay(Player.Tile.Position) )
                {
                    if (observer is Player player)
                    {
                        Context.AddPacket(player.Client.Connection, showTextOutgoingPacket2);
                    }

                    Context.AddEvent(observer, new PlayerWhisperEventArgs(Player, Message) );
                }
            }

            Context.AddEvent(new PlayerWhisperEventArgs(Player, Message) );

            return Promise.Completed;
        }
    }
}