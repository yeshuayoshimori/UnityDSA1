using System;
using System.Collections.Generic;
using ADS.A1;
using NUnit.Framework;

namespace ADS.Tests
{
    public class DynamicArrayTests
    {
        private const string Part = "Part 1 DynamicArray";
        private const string InClass = "Week 2 in class";

        [Test, Category(Part)]
        public void NewArray_HasCountZero()
        {
            var array = new DynamicArray<int>();
            Assert.AreEqual(0, array.Count);
        }

        [Test, Category(Part)]
        public void NewArray_HasCapacityFour()
        {
            var array = new DynamicArray<int>();
            Assert.AreEqual(4, array.Capacity);
        }

        [Test, Category(Part)]
        public void Add_IncreasesCount()
        {
            var array = new DynamicArray<int>();
            array.Add(10);
            array.Add(20);
            Assert.AreEqual(2, array.Count);
        }

        [Test, Category(Part)]
        public void Add_KeepsItemsInOrder()
        {
            var array = MakeArray(10, 20, 30);
            Assert.AreEqual(10, array[0]);
            Assert.AreEqual(20, array[1]);
            Assert.AreEqual(30, array[2]);
        }

        [Test, Category(Part)]
        public void Add_FifthItem_DoublesCapacityToEight()
        {
            var array = MakeArray(0, 1, 2, 3);
            Assert.AreEqual(4, array.Capacity, "Four items should still fit in the starting capacity.");
            array.Add(4);
            Assert.AreEqual(8, array.Capacity, "Adding a fifth item should double the capacity.");
        }

        [Test, Category(Part)]
        public void Add_NinthItem_DoublesCapacityToSixteen()
        {
            var array = MakeArray(0, 1, 2, 3, 4, 5, 6, 7);
            Assert.AreEqual(8, array.Capacity);
            array.Add(8);
            Assert.AreEqual(16, array.Capacity);
        }

        [Test, Category(Part)]
        public void Add_TenThousandItems_KeepsOrder()
        {
            var array = new DynamicArray<int>();
            for (int i = 0; i < 10000; i++)
            {
                array.Add(i);
            }

            Assert.AreEqual(10000, array.Count);
            for (int i = 0; i < 10000; i++)
            {
                Assert.AreEqual(i, array[i], "Item " + i + " moved during a resize.");
            }
        }

        [Test, Category(Part)]
        public void Indexer_Set_ReplacesItem()
        {
            var array = MakeArray(10, 20);
            array[1] = 99;
            Assert.AreEqual(99, array[1]);
            Assert.AreEqual(2, array.Count, "Replacing an item shouldn't change Count.");
        }

        [Test, Category(Part)]
        public void Works_WithStrings()
        {
            var array = new DynamicArray<string>();
            array.Add("sword");
            array.Add("shield");
            Assert.AreEqual("shield", array[1]);
        }

        [Test, Category(Part)]
        public void Insert_AtFront_ShiftsItemsRight()
        {
            var array = MakeArray(1, 2, 3);
            array.Insert(0, 0);
            AssertContents(array, 0, 1, 2, 3);
        }

        [Test, Category(Part)]
        public void Insert_InMiddle_ShiftsLaterItemsRight()
        {
            var array = MakeArray(1, 2, 4);
            array.Insert(2, 3);
            AssertContents(array, 1, 2, 3, 4);
        }

        [Test, Category(Part)]
        public void Insert_AtEnd_WorksLikeAdd()
        {
            var array = MakeArray(1, 2);
            array.Insert(2, 3);
            AssertContents(array, 1, 2, 3);
        }

        [Test, Category(Part)]
        public void Insert_WhenFull_Grows()
        {
            var array = MakeArray(1, 2, 3, 4);
            array.Insert(1, 9);
            Assert.AreEqual(8, array.Capacity);
            AssertContents(array, 1, 9, 2, 3, 4);
        }

        [Test, Category(Part)]
        public void RemoveAt_Front_ShiftsItemsLeft()
        {
            var array = MakeArray(1, 2, 3);
            array.RemoveAt(0);
            AssertContents(array, 2, 3);
        }

        [Test, Category(Part)]
        public void RemoveAt_Middle_ShiftsLaterItemsLeft()
        {
            var array = MakeArray(1, 2, 3, 4);
            array.RemoveAt(1);
            AssertContents(array, 1, 3, 4);
        }

