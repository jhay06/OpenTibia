﻿using OpenTibia.Common.Objects;
using OpenTibia.Game.Commands;
using System;

namespace OpenTibia.Game.Extensions
{
    public static class MonsterExtensions
    {
        /// <exception cref="InvalidOperationException"></exception>

        public static Promise Destroy(this Monster monster)
        {
            Context context = Context.Current;

            if (context == null)
            {
                throw new InvalidOperationException("Context not found.");
            }

            return context.AddCommand(new MonsterDestroyCommand(monster) );
        }

        /// <exception cref="InvalidOperationException"></exception>

        public static Promise Say(this Monster player, string message)
        {
            Context context = Context.Current;

            if (context == null)
            {
                throw new InvalidOperationException("Context not found.");
            }

            return context.AddCommand(new MonsterSayCommand(player, message) );
        }
    }
}