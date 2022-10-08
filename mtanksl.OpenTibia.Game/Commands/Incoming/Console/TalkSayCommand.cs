﻿using OpenTibia.Common.Objects;
using System;

namespace OpenTibia.Game.Commands
{
    public class TalkSayCommand : Command
    {
        public TalkSayCommand(Player player, string message)
        {
            Player = player;

            Message = message;
        }

        public Player Player { get; set; }

        public string Message { get; set; }

        public override Promise Execute(Context context)
        {
            return context.AddCommand(new PlayerSayCommand(Player, Message) );
        }
    }
}