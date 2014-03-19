using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LFUSharp.Core;

namespace LFUSharp 
{
    public class LFUIntCache<TValue> : LFUCache<int, TValue>
    {
        public override int Add(TValue item)
        {
            int key;
            if (item is IKeyable)
            {
                object temp = ((IKeyable)item).UniqueKey();
                if (temp is int)
                {
                    key = (int)temp;
                }
                else
                {
                    key = item.GetHashCode();
                }
            }
            else
            {
                key = item.GetHashCode();
            }

            this[key] = item;
            return key;
        }
    }
}
