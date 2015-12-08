using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Webcorp.Model;

namespace Webcorp.lib.onedcut
{
    [DebuggerDisplay("Item Need={Need}")]
    public class Beam : CustomReactiveObject
    {
        private int _need;
        private int _length;
        public Beam(int need, int length):this(need,length,0,0)
        {
           
        }
        public Beam(int need, int length,int removeStart,int removeEnd)
        {
            this._need = this.Need = need;
            this.Length = length;
            this.RemoveStart = removeStart;
            this.RemoveEnd = removeEnd;
        }
        public int Length { get { return _length + RemoveStart + RemoveEnd; } set { _length = value; } }

        public int Need;

        public int RemoveStart { get; private set; }

        public int RemoveEnd { get; private set; }

        public int TotalLength => _need *( Length+RemoveStart+RemoveEnd);

        public void Reset()
        {
            Need = _need;
        }

        public override string ToString()
        {
            return "Item Need:" + Need;
        }
    }
}
