﻿using OpenTibia.Game.Commands;
using OpenTibia.Game.Plugins;
using System;

namespace OpenTibia.Game.CommandHandlers
{
    public class PlayerSayScriptingHandler : CommandHandler<PlayerSayCommand>
    {
        public override Promise Handle(Func<Promise> next, PlayerSayCommand command)
        {
            PlayerSayPlugin plugin = Context.Server.Plugins.GetPlayerSayPlugin(command.Message);

            if (plugin != null)
            {
                return plugin.OnSay(command.Player, command.Message).Then( (result) =>
                {
                    if (result)
                    {
                        return Promise.Completed;
                    }

                    return next();
                } );
            }

            return next();
        }
    }
}