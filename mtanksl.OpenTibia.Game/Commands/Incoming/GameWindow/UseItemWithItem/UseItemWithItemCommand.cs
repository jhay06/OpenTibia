﻿using OpenTibia.Common.Objects;
using OpenTibia.Common.Structures;
using System;

namespace OpenTibia.Game.Commands
{
    public abstract class UseItemWithItemCommand : Command
    {
        public UseItemWithItemCommand(Player player)
        {
            Player = player;
        }

        public Player Player { get; set; }

        protected bool IsUseable(Context context, Item fromItem)
        {
            if ( !fromItem.Metadata.Flags.Is(ItemMetadataFlags.Useable) )
            {
                return false;
            }

            return true;
        }
    }
}