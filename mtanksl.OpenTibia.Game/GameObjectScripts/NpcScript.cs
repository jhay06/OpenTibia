﻿using OpenTibia.Common.Objects;
using OpenTibia.Common.Structures;
using OpenTibia.Game.Components;
using System;

namespace OpenTibia.Game.GameObjectScripts
{
    public class NpcScript : GameObjectScript<string, Npc>
    {
        public override string Key
        {
            get
            {
                return "";
            }
        }

        public override void Start(Npc npc)
        {
            if (npc.Metadata.Sentences != null)
            {
                Context.Server.GameObjectComponents.AddComponent(npc, new CreatureTalkBehaviour(TimeSpan.FromSeconds(30), TalkType.Say, npc.Metadata.Sentences) );
            }

            Context.Server.GameObjectComponents.AddComponent(npc, new CreatureWalkBehaviour(new RandomWalkStrategy() ) );
        }

        public override void Stop(Npc npc)
        {

        }
    }
}