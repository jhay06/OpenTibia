﻿using OpenTibia.Common.Objects;
using OpenTibia.Game;
using OpenTibia.Game.Commands;

namespace mtanksl.OpenTibia.Game.Plugins
{
    public class LuaScriptingPlayerMoveItemPlugin : PlayerMoveItemPlugin
    {
        private string fileName;

        public LuaScriptingPlayerMoveItemPlugin(string fileName)
        {
            this.fileName = fileName;
        }

        private LuaScope script;

        public override void Start()
        {
            script = Context.Server.LuaScripts.Create(Context.Server.PathResolver.GetFullPath("data/plugins/lib.lua"), Context.Server.PathResolver.GetFullPath("data/plugins/actions/lib.lua"), Context.Server.PathResolver.GetFullPath(fileName) );
        }

        public override PromiseResult<bool> OnMoveItem(Player player, Item item, IContainer toContainer, byte toIndex, byte count)
        {
            return script.CallFunction("onmoveitem", player, item, toContainer, toIndex, count).Then(result =>
            {
                return Promise.FromResult ( (bool)result[0] );
            } );
        }

        public override void Stop()
        {
            script.Dispose();
        }
    }
}