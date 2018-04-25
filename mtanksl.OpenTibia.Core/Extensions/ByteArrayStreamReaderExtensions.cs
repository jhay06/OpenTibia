﻿using OpenTibia.IO;
using System;
using System.Text;

namespace OpenTibia
{
    public static class ByteArrayStreamReaderExtensions
    {
        public static Outfit ReadOutfit(this ByteArrayStreamReader reader)
        {
            ushort id = reader.ReadUShort();

            if (id == 0)
            {
                return new Outfit( reader.ReadUShort() );
            }

            return new Outfit( id, reader.ReadByte(), reader.ReadByte(), reader.ReadByte(), reader.ReadByte(), (Addons)reader.ReadByte() );
        }

        public static Light ReadLight(this ByteArrayStreamReader reader)
        {
            return new Light( reader.ReadByte(), reader.ReadByte() );
        }

        public static string ReadCsd(this ByteArrayStreamReader reader)
        {
            byte[] bytes = reader.ReadBytes(128);
            
            int index = Array.FindIndex(bytes, b => b == 0);

            if (index == -1)
	        {
                 return Encoding.Default.GetString(bytes);
	        }

            return Encoding.Default.GetString(bytes, index, bytes.Length - index);
        }
    }
}