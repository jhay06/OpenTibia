﻿using OpenTibia.IO;

namespace OpenTibia
{
    public class RemoveVipIncomingPacket : IIncomingPacket
    {
        public uint CreatureId { get; set; }
        
        public void Read(ByteArrayStreamReader reader)
        {
            CreatureId = reader.ReadUInt();
        }
    }
}