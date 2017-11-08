using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intro2DGame.Utility
{
    public class DoubleLinkedChain<T>
    {
        private int Count = 0;

        private DoubleLinkedNode<T> First;
        private DoubleLinkedNode<T> Last;

        public DoubleLinkedChain()
        {
            this.First = null;
            this.Last = null;
        }

        public void Push(T o)
        {
            Count++;

            if (First == null)
            {

                DoubleLinkedNode<T> n = new DoubleLinkedNode<T>(o);
                this.First = n;
                this.Last = n;
            }
            else
            {

                DoubleLinkedNode<T> n = new DoubleLinkedNode<T>(o, Last, First);
                Last.SetNext(n);
                First.SetPrevious(n);
                this.Last = n;

            }
        }

        public T Pop()
        {
            if (First == null) throw new Exception("No Nodes to pop");

            Count--;
            if (First != Last)
            {
                DoubleLinkedNode<T> r = Last;
                T val = r.Value;
                r.GetPrevious().SetNext(First);
                r.GetNext().SetPrevious(r.GetPrevious());

                r = null;

                return val;
            }
            else
            {
                T val = First.Value;
                First = null;
                Last = null;
                return val;
            }


        }

        public void Clear()
        {
            First = null;
            Last = null;
        }

        public System.Collections.IEnumerable GetValues()
        {
            DoubleLinkedNode<T> n = First;

            do
            {
                yield return n.Value;
                n = n.GetNext();
            } while (n != First);
        }

    }

    public class DoubleLinkedNode<T>
    {
        private DoubleLinkedNode<T> Previous;
        private DoubleLinkedNode<T> Next;

        public T Value
        {
            get;
            private set;
        }

        public DoubleLinkedNode(T val)
        {
            this.Value = val;
            this.Previous = this;
            this.Next = this;
        }

        public DoubleLinkedNode(T val, DoubleLinkedNode<T> prev, DoubleLinkedNode<T> next)
        {
            this.Value = val;
            this.Previous = prev;
            this.Next = next;
        }

        public void SetNext(DoubleLinkedNode<T> next)
        {
            this.Next = next;
        }

        public DoubleLinkedNode<T> GetNext()
        {
            return this.Next;
        }

        public void SetPrevious(DoubleLinkedNode<T> previous)
        {
            this.Previous = previous;
        }

        public DoubleLinkedNode<T> GetPrevious()
        {
            return this.Previous;
        }
    }
}
