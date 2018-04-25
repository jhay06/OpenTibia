﻿using OpenTibia.IO;

namespace OpenTibia
{
    public class SlotRemoveOutgoingPacket : IOutgoingPacket
    {
        public SlotRemoveOutgoingPacket(Slot slot)
        {
            this.Slot = slot;
        }

        public Slot Slot { get; set; }
        
        public void Write(ByteArrayStreamWriter writer)
        {
            writer.Write( (byte)0x79 );

            writer.Write( (byte)Slot );
        }
    }
}