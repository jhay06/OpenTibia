﻿using OpenTibia.Common.Objects;
using OpenTibia.Common.Structures;
using OpenTibia.Game.Commands;
using OpenTibia.Network.Packets.Outgoing;
using System;

namespace OpenTibia.Game.CommandHandlers
{
    public class MoveAwayTradingRejectHandler : CommandHandler<CreatureWalkCommand>
    {
        public override Promise Handle(Func<Promise> next, CreatureWalkCommand command)
        {
            if (command.Creature is Player player)
            {
                return next().Then( () =>
                {
                    if (Context.Server.Tradings.Count > 0)
                    {
                        Trading reject = null;

                        Trading trading = Context.Server.Tradings.GetTradingByOfferPlayer(player);

                        if (trading != null)
                        {
                            if ( !trading.OfferPlayer.Tile.Position.IsInRange(trading.CounterOfferPlayer.Tile.Position, 2) )
                            {
                                reject = trading;
                            }
                            else
                            {
                                switch (trading.Offer.Root() )
                                {
                                    case null:

                                        reject = trading;

                                        break;

                                    case Tile tile:

                                        if ( !trading.OfferPlayer.Tile.Position.IsNextTo(tile.Position) )
                                        {
                                            reject = trading;
                                        }

                                        break;
                                }
                            }
                        }
                        else
                        {
                            trading = Context.Server.Tradings.GetTradingByCounterOfferPlayer(player);

                            if (trading != null)
                            {
                                if ( !trading.CounterOfferPlayer.Tile.Position.IsInRange(trading.OfferPlayer.Tile.Position, 2) )
                                {
                                    reject = trading;
                                }
                                else
                                {
                                    if (trading.CounterOffer != null)
                                    {
                                        switch (trading.CounterOffer.Root() )
                                        {
                                            case null:

                                                reject = trading;

                                                break;

                                            case Tile tile:

                                                if ( !trading.CounterOfferPlayer.Tile.Position.IsNextTo(tile.Position) )
                                                {
                                                    reject = trading;
                                                }

                                                break;
                                        }
                                    }
                                }
                            }
                        }

                        if (reject != null)
                        {
                            Context.AddPacket(reject.OfferPlayer.Client.Connection, new ShowWindowTextOutgoingPacket(TextColor.WhiteBottomGameWindow, Constants.TradeCanceled) );

                            Context.AddPacket(reject.OfferPlayer.Client.Connection, new CloseTradeOutgoingPacket() );

                            Context.AddPacket(reject.CounterOfferPlayer.Client.Connection, new ShowWindowTextOutgoingPacket(TextColor.WhiteBottomGameWindow, Constants.TradeCanceled) );

                            Context.AddPacket(reject.CounterOfferPlayer.Client.Connection, new CloseTradeOutgoingPacket() );

                            Context.Server.Tradings.RemoveTrading(reject);
                        }
                    }

                    return Promise.Completed;
                } );
            }

            return next();
        }
    }
}