        [Test, Category(Part)]
        public void RemoveAt_Last_LeavesTheRest()
        {
            var array = MakeArray(1, 2, 3);
            array.RemoveAt(2);
            AssertContents(array, 1, 2);
        }

        [Test, Category(Part)]
        public void Clear_EmptiesButKeepsCapacity()
        {
            var array = MakeArray(1, 2, 3, 4, 5);
            array.Clear();
            Assert.AreEqual(0, array.Count);
            Assert.AreEqual(8, array.Capacity, "Clear shouldn't shrink the array.");
            array.Add(7);
            AssertContents(array, 7);
        }

        [Test, Category(Part)]
        public void Contains_FindsStoredItem()
        {
            var array = new DynamicArray<string>();
            array.Add("sword");
            array.Add("shield");
            Assert.IsTrue(array.Contains("shield"));
        }

        [Test, Category(Part)]
        public void Contains_IgnoresRemovedItems()
        {
            var array = MakeArray(1, 2, 3);
            array.RemoveAt(2);
            Assert.IsFalse(array.Contains(3), "A removed item is still being found.");
        }

        [Test, Category(Part), Category(InClass)]
        public void Indexer_Get_ThrowsAtMinusOne()
        {
            var array = MakeArray(1, 2, 3);
            Assert.Throws<ArgumentOutOfRangeException>(() => { int x = array[-1]; });
        }

        [Test, Category(Part), Category(InClass)]
        public void Indexer_Get_ThrowsAtCount()
        {
            var array = MakeArray(1, 2, 3);
            Assert.Throws<ArgumentOutOfRangeException>(() => { int x = array[3]; });
        }

        [Test, Category(Part), Category(InClass)]
        public void Indexer_Set_ThrowsAtCount()
        {
            var array = MakeArray(1, 2, 3);
            Assert.Throws<ArgumentOutOfRangeException>(() => array[3] = 9);
        }

        [Test, Category(Part), Category(InClass)]
        public void Insert_PastCount_Throws()
        {
            var array = MakeArray(1, 2, 3);
            Assert.Throws<ArgumentOutOfRangeException>(() => array.Insert(4, 9));
            Assert.Throws<ArgumentOutOfRangeException>(() => array.Insert(-1, 9));
            AssertContents(array, 1, 2, 3);
        }

        [Test, Category(Part), Category(InClass)]
        public void RemoveAt_OnEmptyArray_Throws()
        {
            var array = new DynamicArray<int>();
            Assert.Throws<ArgumentOutOfRangeException>(() => array.RemoveAt(0));
            Assert.AreEqual(0, array.Count, "Count went negative.");
        }

        [Test, Category(Part), Category(InClass)]
        public void RemoveAt_AtCount_Throws()
        {
            var array = MakeArray(1, 2, 3);
            Assert.Throws<ArgumentOutOfRangeException>(() => array.RemoveAt(3));
            AssertContents(array, 1, 2, 3);
        }

        [Test, Category(Part), Category(InClass)]
        public void Foreach_VisitsItemsInOrder()
        {
            var array = MakeArray(5, 6, 7);
            var seen = new List<int>();
            foreach (int item in array)
            {
                seen.Add(item);
            }

            CollectionAssert.AreEqual(new[] { 5, 6, 7 }, seen);
        }

        [Test, Category(Part), Category(InClass)]
        public void Foreach_OnEmptyArray_DoesNothing()
        {
            var array = new DynamicArray<int>();
            int visits = 0;
            foreach (int item in array)
            {
                visits++;
            }

            Assert.AreEqual(0, visits);
        }

        [Test, Category(Part), Category(InClass)]
        public void Foreach_StopsAtCount_NotCapacity()
        {
            var array = MakeArray(1, 2, 3, 4, 5);
            int visits = 0;
            foreach (int item in array)
            {
                visits++;
            }

            Assert.AreEqual(5, visits, "foreach visited empty slots past Count.");
        }

        private static DynamicArray<int> MakeArray(params int[] values)
        {
            var array = new DynamicArray<int>();
            foreach (int value in values)
            {
                array.Add(value);
            }

            return array;
        }

        private static void AssertContents(DynamicArray<int> array, params int[] expected)
        {
            Assert.AreEqual(expected.Length, array.Count, "Wrong Count.");
            for (int i = 0; i < expected.Length; i++)
            {
                Assert.AreEqual(expected[i], array[i], "Wrong item at index " + i + ".");
            }
        }
    }
}
