﻿namespace OpenTibia.Common.Objects
{
    public interface IConnection 
    {
        int Port { get; }

        IClient Client { get; set; }

        uint[] Keys { get; set; }

        void Send(byte[] bytes);

        void Disconnect();
    }
}