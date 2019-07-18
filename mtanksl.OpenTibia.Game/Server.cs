﻿using OpenTibia.Common.Objects;
using OpenTibia.FileFormats.Dat;
using OpenTibia.FileFormats.Otb;
using OpenTibia.FileFormats.Otbm;
using OpenTibia.FileFormats.Xml.Items;
using OpenTibia.FileFormats.Xml.Monsters;
using OpenTibia.FileFormats.Xml.Npcs;
using OpenTibia.Game.Commands;
using OpenTibia.Network.Sockets;
using OpenTibia.Threading;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace OpenTibia.Game
{
    public class Server : IDisposable
    {
        private Dispatcher dispatcher;

        private Scheduler scheduler;

        private List<Listener> listeners = new List<Listener>();

        public Server()
        {
            dispatcher = new Dispatcher();

            scheduler = new Scheduler(dispatcher);

            listeners.Add(new Listener(7171, (port, socket) => new LoginConnection(this, port, socket) ) );

            listeners.Add(new Listener(7172, (port, socket) => new GameConnection(this, port, socket) ) );
        }

        ~Server()
        {
            Dispose(false);
        }

        public Logger Logger { get; set; }

        public ChannelCollection Channels { get; set; }

        public RuleViolationCollection RuleViolations { get; set; }

        public PacketsFactory PacketsFactory { get; set; }

        public ItemFactory ItemFactory { get; set; }
        
        public MonsterFactory MonsterFactory { get; set; }
        
        public NpcFactory NpcFactory { get; set; }

        public Map Map { get; set; }
        
        public void Start()
        {
            Logger = new Logger();

            Channels = new ChannelCollection();

            RuleViolations = new RuleViolationCollection();

            PacketsFactory = new PacketsFactory();

            using (Logger.Measure("Loading items", true) )
            {
                ItemFactory = new ItemFactory(OtbFile.Load("data/items/items.otb"), DatFile.Load("data/items/tibia.dat"), ItemsFile.Load("data/items/items.xml") );
            }

            using (Logger.Measure("Loading monsters", true) )
            {
                MonsterFactory = new MonsterFactory(MonsterFile.Load("data/monsters") );
            }

            using (Logger.Measure("Loading npcs", true) )
            {
                NpcFactory = new NpcFactory(NpcFile.Load("data/npcs") );
            }

            using (Logger.Measure("Loading map", true) )
            {
                Map = new Map(this, OtbmFile.Load("data/map/pholium3.otbm") );
            }           

            dispatcher.Start();

            scheduler.Start();

            foreach (var listener in listeners)
            {
                listener.Start();
            }

            Logger.WriteLine("Server online");
        }

        public void QueueForExecution(Command command, Action callback = null)
        {
            dispatcher.QueueForExecution( () =>
            {
                CommandContext context = new CommandContext();

                try
                {
                    command.Execute(this, context);

                    context.Flush();
                }
                catch (Exception ex)
                {
                    Logger.WriteLine(ex.ToString() );
                }

                if (callback != null)
                {
                    callback();
                }
            } );
        }

        private Dictionary<string, SchedulerEvent> events = new Dictionary<string, SchedulerEvent>();

        public void QueueForExecution(string key, int executeIn, Command command, Action callback = null)
        {
            SchedulerEvent schedulerEvent;

            if ( events.TryGetValue(key, out schedulerEvent) )
            {
                events.Remove(key);

                schedulerEvent.Cancel();
            }

            schedulerEvent = new SchedulerEvent(executeIn, () =>
            {
                events.Remove(key);

                CommandContext context = new CommandContext();

                try
                {
                    command.Execute(this, context);

                    context.Flush();
                }
                catch (Exception ex)
                {
                    Logger.WriteLine(ex.ToString() );
                }

                if (callback != null)
                {
                    callback();
                }
            } );

            events[key] = schedulerEvent;

            scheduler.QueueForExecution(schedulerEvent);
        }

        public bool CancelQueueForExecution(string key)
        {
            SchedulerEvent schedulerEvent;

            if ( events.TryGetValue(key, out schedulerEvent) )
            {
                events.Remove(key);

                schedulerEvent.Cancel();

                return true;
            }

            return false;
        }

        public void Stop()
        {
            foreach (var listener in listeners)
            {
                listener.Stop();
            }

            scheduler.Stop();

            dispatcher.Stop();

            Logger.WriteLine("Server offline");
        }

        private bool disposed = false;

        public void Dispose()
        {
            Dispose(true);

            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                disposed = true;

                if (disposing)
                {
                    foreach (var listener in listeners)
                    {
                        listener.Dispose();
                    }
                }
            }
        }
    }
}