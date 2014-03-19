using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LFUSharp.Core.Dictionary
{
    public class NullValueDictionary<T, U> : Dictionary<T, U>, INullValueDictionary<T, U>
    where U : class
    {
        U INullValueDictionary<T, U>.this[T key]
        {
            get
            {
                U val;
                this.TryGetValue(key, out val);
                return val;
            }
        }
    }
}
