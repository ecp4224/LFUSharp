using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LFUSharp.Core.Dictionary
{
    public interface INullValueDictionary<T, U>
    where U : class
    {
        U this[T key] { get; }
    }
}
