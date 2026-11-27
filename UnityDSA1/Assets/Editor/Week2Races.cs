using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using ADS.A1;
using ADS.Week2;
using UnityEditor;

namespace ADS.EditorTools
{
    public static class Week2Races
    {
        [MenuItem("ADS/Week 2/Race 1: insert at the front (1K, 10K, 50K)")]
        public static void RaceInsertAtFront()
        {
            var report = new StringBuilder();
            report.AppendLine("Race 1: milliseconds to insert n ints, each one at the front");
            report.AppendLine("n          DynamicArray.Insert(0)   SinglyLinkedList.AddFirst   List<T>.Insert(0)");

            foreach (int n in new[] { 1000, 10000, 50000 })
            {
                string array = TimeOrTodo(() =>
                {
                    var a = new DynamicArray<int>();
                    for (int i = 0; i < n; i++) a.Insert(0, i);
                });

                string linked = TimeOrTodo(() =>
                {
                    var l = new SinglyLinkedList<int>();
                    for (int i = 0; i < n; i++) l.AddFirst(i);
                });

                string list = TimeOrTodo(() =>
                {
                    var l = new List<int>();
                    for (int i = 0; i < n; i++) l.Insert(0, i);
                });

                report.AppendLine(string.Format("{0,-10} {1,-24} {2,-27} {3}", n.ToString("N0"), array, linked, list));
            }

            UnityEngine.Debug.Log(report.ToString());
        }

        [MenuItem("ADS/Week 2/Race 2: sum 1,000,000 items (List vs LinkedList)")]
        public static void RaceSum()
        {
            const int n = 1000000;
            var list = new List<int>(n);
            var linked = new LinkedList<int>();
            for (int i = 0; i < n; i++)
            {
                list.Add(i);
                linked.AddLast(i);
            }

            double bestList = double.MaxValue;
            double bestLinked = double.MaxValue;
            long check = 0;
            for (int run = 0; run < 5; run++)
            {
                bestList = Math.Min(bestList, Time(() =>
                {
                    long sum = 0;
                    foreach (int x in list) sum += x;
                    check += sum;
                }));

                bestLinked = Math.Min(bestLinked, Time(() =>
                {
                    long sum = 0;
                    foreach (int x in linked) sum += x;
                    check += sum;
                }));
            }

            var report = new StringBuilder();
            report.AppendLine("Race 2: milliseconds to sum 1,000,000 ints with foreach (best of 5)");
            report.AppendLine("Both are O(n): one pass over every item.");
            report.AppendLine(string.Format("List<int>        {0:0.00} ms", bestList));
            report.AppendLine(string.Format("LinkedList<int>  {0:0.00} ms   ({1:0.0}x slower)", bestLinked, bestLinked / bestList));
            report.AppendLine("(checksum " + check + ")");
            UnityEngine.Debug.Log(report.ToString());
        }

        private static string TimeOrTodo(Action work)
        {
            try
            {
                return Time(work).ToString("0.0") + " ms";
            }
            catch (NotImplementedException)
            {
                return "not done yet";
            }
        }

        private static double Time(Action work)
        {
            var watch = Stopwatch.StartNew();
            work();
            watch.Stop();
            return watch.Elapsed.TotalMilliseconds;
        }
    }
}
