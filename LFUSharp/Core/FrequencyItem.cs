using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace LFUSharp.Core
{
    public class FrequencyItem<TKey>
    {
        public int Frequency
        {
            get;
            private set;
        }
        public FrequencyItem<TKey> Next
        {
            get;
            set;
        }
        public FrequencyItem<TKey> Previous
        {
            get;
            set;
        }
        internal List<TKey> _nodes = new List<TKey>();

        public FrequencyItem()
        {
            Frequency = 0;
            Next = this;
            Previous = this;
        }

        public FrequencyItem(int frequency)
        {
            this.Frequency = frequency;
            this.Next = this;
            this.Previous = this;
        }

        public FrequencyItem(int frequency, FrequencyItem<TKey> previous, FrequencyItem<TKey> next)
        {
            this.Next = next;
            this.Previous = previous;
            Previous.Next = this;
            Next.Previous = this;
            this.Frequency = frequency;
        }

        public void Delete()
        {
            Previous.Next = Next;
            Next.Previous = Previous;
        }
    }
}
