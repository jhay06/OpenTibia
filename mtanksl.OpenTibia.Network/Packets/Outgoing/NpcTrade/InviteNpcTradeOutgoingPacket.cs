﻿using OpenTibia.IO;
using System.Collections.Generic;

namespace OpenTibia.Network.Packets.Outgoing
{
    public class InviteNpcTradeOutgoingPacket : IOutgoingPacket
    {
        public InviteNpcTradeOutgoingPacket(List<OfferDto> offers)
        {
            this.Offers = offers;
        }

        private List<OfferDto> offers;

        public List<OfferDto> Offers
        {
            get
            {
                return offers ?? ( offers = new List<OfferDto>() );
            }
            set
            {
                offers = value;
            }
        }

        public void Write(ByteArrayStreamWriter writer)
        {
            writer.Write( (byte)0x7A );

            writer.Write( (byte)Offers.Count );

            foreach (var offer in Offers)
            {
                writer.Write(offer.ItemId);

                writer.Write(offer.Type);

                writer.Write(offer.Name);

                writer.Write(offer.Weight);

                writer.Write(offer.BuyPrice);

                writer.Write(offer.SellPrice);
            }
        }
    }
}