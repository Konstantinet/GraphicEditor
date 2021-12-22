using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace UniStorage
{
    public class UniversalStoradge<T>:IEnumerable<T>
    {
        private int Size;
        Node<T> First;
        Node<T> Head;
        public UniversalStoradge()
        {
            First = Head = null;
        }



        public int GetCount() => Size;
        public bool IsEmpty() => (Size == 0); 

        public void AddElement(T element)
        {
            Node<T> next = new Node<T>(element);
            if (First != null)
                Head.Next = next;
            else
                First = next;
            Head = next;
            Size++;
        }


        public void RemoveElement(T element)
        {
            Node<T> current = First;
            Node<T> previous = null;

            while (current != null)
            {
                if (current.Data.Equals(element))
                {

                    if (previous == null)
                    {
                        First = First.Next;
                        if (First == null)
                            Head = null;
                    }
                    else
                    {
                        previous.Next = current.Next;
                        if (current.Next == null)
                            Head = previous;
                    }
                    Size--;
                    return;
                }
                previous = current;
                current = current.Next;
            }
            return;
        }

        public Iterator<T> GetIterator()
        {
            return new Iterator<T>(this);
        }

        public class Iterator<T>
        {
            UniversalStoradge<T> Storadge;
            int Count;
            Node<T> Current;

            public T GetCurrent() => Current.Data;
            public Iterator(UniversalStoradge<T> st)
            {
                Count = 0;
                Storadge = st;
                Current = Storadge.First;
            }
            public void First()
            {
                Current = Storadge.First;
            }
            public void Next()
            {
                Count++;
                Current = Current.Next;
            }
            public bool IsEOL()
            {
                if (Count >= Storadge.GetCount())
                    return false;
                else
                    return true;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)this).GetEnumerator();
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            Node<T> current = First;
            while (current != null)
            {
                yield return current.Data;
                current = current.Next;
            }
        }
    }
}
