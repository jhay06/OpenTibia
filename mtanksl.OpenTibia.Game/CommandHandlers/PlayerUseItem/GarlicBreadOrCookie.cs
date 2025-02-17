﻿using OpenTibia.Common.Structures;
using OpenTibia.Game.Commands;
using System;
using System.Collections.Generic;

namespace OpenTibia.Game.CommandHandlers
{
    public class GarlicBreadOrCookie : CommandHandler<PlayerUseItemCommand>
    {
        private HashSet<ushort> garlicBreadOrCookie = new HashSet<ushort>() { 9111, 9116 };

        private string sound = "After taking a small bite you decide that you don't want to eat that.";

        public override Promise Handle(Func<Promise> next, PlayerUseItemCommand command)
        {
            if (garlicBreadOrCookie.Contains(command.Item.Metadata.OpenTibiaId) )
            {
                return Context.AddCommand(new ShowTextCommand(command.Player, TalkType.MonsterSay, sound) );
            }

            return next();
        }
    }
}