﻿using OpenTibia.Common.Structures;
using OpenTibia.Game;

namespace OpenTibia.Common.Objects
{
    public class Client : IClient
    {
        public Client(Server server)
        {
            this.Battles = new BattleCollection(server, this);

            this.Containers = new ContainerCollection(this);

            this.Windows = new WindowCollection(this);

            this.Outfits = new PlayerOutfitCollection();

            this.Storages = new PlayerStorageCollection();

            this.Spells = new PlayerSpellCollection();

            this.Vips = new PlayerVipCollection();
        }

        public IBattleCollection Battles { get; }

        public IContainerCollection Containers { get; }

        public IWindowCollection Windows { get; }

        public IPlayerOutfitCollection Outfits { get; }

        public IPlayerStorageCollection Storages { get; }

        public IPlayerSpellCollection Spells { get; }

        public IPlayerVipCollection Vips { get; }

        private Player player;

        public Player Player
        {
            get
            {
                return player;
            }
            set
            {
                if (value != player)
                {
                    var current = player;

                                  player = value;

                    if (value == null)
                    {
                        current.Client = null;
                    }
                    else
                    {
                        player.Client = this;
                    }
                }
            }
        }

        private IConnection connection;

        public IConnection Connection
        {
            get
            {
                return connection;
            }
            set
            {
                if (value != connection)
                {
                    var current = connection;

                                  connection = value;

                    if (value == null)
                    {
                        current.Client = null;
                    }
                    else
                    {
                        connection.Client = this;
                    }
                }
            }
        }

        public FightMode FightMode { get; set; }

        public ChaseMode ChaseMode { get; set; }

        public SafeMode SafeMode { get; set; }

        public IContent GetContent(IContainer container, byte clientIndex)
        {
            if (container is Tile tile)
            {
                byte index = 0;

                foreach (var _content in tile.GetContents() )
                {
                    if (index >= Constants.ObjectsPerPoint)
                    {
                        break;
                    }

                    if (_content is Creature _creature && _creature != Player && _creature.Invisible)
                    {
                        continue;
                    }

                    if (clientIndex == index)
                    {
                        return _content;
                    }

                    index++;
                }

                return null;
            }

            return container.GetContent(clientIndex);
        }

        public bool TryGetIndex(IContent content, out byte clientIndex)
        {
            switch (content)
            {
                case Item item:

                    switch (item.Parent)
                    {
                        case Tile tile:

                            if (Player.Tile.Position.CanSee(tile.Position) )
                            {
                                byte index = 0;

                                foreach (var _content in tile.GetContents() )
                                {
                                    if (index >= Constants.ObjectsPerPoint)
                                    {
                                        break;
                                    }

                                    if (_content is Creature _creature && _creature != Player && _creature.Invisible)
                                    {
                                        continue;
                                    }

                                    if (_content == item)
                                    {
                                        clientIndex = index;

                                        return true;
                                    }

                                    index++;
                                }
                            }

                            break;

                        case Inventory inventory:

                            if (Player.Inventory == inventory)
                            {
                                clientIndex = (byte)inventory.GetIndex(content);

                                return true;
                            }

                            break;

                        case Container container:

                            foreach (var pair in Containers.GetIndexedContainers() )
                            {
                                if (pair.Value == container)
                                {
                                    clientIndex = (byte)container.GetIndex(content);

                                    return true;
                                }
                            }

                            break;
                    }

                    break;

                case Creature creature:

                    if (Player.Tile.Position.CanSee(creature.Tile.Position) )
                    {
                        byte index = 0;

                        foreach (var _content in creature.Tile.GetContents() )
                        {
                            if (index >= Constants.ObjectsPerPoint)
                            {
                                break;
                            }

                            if (_content is Creature _creature && _creature != Player && _creature.Invisible)
                            {
                                continue;
                            }

                            if (_content == creature)
                            {
                                clientIndex = index;

                                return true;
                            }

                            index++;
                        }
                    }

                    break;
            }

            clientIndex = 0;

            return false;
        }
    }
}