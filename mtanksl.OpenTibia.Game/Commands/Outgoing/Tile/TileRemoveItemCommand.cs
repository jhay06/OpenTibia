﻿using OpenTibia.Common.Objects;
using OpenTibia.Network.Packets.Outgoing;
using System;

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

        public override Promise Execute(Context context)
        {
            return Promise.Run(resolve =>
            {
                byte index = Tile.GetIndex(Item);

                Tile.RemoveContent(index);

                foreach (var observer in context.Server.GameObjects.GetPlayers() )
                {
                    if (observer.Tile.Position.CanSee(Tile.Position) )
                    {
                        context.AddPacket(observer.Client.Connection, new ThingRemoveOutgoingPacket(Tile.Position, index) );
                    }
                }

                resolve(context);
            } );
        }
    }
}