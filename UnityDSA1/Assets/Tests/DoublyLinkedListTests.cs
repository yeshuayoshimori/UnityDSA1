using System;
using System.Collections.Generic;
using ADS.A1;
using NUnit.Framework;

namespace ADS.Tests
{
    public class DoublyLinkedListTests
    {
        private const string Part = "Part 2 DoublyLinkedList";

        [Test, Category(Part)]
        public void NewList_HasCountZero()
        {
            var list = new DoublyLinkedList<int>();
            Assert.AreEqual(0, list.Count);
        }

        [Test, Category(Part)]
        public void AddLast_IncreasesCount()
        {
            var list = new DoublyLinkedList<int>();
            list.AddLast(1);
            list.AddLast(2);
            Assert.AreEqual(2, list.Count);
        }

        [Test, Category(Part)]
        public void AddLast_KeepsItemsInOrder()
        {
            var list = MakeList(1, 2, 3);
            AssertContents(list, 1, 2, 3);
        }

        [Test, Category(Part)]
        public void Foreach_OnEmptyList_DoesNothing()
        {
            var list = new DoublyLinkedList<int>();
            AssertContents(list);
        }

        [Test, Category(Part)]
        public void AddFirst_PutsItemAtFront()
        {
            var list = new DoublyLinkedList<int>();
            list.AddFirst(3);
            list.AddFirst(2);
            list.AddFirst(1);
            AssertContents(list, 1, 2, 3);
        }

        [Test, Category(Part)]
        public void AddFirst_And_AddLast_Mix()
        {
            var list = new DoublyLinkedList<int>();
            list.AddLast(2);
            list.AddFirst(1);
            list.AddLast(3);
            AssertContents(list, 1, 2, 3);
        }

        [Test, Category(Part)]
        public void Indexer_Get_ReturnsItemAtIndex()
        {
            var list = MakeList(10, 20, 30, 40, 50);
            Assert.AreEqual(10, list[0]);
            Assert.AreEqual(30, list[2]);
            Assert.AreEqual(50, list[4]);
        }

        [Test, Category(Part)]
        public void Indexer_Set_ReplacesItem()
        {
            var list = MakeList(10, 20, 30);
            list[1] = 99;
            AssertContents(list, 10, 99, 30);
        }

        [Test, Category(Part)]
        public void Indexer_Throws_AtMinusOne()
        {
            var list = MakeList(1, 2, 3);
            Assert.Throws<ArgumentOutOfRangeException>(() => { int x = list[-1]; });
        }

        [Test, Category(Part)]
        public void Indexer_Throws_AtCount()
        {
            var list = MakeList(1, 2, 3);
            Assert.Throws<ArgumentOutOfRangeException>(() => { int x = list[3]; });
            Assert.Throws<ArgumentOutOfRangeException>(() => list[3] = 9);
        }

        [Test, Category(Part)]
        public void Works_WithStrings()
        {
            var list = new DoublyLinkedList<string>();
            list.AddLast("sword");
            list.AddLast("shield");
            Assert.AreEqual("shield", list[1]);
        }

        [Test, Category(Part)]
        public void Insert_AtFront()
        {
            var list = MakeList(2, 3);
            list.Insert(0, 1);
            AssertContents(list, 1, 2, 3);
        }

        [Test, Category(Part)]
        public void Insert_InMiddle()
        {
            var list = MakeList(1, 2, 4, 5);
            list.Insert(2, 3);
            AssertContents(list, 1, 2, 3, 4, 5);
        }

        [Test, Category(Part)]
        public void Insert_AtEnd_WorksLikeAddLast()
        {
            var list = MakeList(1, 2);
            list.Insert(2, 3);
            AssertContents(list, 1, 2, 3);
        }

        [Test, Category(Part)]
        public void Insert_IntoEmptyList()
        {
            var list = new DoublyLinkedList<int>();
            list.Insert(0, 7);
            AssertContents(list, 7);
        }

        [Test, Category(Part)]
        public void Insert_InMiddle_KeepsBackwardLinks()
        {
            var list = MakeList(1, 2, 4, 5);
            list.Insert(2, 3);
            AssertBackwards(list, 5, 4, 3, 2, 1);
        }

        [Test, Category(Part)]
        public void Insert_OutOfRange_Throws()
        {
            var list = MakeList(1, 2, 3);
            Assert.Throws<ArgumentOutOfRangeException>(() => list.Insert(4, 9));
            Assert.Throws<ArgumentOutOfRangeException>(() => list.Insert(-1, 9));
            AssertContents(list, 1, 2, 3);
        }

