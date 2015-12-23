using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webcorp.Model
{
   public abstract class ErpEntity:Entity
    {
        string _soc;
        [BsonElement("soc")]
        [BsonRequired]
        public string Societe { get { return _soc; } set { this.SetAndRaise(ref _soc, value); } }
    }
}
