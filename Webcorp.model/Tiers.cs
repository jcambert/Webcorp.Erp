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
        [BsonId(IdGenerator = typeof(EntityIdGenerator)),BsonIgnoreIfNull]
        public override string Id { get; set; }

        [KeyProvider]
        [BsonElement("nom")]
        public string Nom { get; set; }

        [BsonIgnoreIfNull]
        public Addresse Adresse { get; set; }
    }

    public class Societe : Tiers
    {

    }

    public class Personne : Tiers
    {

    }
}
