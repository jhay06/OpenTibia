﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using OpenTibia.Data.Contexts;
using System;

namespace OpenTibia.Game
{
    public class DatabaseFactory
    {
        private Server server;

        private Func<DbContextOptionsBuilder, DatabaseContext> factory;

        public DatabaseFactory(Server server, Func<DbContextOptionsBuilder, DatabaseContext> factory)
        {
            this.server = server;

            this.factory = factory;
        }

        public DatabaseContext Create()
        {
            var builder = new DbContextOptionsBuilder();

            builder.LogTo(

                action:                         
                    message => server.Logger.WriteLine(message.Substring(message.IndexOf("CommandType='Text', CommandTimeout='30'") + 40), LogLevel.Debug),

                events: 
                    new[] { RelationalEventId.CommandExecuted }, 

                options:                         
                    DbContextLoggerOptions.SingleLine
            );

            return factory(builder);
        }
    }
}