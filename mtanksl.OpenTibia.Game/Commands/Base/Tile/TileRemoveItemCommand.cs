﻿using OpenTibia.Common.Events;
using OpenTibia.Common.Objects;
using OpenTibia.Network.Packets.Outgoing;

namespace OpenTibia.Game.Commands
{
    public class TileRemoveItemCommand : Command
    {
        public TileRemoveItemCommand(Tile tile, Item item)
        {
            Tile = tile;

            Item = item;
        }

        public Tile Tile { get; set; }

        public Item Item { get; set; }

        public override void Execute(Context context)
        {
            byte index = Tile.GetIndex(Item);

            Tile.RemoveContent(index);

            foreach (var observer in context.Server.GameObjects.GetPlayers() )
            {
                if (observer.Tile.Position.CanSee(Tile.Position) )
                {
                    context.WritePacket(observer.Client.Connection, new ThingRemoveOutgoingPacket(Tile.Position, index) );
                }
            }

            context.AddEvent(new TileRemoveItemEventArgs(Tile, Item, index) );

            base.OnCompleted(context);
        }
    }
}