        [Test, Category(Part)]
        public void RemoveFirst_ReturnsAndRemovesFront()
        {
            var list = MakeList(1, 2, 3);
            Assert.AreEqual(1, list.RemoveFirst());
            AssertContents(list, 2, 3);
        }

        [Test, Category(Part)]
        public void RemoveLast_ReturnsAndRemovesBack()
        {
            var list = MakeList(1, 2, 3);
            Assert.AreEqual(3, list.RemoveLast());
            AssertContents(list, 1, 2);
        }

        [Test, Category(Part)]
        public void RemoveFirst_OnEmptyList_Throws()
        {
            var list = new DoublyLinkedList<int>();
            Assert.Throws<InvalidOperationException>(() => list.RemoveFirst());
        }

        [Test, Category(Part)]
        public void RemoveLast_OnEmptyList_Throws()
        {
            var list = new DoublyLinkedList<int>();
            Assert.Throws<InvalidOperationException>(() => list.RemoveLast());
        }

        [Test, Category(Part)]
        public void RemoveFirst_OnlyItem_LeavesUsableEmptyList()
        {
            var list = MakeList(1);
            list.RemoveFirst();
            AssertContents(list);
            list.AddLast(5);
            AssertContents(list, 5);
            Assert.AreEqual(5, list.RemoveLast(), "The tail still points at the removed node.");
        }

        [Test, Category(Part)]
        public void RemoveLast_OnlyItem_LeavesUsableEmptyList()
        {
            var list = MakeList(1);
            list.RemoveLast();
            AssertContents(list);
            list.AddFirst(5);
            AssertContents(list, 5);
            Assert.AreEqual(5, list.RemoveFirst(), "The head still points at the removed node.");
        }

        [Test, Category(Part)]
        public void RemoveAt_Front()
        {
            var list = MakeList(1, 2, 3);
            list.RemoveAt(0);
            AssertContents(list, 2, 3);
        }

        [Test, Category(Part)]
        public void RemoveAt_Middle()
        {
            var list = MakeList(1, 2, 3, 4);
            list.RemoveAt(2);
            AssertContents(list, 1, 2, 4);
        }

        [Test, Category(Part)]
        public void RemoveAt_Last()
        {
            var list = MakeList(1, 2, 3);
            list.RemoveAt(2);
            AssertContents(list, 1, 2);
            list.AddLast(9);
            AssertContents(list, 1, 2, 9);
        }

        [Test, Category(Part)]
        public void RemoveAt_Middle_KeepsBackwardLinks()
        {
            var list = MakeList(1, 2, 3, 4);
            list.RemoveAt(1);
            AssertBackwards(list, 4, 3, 1);
        }

        [Test, Category(Part)]
        public void RemoveAt_OutOfRange_Throws()
        {
            var list = MakeList(1, 2, 3);
            Assert.Throws<ArgumentOutOfRangeException>(() => list.RemoveAt(3));
            Assert.Throws<ArgumentOutOfRangeException>(() => list.RemoveAt(-1));
            AssertContents(list, 1, 2, 3);
        }

        [Test, Category(Part)]
        public void Contains_FindsStoredItem()
        {
            var list = MakeList(1, 2, 3);
            Assert.IsTrue(list.Contains(3));
        }

        [Test, Category(Part)]
        public void Contains_FalseForMissingItem()
        {
            var list = MakeList(1, 2, 3);
            Assert.IsFalse(list.Contains(4));
        }

        [Test, Category(Part)]
        public void Clear_EmptiesTheList()
        {
            var list = MakeList(1, 2, 3);
            list.Clear();
            AssertContents(list);
            list.AddLast(4);
            AssertContents(list, 4);
        }

        private static DoublyLinkedList<int> MakeList(params int[] values)
        {
            var list = new DoublyLinkedList<int>();
            foreach (int value in values)
            {
                list.AddLast(value);
            }

            return list;
        }

        private static void AssertContents(DoublyLinkedList<int> list, params int[] expected)
        {
            Assert.AreEqual(expected.Length, list.Count, "Wrong Count.");
            var seen = new List<int>();
            foreach (int item in list)
            {
                seen.Add(item);
            }

            CollectionAssert.AreEqual(expected, seen, "Wrong items, reading from the head.");
        }

        private static void AssertBackwards(DoublyLinkedList<int> list, params int[] expected)
        {
            var seen = new List<int>();
            while (list.Count > 0)
            {
                seen.Add(list.RemoveLast());
            }

            CollectionAssert.AreEqual(expected, seen, "Wrong items, reading from the tail: a Prev link is broken.");
        }
    }
}
