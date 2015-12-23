using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Webcorp.unite;

namespace Webcorp.Model.Quotation
{
    public class Prestataire:Entity
    {
        [BsonId(IdGenerator = typeof(EntityIdGenerator))]
        public override string Id { get; set; }

        [KeyProvider]
        public string Nom { get; set; }


    }

    public class Prestation<T,U> where T : Unit<T> where U : Unit<U>
    {
        public string Nom { get; set; }
        public List<Tranche<T,U>> Tranches { get; set; }
    }
}
