using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webcorp.Model.Quotation
{
    public class Fournisseur:Entity
    {
        [BsonId(IdGenerator = typeof(EntityIdGenerator))]
        public override string Id { get; set; }
        public string Nom { get; set; }
    }
}
