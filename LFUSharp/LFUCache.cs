using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LFUSharp.Core;
using LFUSharp.Core.Dictionary;

namespace LFUSharp
{
    public class LFUCache<TKey, TValue>
    {
        private NullValueDictionary<TKey, NodeItem<TKey, TValue>> _lfuHash;
        private FrequencyItem<TKey> _head;

        public LFUCache() : this(new TValue[] { }) { }

        public LFUCache(params TValue[] Items)
        {
            _lfuHash = new NullValueDictionary<TKey, NodeItem<TKey, TValue>>();
            _head = new FrequencyItem<TKey>();

            foreach (TValue i in Items)
            {
                Add(i);
            }
        }

        public TValue this[TKey key]
        {
            get
            {
                NodeItem<TKey, TValue> tmp = _lfuHash[key];
                if (tmp == null)
                {
                    throw new KeyNotFoundException();
                }
                FrequencyItem<TKey> freq = tmp.Node;
                FrequencyItem<TKey> next_freq = freq.Next;

                if (next_freq == _head || next_freq.Frequency != freq.Frequency + 1)
                {
                    next_freq = new FrequencyItem<TKey>(freq.Frequency + 1, freq, next_freq);
                }
                next_freq._nodes.Add(key);
                tmp.Node = next_freq;

                freq._nodes.Remove(key);

                if (freq._nodes.Count == 0)
                {
                    freq.Delete();
                }
                return tmp.Item;
            }
            set
            {
                if (_lfuHash.ContainsKey(key))
                {
                    throw new InvalidOperationException("Key already exists!");
                }
                FrequencyItem<TKey> freq = _head.Next;
                if (freq.Frequency != 1)
                {
                    freq = new FrequencyItem<TKey>(1, _head, freq);
                }

                freq._nodes.Add(key);
                _lfuHash.Add(key, new NodeItem<TKey, TValue>(value, freq));
            }
        }

        public virtual TKey Add(TValue item)
        {
            TKey key;
            if (item is IKeyable)
            {
                object tmp = ((IKeyable)item).UniqueKey();
                if (tmp is TKey)
                {
                    key = (TKey)tmp;
                }
                else
                {
                    throw new InvalidCastException("The IKeyable object did not return a value key type!");
                }
            }
            else
            {
                throw new InvalidCastException("The object is not a IKeyable type, so a key could not be obtained!");
            }

            this[key] = item;

            return key;
        }

        public NodeItem<TKey, TValue> LeastFrequentlyUsedNode
        {
            get
            {
                if (_lfuHash.Count == 0)
                    throw new Exception("The cache is empty!");

                return _lfuHash[_head.Next._nodes[0]];
            }
        }

        public TValue LeastFrequentlyUsedObject
        {
            get
            {
                if (_lfuHash.Count == 0)
                    throw new Exception("The cache is empty!");

                return _lfuHash[_head.Next._nodes[0]].Item;
            }
        }
    }
}
