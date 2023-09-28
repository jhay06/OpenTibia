﻿using OpenTibia.Common.Structures;
using OpenTibia.Game.Commands;
using OpenTibia.Game.Events;
using System;
using System.Collections.Generic;

namespace OpenTibia.Game.CommandHandlers
{
    public class CampfireHandler : EventHandlers.EventHandler<TileAddCreatureEventArgs>
    {
        private HashSet<ushort> campfires = new HashSet<ushort>() { 1423, 1424, 1425 };

        public override Promise Handle(TileAddCreatureEventArgs e)
        {
            foreach (var topItem in e.Tile.GetItems() )
            {
                if (campfires.Contains(topItem.Metadata.OpenTibiaId) )
                {
                    return Context.AddCommand(new CreatureAttackCreatureCommand(null, e.Creature, 

                        new SimpleAttack(null, MagicEffectType.FirePlume, AnimatedTextColor.Orange, 20, 20),
                                                                                                                         
                        new DamageCondition(SpecialCondition.Burning, MagicEffectType.FirePlume, AnimatedTextColor.Orange, new[] { 10, 10, 10, 10, 10, 10, 10 }, TimeSpan.FromSeconds(2) ) ) );
                }
            }

            return Promise.Completed;
        }
    }
}