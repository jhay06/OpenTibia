﻿using OpenTibia.Common.Objects;
using OpenTibia.Game.Commands;

namespace OpenTibia.Game.Plugins
{
    public class LuaScriptingPlayerRotateItemPlugin : PlayerRotateItemPlugin
    {
        private string fileName;

        public LuaScriptingPlayerRotateItemPlugin(string fileName)
        {
            this.fileName = fileName;
        }

        private LuaScope script;

        public override void Start()
        {
            script = Context.Server.LuaScripts.Create(Context.Server.PathResolver.GetFullPath("data/plugins/lib.lua"), Context.Server.PathResolver.GetFullPath("data/plugins/actions/lib.lua"), Context.Server.PathResolver.GetFullPath(fileName) );
        }

        public override PromiseResult<bool> OnRotateItem(Player player, Item item)
        {
            return script.CallFunction("onrotateitem", player, item).Then(result =>
            {
                return (bool)result[0] ? Promise.FromResultAsBooleanTrue : Promise.FromResultAsBooleanFalse;
            } );
        }

        public override void Stop()
        {
            script.Dispose();
        }
    }
}