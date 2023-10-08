﻿using mtanksl.OpenTibia.Game.Plugins;
using OpenTibia.Common.Objects;
using OpenTibia.Common.Structures;
using OpenTibia.Game.Commands;
using OpenTibia.Game.Components;
using System;

namespace mtanksl.OpenTibia.GameData.Plugins.Spells
{
    public class GreatLightSpellPlugin : SpellPlugin
    {
        public GreatLightSpellPlugin(Spell spell) : base(spell)
        {

        }

        public override void Start()
        {

        }

        public override PromiseResult<bool> OnCasting(Player player, Creature target, string message)
        {
            return Promise.FromResult(true);
        }

        public override Promise OnCast(Player player, Creature target, string message)
        {
            return Context.AddCommand(new ShowMagicEffectCommand(player.Tile.Position, MagicEffectType.BlueShimmer) ).Then( () =>
            {
                return Context.AddCommand(new CreatureAddConditionCommand(player, 
                            
                    new LightCondition(new Light(8, 215), new TimeSpan(0, 11, 35) ) ) );
            } );
        }
             
        public override void Stop()
        {
            
        }
    }
}