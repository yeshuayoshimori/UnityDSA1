using System;

namespace ADS.A1
{
    public class RingBufferQueue<T> //first in, first out, like a ferrish wheel kind of, or clock, the finger moves around the circle/cycle
    {
        private T[] items;
        private int head;
        private int count;
        private readonly bool canGrow;

        public RingBufferQueue(int capacity, bool canGrow = false)
        {
            if (capacity < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(capacity), "Capacity must be at least 1.");
            }

            items = new T[capacity];
            this.canGrow = canGrow;
        }

        public int Count => count;

        public int Capacity => items.Length;

        public bool IsFull => count == items.Length;

        public void Enqueue(T item) //Add/pushing items into the queue
        {
            if (IsFull) //then add more space
            {
                if (canGrow)
                {
                    Grow();
                }
                else
                {
                    ThrowError("The queue is full.");
                }
            }

            items[(head + count) % items.Length] = item;
            count++;
        }

        public T Dequeue() //removing, poping from queue
        {
            if (count == 0)
            {
                ThrowError("The queue is empty.");
            }

            T item = items[head];
            items[head] = default;
            head = (head + 1) % items.Length;
            count--;
            return item;
        }

        public T Peek() //returning, like pop, but withouth removing it from the list
        {
            if (count == 0)
            {
                ThrowError("The queue is empty.");
            }

            return items[head];
        }

        private void Grow() //For expanding the list when the count gets more items
        {
            T[] bigger = new T[items.Length * 2];
            for (int i = 0; i < count; i++)
            {
                bigger[i] = items[(head + i) % items.Length];
            }
            items = bigger;
            head = 0;
        }

        private void ThrowError(string message) //for showing exceptions or errors 
        {
            throw new InvalidOperationException(message);
        }
    }
}
