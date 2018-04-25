﻿using OpenTibia.IO;

namespace OpenTibia
{
    public class PleaseWaitOutgoingPacket : IOutgoingPacket
    {
        public PleaseWaitOutgoingPacket(string message, byte time)
        {
            this.Message = message;

            this.Time = time;
        }

        public string Message { get; set; }

        public byte Time { get; set; }
        
        public void Write(ByteArrayStreamWriter writer)
        {
            writer.Write( (byte)0x16 );

            writer.Write(Message);

            writer.Write(Time);
        }
    }
}