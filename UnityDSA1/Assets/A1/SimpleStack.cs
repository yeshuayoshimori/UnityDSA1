using System;

namespace ADS.A1
{
    public class SimpleStack<T> //top of the pile of "dishes" (analogy) is the last
    {
        private DynamicArray<T> items = new DynamicArray<T>(); //stors inside dynamic array #Y-CHK

        public int Count => items.Count;

        public void Push(T item) //adds to the top, in this case it is the end of the array 
        {
            items.Add(item);
        }

        public T Pop() //removes item, last added
        {
            if (items.Count == 0)
            {
                ThrowError("The stack is empty.");
            }
            //Top is last index || Count 3, || top is 2 (Count - 1)
            T item = items[items.Count - 1]; //saving, and removing
            items.RemoveAt(items.Count - 1); //remove
            return item;
        }

        public T Peek() //returnign without removing it, just point look at it
        {
            if (items.Count == 0)
            {
                ThrowError("The stack is empty.");
            }

            return items[items.Count - 1]; //looks at top only
        }

        private void ThrowError(string message) //throwing error or exceptions function
        {
            throw new InvalidOperationException(message);
        }
    }
}
//Example
//var s = new SimpleStack<string>();
//s.Push("Bow"); 
//s.Push("Megaton Hammer"); 
//s.Push("Boomerang"); 
//s.Peek();                //Boomerang
//s.Pop();                 //Boomerang
//s.Pop();                 //Megaton hammer
//s.Push("Hookshot");
//s.Pop();                 //Hookshot
//s.Pop();                 //Bow
//s.Pop();                 //error 