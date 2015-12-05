using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webcorp.lib.onedcut
{
    public class SortBySizeDescendant<T> : IComparer<T> where T : Item
    {
        public int Compare(T x, T y)
        {
            return x.Size < y.Size ? 1 : x.Size > y.Size ? -1 : 0;
        }
    }
}
