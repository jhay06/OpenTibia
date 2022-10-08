﻿using OpenTibia.Common.Objects;
using System;

namespace OpenTibia.Game.Commands
{
    public class TradeWithFromContainerCommand : TradeWithCommand
    {
        public TradeWithFromContainerCommand(Player player, byte fromContainerId, byte fromContainerIndex, ushort itemId, uint creatureId) : base(player)
        {
            FromContainerId = fromContainerId;

            FromContainerIndex = fromContainerIndex;

            ItemId = itemId;

            ToCreatureId = creatureId;
        }

        public byte FromContainerId { get; set; }

        public byte FromContainerIndex { get; set; }

        public ushort ItemId { get; set; }

        public uint ToCreatureId { get; set; }

        public override Promise Execute(Context context)
        {
            return Promise.Run(resolve =>
            {
                Container fromContainer = Player.Client.ContainerCollection.GetContainer(FromContainerId);

                if (fromContainer != null)
                {
                    Item fromItem = fromContainer.GetContent(FromContainerIndex) as Item;

                    if (fromItem != null && fromItem.Metadata.TibiaId == ItemId)
                    {
                        Player toPlayer = context.Server.GameObjects.GetGameObject<Player>(ToCreatureId);

                        if (toPlayer != null && toPlayer != Player)
                        {
                            resolve(context);
                        }
                    }
                }            
            } );
        }
    }
}