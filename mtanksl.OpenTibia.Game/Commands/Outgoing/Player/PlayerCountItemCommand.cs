﻿using OpenTibia.Common.Objects;
using OpenTibia.Common.Structures;

namespace OpenTibia.Game.Commands
{
    public class PlayerCountItemCommand : CommandResult<int>
    {
        public PlayerCountItemCommand(Player player, ushort openTibiaId, byte type)
        {
            Player = player;

            OpenTibiaId = openTibiaId;

            Type = type;
        }

        public Player Player { get; set; }

        public ushort OpenTibiaId { get; set; }

        public byte Type { get; set; }

        public override PromiseResult<int> Execute()
        {
            int sum = Sum(Player.Inventory, OpenTibiaId);

            return Promise.FromResult(sum);
        }

        private int Sum(IContainer parent, ushort openTibiaId)
        {
            int sum = 0;

            foreach (Item content in parent.GetContents() )
            {
                if (content is Container container)
                {
                    sum += Sum(container, openTibiaId);
                }

                if (content.Metadata.OpenTibiaId == openTibiaId)
                {
                    if (content is StackableItem stackableItem)
                    {
                        sum += stackableItem.Count;
                    }
                    else if (content is FluidItem fluidItem)
                    {
                        if (fluidItem.FluidType == (FluidType)Type)
                        {
                            sum += 1;
                        }
                    }
                    else
                    {
                        sum += 1;
                    }
                }
            }

            return sum;
        }
    }
}