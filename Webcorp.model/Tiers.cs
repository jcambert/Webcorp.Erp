using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webcorp.Model
{
    public class Addresse
    {

    }

    public abstract class Tiers:ErpEntity
    {
        [BsonId(IdGenerator = typeof(EntityIdGenerator))]
        public override string Id { get; set; }

        [KeyProvider]
        public string Nom { get; set; }

        public Addresse Adresse { get; set; }
    }

    public class Societe : Tiers
    {

    }

    public class Personne : Tiers
    {

    }
}
