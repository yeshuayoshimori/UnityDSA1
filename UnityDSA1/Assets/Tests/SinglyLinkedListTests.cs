using System.Collections.Generic;
using ADS.Week2;
using NUnit.Framework;

namespace ADS.Tests
{
    public class SinglyLinkedListTests
    {
        private const string TypeAlong = "Week 2 type along";

        [Test, Category(TypeAlong)]
        public void NewList_HasCountZero()
        {
            var list = new SinglyLinkedList<int>();
            Assert.AreEqual(0, list.Count);
        }

        [Test, Category(TypeAlong)]
        public void AddFirst_PutsItemAtFront()
        {
            var list = new SinglyLinkedList<int>();
            list.AddFirst(3);
            list.AddFirst(2);
            list.AddFirst(1);
            AssertContents(list, 1, 2, 3);
        }

        [Test, Category(TypeAlong)]
        public void AddLast_PutsItemAtBack()
        {
            var list = new SinglyLinkedList<int>();
            list.AddLast(1);
            list.AddLast(2);
            list.AddLast(3);
            AssertContents(list, 1, 2, 3);
        }

        [Test, Category(TypeAlong)]
        public void AddLast_AfterAddFirst_UsesTheTail()
        {
            var list = new SinglyLinkedList<int>();
            list.AddFirst(1);
            list.AddLast(2);
            AssertContents(list, 1, 2);
        }

        [Test, Category(TypeAlong)]
        public void Foreach_OnEmptyList_DoesNothing()
        {
            var list = new SinglyLinkedList<int>();
            AssertContents(list);
        }

        private static void AssertContents(SinglyLinkedList<int> list, params int[] expected)
        {
            Assert.AreEqual(expected.Length, list.Count, "Wrong Count.");
            var seen = new List<int>();
            foreach (int item in list)
            {
                seen.Add(item);
            }

            CollectionAssert.AreEqual(expected, seen);
        }
    }
}
