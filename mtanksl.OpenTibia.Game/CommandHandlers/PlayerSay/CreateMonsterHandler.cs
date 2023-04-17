﻿using OpenTibia.Common.Objects;
using OpenTibia.Common.Structures;
using OpenTibia.Game.Commands;
using System;

namespace OpenTibia.Game.CommandHandlers
{
    public class CreateMonsterHandler : CommandHandler<PlayerSayCommand>
    {
        public override Promise Handle(Func<Promise> next, PlayerSayCommand command)
        {
            if (command.Message.StartsWith("/m ") )
            {
                string name = command.Message.Substring(3);

                Tile toTile = Context.Server.Map.GetTile(command.Player.Tile.Position.Offset(command.Player.Direction) );

                if (toTile != null)
                {
                    return Context.AddCommand(new TileCreateMonsterCommand(toTile, name) ).Then( (moster) =>
                    {
                        return Context.AddCommand(new ShowMagicEffectCommand(toTile.Position, MagicEffectType.BlueShimmer) );
                    } );
                }

                return Context.AddCommand(new ShowMagicEffectCommand(command.Player.Tile.Position, MagicEffectType.Puff) );
            }

            return next();
        }
    }
}