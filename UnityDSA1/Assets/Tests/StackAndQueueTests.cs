using System;
using System.Collections.Generic;
using ADS.A1;
using NUnit.Framework;

namespace ADS.Tests
{
    public class SimpleStackTests
    {
        private const string Part = "Part 3 Stack";

        [Test, Category(Part)]
        public void NewStack_HasCountZero()
        {
            var stack = new SimpleStack<int>();
            Assert.AreEqual(0, stack.Count);
        }

        [Test, Category(Part)]
        public void Push_IncreasesCount()
        {
            var stack = new SimpleStack<int>();
            stack.Push(1);
            stack.Push(2);
            Assert.AreEqual(2, stack.Count);
        }

        [Test, Category(Part)]
        public void Pop_ReturnsLastPushed()
        {
            var stack = new SimpleStack<int>();
            stack.Push(1);
            stack.Push(2);
            Assert.AreEqual(2, stack.Pop());
            Assert.AreEqual(1, stack.Count);
        }

        [Test, Category(Part)]
        public void Peek_ReturnsTopWithoutRemoving()
        {
            var stack = new SimpleStack<string>();
            stack.Push("a");
            stack.Push("b");
            Assert.AreEqual("b", stack.Peek());
            Assert.AreEqual(2, stack.Count);
        }

        [Test, Category(Part)]
        public void Pop_OnEmptyStack_Throws()
        {
            var stack = new SimpleStack<int>();
            Assert.Throws<InvalidOperationException>(() => stack.Pop());
        }

        [Test, Category(Part)]
        public void Peek_OnEmptyStack_Throws()
        {
            var stack = new SimpleStack<int>();
            Assert.Throws<InvalidOperationException>(() => stack.Peek());
        }

        [Test, Category(Part)]
        public void PushMany_PopAll_ComesOutReversed()
        {
            var stack = new SimpleStack<int>();
            for (int i = 0; i < 1000; i++)
            {
                stack.Push(i);
            }

            for (int i = 999; i >= 0; i--)
            {
                Assert.AreEqual(i, stack.Pop());
            }

            Assert.AreEqual(0, stack.Count);
        }

        [Test, Category(Part)]
        public void Push_AfterEmptying_Works()
        {
            var stack = new SimpleStack<int>();
            stack.Push(1);
            stack.Pop();
            stack.Push(2);
            Assert.AreEqual(2, stack.Peek());
            Assert.AreEqual(1, stack.Count);
        }
    }

    public class RingBufferQueueTests
    {
        private const string Part = "Part 4 Queue";
        private const string Bonus = "Bonus";

        [Test, Category(Part)]
        public void NewQueue_HasCountZero_AndTheCapacityYouAskedFor()
        {
            var queue = new RingBufferQueue<int>(5);
            Assert.AreEqual(0, queue.Count);
            Assert.AreEqual(5, queue.Capacity);
        }

        [Test, Category(Part)]
        public void Constructor_CapacityBelowOne_Throws()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new RingBufferQueue<int>(0));
            Assert.Throws<ArgumentOutOfRangeException>(() => new RingBufferQueue<int>(-3));
        }

        [Test, Category(Part)]
        public void Enqueue_IncreasesCount()
        {
            var queue = new RingBufferQueue<int>(5);
            queue.Enqueue(1);
            queue.Enqueue(2);
            Assert.AreEqual(2, queue.Count);
        }

        [Test, Category(Part)]
        public void Dequeue_ReturnsItemsInArrivalOrder()
        {
            var queue = new RingBufferQueue<int>(5);
            queue.Enqueue(1);
            queue.Enqueue(2);
            queue.Enqueue(3);
            Assert.AreEqual(1, queue.Dequeue());
            Assert.AreEqual(2, queue.Dequeue());
            Assert.AreEqual(3, queue.Dequeue());
            Assert.AreEqual(0, queue.Count);
        }

        [Test, Category(Part)]
        public void Peek_ReturnsFrontWithoutRemoving()
        {
            var queue = new RingBufferQueue<string>(3);
            queue.Enqueue("a");
            queue.Enqueue("b");
            Assert.AreEqual("a", queue.Peek());
            Assert.AreEqual(2, queue.Count);
        }

        [Test, Category(Part)]
        public void Dequeue_OnEmptyQueue_Throws()
        {
            var queue = new RingBufferQueue<int>(3);
            Assert.Throws<InvalidOperationException>(() => queue.Dequeue());
        }

        [Test, Category(Part)]
        public void Peek_OnEmptyQueue_Throws()
        {
            var queue = new RingBufferQueue<int>(3);
            Assert.Throws<InvalidOperationException>(() => queue.Peek());
        }

        [Test, Category(Part)]
        public void IsFull_OnlyWhenCountReachesCapacity()
        {
            var queue = new RingBufferQueue<int>(2);
            Assert.IsFalse(queue.IsFull);
            queue.Enqueue(1);
            Assert.IsFalse(queue.IsFull);
            queue.Enqueue(2);
            Assert.IsTrue(queue.IsFull);
        }

        [Test, Category(Part)]
        public void Enqueue_WhenFull_Throws()
        {
            var queue = new RingBufferQueue<int>(2);
            queue.Enqueue(1);
            queue.Enqueue(2);
            Assert.Throws<InvalidOperationException>(() => queue.Enqueue(3));
            Assert.AreEqual(2, queue.Count);
        }

        [Test, Category(Part)]
        public void WrapsAroundTheEndOfTheArray()
        {
            var queue = new RingBufferQueue<int>(3);
            queue.Enqueue(1);
            queue.Enqueue(2);
            queue.Enqueue(3);
            queue.Dequeue();
            queue.Enqueue(4);
            Assert.AreEqual(2, queue.Dequeue());
            Assert.AreEqual(3, queue.Dequeue());
            Assert.AreEqual(4, queue.Dequeue());
        }

        [Test, Category(Part)]
        public void ManyRoundsThroughASmallBuffer_KeepOrder()
        {
            var queue = new RingBufferQueue<int>(4);
            var expected = new Queue<int>();
            int next = 0;
            for (int round = 0; round < 50; round++)
            {
                while (!queue.IsFull)
                {
                    queue.Enqueue(next);
                    expected.Enqueue(next);
                    next++;
                }

                for (int i = 0; i < 3; i++)
                {
                    Assert.AreEqual(expected.Dequeue(), queue.Dequeue());
                }
            }
        }

        [Test, Category(Bonus)]
        public void Bonus_CanGrow_GrowsInsteadOfThrowing()
        {
            var queue = new RingBufferQueue<int>(2, canGrow: true);
            queue.Enqueue(1);
            queue.Enqueue(2);
            queue.Enqueue(3);
            Assert.AreEqual(3, queue.Count);
            Assert.AreEqual(4, queue.Capacity, "Grow by doubling, like DynamicArray.");
        }

        [Test, Category(Bonus)]
        public void Bonus_GrowingAWrappedQueue_KeepsOrder()
        {
            var queue = new RingBufferQueue<int>(3, canGrow: true);
            queue.Enqueue(1);
            queue.Enqueue(2);
            queue.Enqueue(3);
            queue.Dequeue();
            queue.Enqueue(4);
            queue.Enqueue(5);
            for (int expected = 2; expected <= 5; expected++)
            {
                Assert.AreEqual(expected, queue.Dequeue());
            }
        }
    }
}
