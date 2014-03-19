using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LFUSharp.Core
{
    public interface IKeyable
    {
        object UniqueKey();
    }
}
