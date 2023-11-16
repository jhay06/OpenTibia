﻿using OpenTibia.Game;
using System;
using System.Collections.Generic;

namespace OpenTibia.Common.Objects
{
    public class RateLimiting
    {
        private class RateLimitItem
        {
            public DateTime LastConnection { get; set; }

            public int ConnectionCount { get; set; }

            public DateTime LastPacket { get; set; }
 
            public int PacketCount { get; set; }

            public DateTime LastLoginAttempts { get; set; }

            public int LoginAttemptsCount { get; set; }

            public DateTime BanTimeout { get; set; }
        }

        private readonly object sync = new object();

        private Server server;

        public RateLimiting(Server server)
        {
            this.server = server;
        }

        private Dictionary<string, RateLimitItem> items = new Dictionary<string, RateLimitItem>();

        public bool CanConnect(string ipAddress)
        {
            lock (sync)
            {
                RateLimitItem item;

                if ( !items.TryGetValue(ipAddress, out item) )
                {
                    item = new RateLimitItem();

                    items.Add(ipAddress, item);
                }

                if (DateTime.UtcNow < item.BanTimeout)
                {
                    return false;
                }

                if ( (DateTime.UtcNow - item.LastConnection).TotalMilliseconds > server.Config.RateLimitingMaxConnectionsPerMilliseconds)
                {
                    item.LastConnection = DateTime.UtcNow;

                    item.ConnectionCount = 1;
                }
                else
                {
                    item.ConnectionCount++;

                    if (item.ConnectionCount > server.Config.RateLimitingMaxConnections)
                    {
                        item.BanTimeout = DateTime.UtcNow.AddMilliseconds(server.Config.RateLimitingConnectionsAbuseBanMilliseconds);

                        return false;
                    }
                }

                return true;
            }
        }

        public bool CanReceive(string ipAddress)
        {
            lock (sync)
            {
                RateLimitItem item;

                if ( !items.TryGetValue(ipAddress, out item) )
                {
                    item = new RateLimitItem();

                    items.Add(ipAddress, item);
                }

                if (DateTime.UtcNow < item.BanTimeout)
                {
                    return false;
                }

                if ( (DateTime.UtcNow - item.LastPacket).TotalMilliseconds > server.Config.RateLimitingMaxPacketsPerMilliseconds)
                {
                    item.LastPacket = DateTime.UtcNow;

                    item.PacketCount = 1;
                }
                else
                {
                    item.PacketCount++;

                    if (item.PacketCount > server.Config.RateLimitingMaxPackets)
                    {
                        item.BanTimeout = DateTime.UtcNow.AddMilliseconds(server.Config.RateLimitingPacketsAbuseBanMilliseconds);

                        return false;
                    }
                }

                return true;
            }            
        }
                
        public bool CanLogin(string ipAddress)
        {
            lock (sync)
            {
                RateLimitItem item;

                if ( !items.TryGetValue(ipAddress, out item) )
                {
                    item = new RateLimitItem();

                    items.Add(ipAddress, item);
                }

                if (DateTime.UtcNow < item.BanTimeout)
                {
                    return false;
                }

                if ( (DateTime.UtcNow - item.LastLoginAttempts).TotalMilliseconds > server.Config.RateLimitingMaxLoginAttemptsPerMilliseconds)
                {
                    item.LastLoginAttempts = DateTime.UtcNow;

                    item.LoginAttemptsCount = 1;
                }
                else
                {
                    item.LoginAttemptsCount++;

                    if (item.LoginAttemptsCount > server.Config.RateLimitingMaxLoginAttempts)
                    {
                        item.BanTimeout = DateTime.UtcNow.AddMilliseconds(server.Config.RateLimitingLoginAttemptsAbuseBanMilliseconds);

                        return false;
                    }
                }

                return true;              
            }
        }
    }
}