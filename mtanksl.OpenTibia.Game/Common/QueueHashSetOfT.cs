﻿using System;
using System.Collections.Generic;

namespace OpenTibia.Common.Objects
{
    public class QueueHashSet<T>
    {
        private LinkedList<T> queue = new LinkedList<T>();

        private HashSet<T> hashSet = new HashSet<T>();

        public int Count
        {
            get
            {
                return queue.Count;
            }
        }

        public T Peek()
        {
            if (queue.First != null)
            {
                return queue.First.Value;
            }

            return default(T);
        }

        public T Dequeue()
        {
            if (Count == 0)
            {
                throw new InvalidOperationException();
            }

            var node = queue.First;

            queue.RemoveFirst();

            hashSet.Remove(node.Value);

            return node.Value;
        }

        public bool Add(T value)
        {
            if (hashSet.Add(value) )
            {
                queue.AddLast(value);

                return true;
            }

            return false;
        }

        public bool Remove(T value)
        {
            if (hashSet.Remove(value) )
            {
                queue.Remove(value);

                return true;
            }

            return false;
        }

        public bool Contains(T value)
        {
            return hashSet.Contains(value);
        }
    }
}