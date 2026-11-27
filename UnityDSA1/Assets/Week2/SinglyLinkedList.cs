using System;
using System.Collections;
using System.Collections.Generic;

namespace ADS.Week2
{
    public class Node<T>
    {
        public T Value;
        public Node<T> Next;

        public Node(T value)
        {
            Value = value;
        }
    }

    public class SinglyLinkedList<T> : IEnumerable<T>
    {
        private Node<T> head;
        private Node<T> tail;
        private int count;

        public int Count => count;

        public void AddFirst(T item)
        {
            var node = new Node<T>(item);
            node.Next = head;
            head = node;

            if (tail == null)
            {
                tail = node;
            }

            count++;
        }

        public void AddLast(T item)
        {
            var node = new Node<T>(item);
            if (tail !=null)
            {
                tail.Next = node;
            }
            else
            {
                // empty list
                head = node;
            }
            tail = node;
            count++;
            
        }

        public IEnumerator<T> GetEnumerator()
        {
            for (var n = head; n != null; n = n.Next)
            {
                yield return n.Value;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
