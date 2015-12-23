using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Webcorp.Model;
using Webcorp.unite;

namespace Webcorp.lib.onedcut
{
    [DebuggerDisplay("BeamToCut Item Need={Need}")]
    public class BeamToCut:Entity
    {
        private int _need;
        private int _length;
        private Article _beam;
        public BeamToCut(int need, int length,Article beam):this(need,length,0,0,beam)
        {
           
        }
        public BeamToCut(int need, int length,int removeStart,int removeEnd,Article beam)
        {
            this._need = this.Need = need;
            this.Length = length;
            this.RemoveStart = removeStart;
            this.RemoveEnd = removeEnd;
            this._beam = beam;
        }

        [BsonId(IdGenerator = typeof(EntityIdGenerator))]
        public override string Id { get; set; }

        public int Length { get { return _length + RemoveStart + RemoveEnd; } set { _length = value; } }

        public int Need;

        public int RemoveStart { get; private set; }

        public int RemoveEnd { get; private set; }

        public int TotalLength => _need *( Length+RemoveStart+RemoveEnd);

        public Mass TotalMass => this._beam.MassLinear * _need;

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
