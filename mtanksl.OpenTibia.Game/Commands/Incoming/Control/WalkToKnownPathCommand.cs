﻿using OpenTibia.Common.Objects;
using OpenTibia.Common.Structures;
using System.Collections.Generic;

namespace OpenTibia.Game.Commands
{
    public class WalkToKnownPathCommand : Command
    {
        public WalkToKnownPathCommand(Player player, MoveDirection[] moveDirections)
        {
            Player = player;

            MoveDirections = moveDirections;
        }

        public Player Player { get; set; }

        public MoveDirection[] MoveDirections { get; set; }

        public override void Execute(Context context)
        {
            List<Command> commands = new List<Command>();

            foreach (var moveDirection in MoveDirections)
            {
                commands.Add(new WalkCommand(Player, moveDirection) );
            }

            context.AddCommand(new SequenceCommand(commands.ToArray() ) ).Then(ctx =>
            {
                OnComplete(ctx);
            } );
        }
    }
}