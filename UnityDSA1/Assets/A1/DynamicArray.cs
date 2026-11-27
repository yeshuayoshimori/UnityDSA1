using System;
using System.Collections;
using System.Collections.Generic;

namespace ADS.A1
{
    public class DynamicArray<T> : IEnumerable<T>
    {
        private const int StartingCapacity = 4;

        private T[] items;
        private int count;

        public DynamicArray()
        {
            items = new T[StartingCapacity];
            count = 0;
        }

        public int Count => count;

        public int Capacity => items.Length;

        public T this[int index]
        {
            get  {
                CheckIndex(index);
                return items[index];
            }
            set  {
                CheckIndex(index);
                items[index] = value;
            }
        }

        public void Add(T item)
        {
            if (count == items.Length)
            {
                Grow();
            }
            items[count] = item;
            count++;
        }

        public void Insert(int index, T item)
        {
            if (index < 0 || index > count)
            {
                throw new ArgumentOutOfRangeException(nameof(index), $"Index {index} is outside 0...{count}.");
            }

            if (count == items.Length)
            {
                Grow();
            }


            for (int i = count; i > index; i--)
            {
                items[i] = items[i - 1];
            }

            items[index] = item;
            count++;
        }

        public void RemoveAt(int index)
        {
            CheckIndex(index);

            for (int i = index; i < count - 1; i++)
            {
                items[i] = items[i + 1];
            }

            count--;
            items[count] = default;
        }

        public void Clear()
        {
            for (int i = 0; i < count; i++)
            {
                items[i] = default;
            }
            count = 0;
        }

        public bool Contains(T item)
        {
            EqualityComparer<T> comparer = EqualityComparer<T>.Default;
            for (int i = 0; i < count; i++)
            {
                if (comparer.Equals(items[i], item))
                {
                    return true;
                }
            }
            return false;
        }

        private void Grow()
        {
            T[] bigger = new T[items.Length * 2];
            for (int i = 0; i < count; i++)
            {
                bigger[i] = items[i];
            }
            items = bigger;
        }

        private void CheckIndex(int index) 
        {
            if (index < 0 || index >= count)
            {
                throw new ArgumentOutOfRangeException(nameof(index), $"Index {index} is outside 0...{count}.");
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            for (int idx = 0; idx < count; idx++)
            {
                yield return items[idx];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
