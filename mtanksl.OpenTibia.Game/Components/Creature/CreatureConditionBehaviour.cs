﻿using OpenTibia.Common.Objects;
using OpenTibia.Common.Structures;
using OpenTibia.Game.Commands;
using OpenTibia.Network.Packets.Outgoing;

namespace OpenTibia.Game.Components
{
    public class CreatureConditionBehaviour : StateBehaviour
    {
        public CreatureConditionBehaviour(Condition condition)
        {
            Condition = condition;
        }

        public Condition Condition { get; set; }

        protected override Promise OnStart()
        {
            Creature creature = (Creature)GameObject;

            SpecialCondition specialCondition = Condition.ConditionSpecialCondition.ToSpecialCondition();

            if (specialCondition != SpecialCondition.None && !creature.HasSpecialCondition(specialCondition) )
            {
                creature.AddSpecialCondition(specialCondition);

                if (creature is Player player)
                {
                    Context.AddPacket(player.Client.Connection, new SetSpecialConditionOutgoingPacket(creature.SpecialConditions) );
                }
            }

            return Condition.AddCondition(creature);
        }

        protected override Promise OnStop(State state)
        {
            Creature creature = (Creature)GameObject;

            SpecialCondition specialCondition = Condition.ConditionSpecialCondition.ToSpecialCondition();

            if (specialCondition != SpecialCondition.None && creature.HasSpecialCondition(specialCondition) )
            {
                creature.RemoveSpecialCondition(specialCondition);

                if (creature is Player player)
                {
                    Context.AddPacket(player.Client.Connection, new SetSpecialConditionOutgoingPacket(creature.SpecialConditions) );
                }
            }

            switch (state)
            {
                case State.Success:
                               
                    Context.Current.Server.GameObjectComponents.RemoveComponent(GameObject, this);

                    break;

                case State.Canceled:
                             
                    Context.Current.Server.GameObjectComponents.RemoveComponent(GameObject, this);

                    break;

                case State.Stopped:
                             
                    Condition.Cancel();

                    break;
            }

            return Condition.RemoveCondition(creature);
        }
    }
}