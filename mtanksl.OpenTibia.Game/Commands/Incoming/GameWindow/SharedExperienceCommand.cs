﻿using OpenTibia.Common.Objects;
using System;

namespace OpenTibia.Game.Commands
{
    public class SharedExperienceCommand : Command
    {
        public SharedExperienceCommand(Player player, bool enabled)
        {
            Player = player;

            Enabled = enabled;
        }

        public Player Player { get; set; }

        public bool Enabled { get; set; }

        public override Promise Execute(Context context)
        {
            return Promise.Run(resolve =>
            {


                resolve(context);
            } );
        }
    }
}