using System;
using System.Collections;
using System.Collections.Generic;

namespace ADS.A1
{
    public class DoublyNode<T>
    {
        public T Value;
        public DoublyNode<T> Next; //path forward is null/default
        public DoublyNode<T> Prev; //path back is null/default

        public DoublyNode(T value)
        {
            Value = value;
        }
    }

     public class DoublyLinkedList<T> : IEnumerable<T>
    {
        private DoublyNode<T> head;
        private DoublyNode<T> tail;
        private int count; 

        public int Count => count;

        public T this[int index]
        {
            get
            {
                CheckIndex(index);
                return GetNode(index).Value;
            }
            set
            {
                CheckIndex(index);
                GetNode(index).Value = value;
            }
        }

        public void AddFirst(T item)  //add before the head
        {
            var node = new DoublyNode<T>(item);
            node.Next = head;   //forward path leads to older

            if (head != null)
            {
                head.Prev = node;
            }
            head = node;

            if (tail == null)
            {
                tail = node;
            }

            count++;
        }

        public void AddLast(T item) //adding after tail
        {
            var node = new DoublyNode<T>(item);
            if (tail != null)
            {
                tail.Next = node; //old tail, forward to the new one
                node.Prev = tail;  //new, backward to the old 
            }
            else
            {
                //empty list
                head = node;
            }
            tail = node;
            count++;
        }

        public void Insert(int index, T item) //add at any given position
        {
            if (index < 0 || index > count)
            {
                throw new ArgumentOutOfRangeException(nameof(index), $"Index {index} is outside 0...{count}.");
            }

            if (index == 0)
            {
                AddFirst(item);
            }
            else if (index == count)
            {
                AddLast(item);
            }
            else //putting it between two existing "n"odes"
            {
                var node = new DoublyNode<T>(item);
                DoublyNode<T> after = GetNode(index);
                DoublyNode<T> before = after.Prev;

                node.Prev = before;
                node.Next = after;
                before.Next = node;
                after.Prev = node;
                count++;
            }
        }

        public T RemoveFirst()  //delete head and give back its value
        {
            if (head == null)
            {
                ThrowError("The list is empty.");
            }

            T value = head.Value; //save/store before removing
            head = head.Next; //the second becomes the head

            if (head != null)
            {
                head.Prev = null;
            }
            else
            {
                tail = null;
            }

            count--;
            return value;
        }

        public T RemoveLast() //delete tail and give back its value
        {
            if (tail == null)
            {
                ThrowError("The list is empty.");
            }

            T value = tail.Value;
            tail = tail.Prev;

            if (tail != null)
            {
                tail.Next = null;
            }
            else
            {
                head = null;
            }

            count--;
            return value;
        }

        public void RemoveAt(int index) //like insert, but removing at any position 
        {
            CheckIndex(index);

            if (index == 0)
            {
                RemoveFirst();
            }
            else if (index == count - 1)
            {
                RemoveLast();
            }
            else //closing a path, the ones at the sides skip over this node
            {
                DoublyNode<T> node = GetNode(index);
                node.Prev.Next = node.Next;
                node.Next.Prev = node.Prev;
                count--;
            }
        }

        public bool Contains(T item) //checking if there is the value is within here
        {
            EqualityComparer<T> comparer = EqualityComparer<T>.Default;
            for (var n = head; n != null; n = n.Next)
            {
                if (comparer.Equals(n.Value, item))
                {
                    return true;
                }
            }
            return false;
        }

        public void Clear()
        {
            head = null;
            tail = null;
            count = 0;
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

        private DoublyNode<T> GetNode(int index)
        {
            var n = head;
            for (int i = 0; i < index; i++)
            {
                n = n.Next;
            }
            return n;
        }

        private void CheckIndex(int index)
        {
            if (index < 0 || index >= count)
            {
                throw new ArgumentOutOfRangeException(nameof(index), $"Index {index} is outside 0...{count}.");
            }
        }

        private void ThrowError(string message)
        {
            throw new InvalidOperationException(message);
        }
    }
}
