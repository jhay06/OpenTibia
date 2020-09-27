﻿using OpenTibia.Common.Objects;

namespace OpenTibia.Game.Commands
{
    public class YellCommand : Command
    {
        public YellCommand(Player player, string message)
        {
            Player = player;

            Message = message;
        }

        public Player Player { get; set; }

        public string Message { get; set; }

        public override void Execute(Context context)
        {
            Command command = context.TransformCommand(new PlayerYellCommand(Player, Message) );

            command.Completed += (s, e) =>
            {
                base.OnCompleted(e.Context);
            };

            command.Execute(context);
        }
    }
}