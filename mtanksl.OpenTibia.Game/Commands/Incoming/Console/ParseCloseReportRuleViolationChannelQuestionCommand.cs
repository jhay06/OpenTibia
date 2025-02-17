﻿using OpenTibia.Common.Objects;
using OpenTibia.Network.Packets.Outgoing;

namespace OpenTibia.Game.Commands
{
    public class ParseCloseReportRuleViolationChannelQuestionCommand : Command
    {
        public ParseCloseReportRuleViolationChannelQuestionCommand(Player player)
        {
            Player = player;
        }

        public Player Player { get; set; }

        public override Promise Execute()
        {
            RuleViolation ruleViolation = Context.Server.RuleViolations.GetRuleViolationByReporter(Player);

            if (ruleViolation != null)
            {
                if (ruleViolation.Assignee == null)
                {
                    Context.Server.RuleViolations.RemoveRuleViolation(ruleViolation);

                    foreach (var observer in Context.Server.Channels.GetChannel(3).GetPlayers() )
                    {
                        Context.AddPacket(observer.Client.Connection, new RemoveRuleViolationOutgoingPacket(ruleViolation.Reporter.Name) );
                    }

                    return Promise.Completed;
                }
                else
                {
                    Context.Server.RuleViolations.RemoveRuleViolation(ruleViolation);

                    Context.AddPacket(ruleViolation.Assignee.Client.Connection, new CancelRuleViolationOutgoingPacket(ruleViolation.Reporter.Name) );

                    return Promise.Completed;
                }
            }

            return Promise.Break;
        }
    }
}