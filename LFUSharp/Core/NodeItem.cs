using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LFUSharp.Core
{
    public class NodeItem<TKey, TValue>
    {
        public TValue Item
        {
            get;
            private set;
        }

        public FrequencyItem<TKey> Node
        {
            get;
            internal set;
        }

        public NodeItem(TValue item, FrequencyItem<TKey> node)
        {
            this.Item = item;
            this.Node = node;
        }
    }
}
