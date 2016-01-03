using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Webcorp.common;

namespace Webcorp.Model
{
    [CollectionName("param")]
    public class Parametre : ErpEntity
    {
        [BsonId(IdGenerator = typeof(ParameterIdGenerator))]
        public override string Id { get; set; }

        [BsonElement("num"),BsonRequired]
        public string Numero { get; set; }

        int? _vali;
        [BsonElement("vali"),BsonIgnoreIfNull]
        public int? ValeurEntier { get { return _vali; } set { this.SetAndRaise(ref _vali, value); } }

        double? _vald;
        [BsonElement("vald"), BsonIgnoreIfNull]
        public double? ValeurDouble { get { return _vald; } set { this.SetAndRaise(ref _vald, value); } }

        bool? _valb;
        [BsonElement("valb"), BsonIgnoreIfNull]
        public bool? ValeurBooleen { get { return _valb; } set { this.SetAndRaise(ref _valb, value); } }

        string _valt;
        [BsonElement("valt"), BsonIgnoreIfNull]
        public string ValeurTexte { get { return _valt; } set { this.SetAndRaise(ref _valt, value); } }
    }

    [CollectionName("pparam")]
    public class PParametre : Entity
    {
        [BsonId]
        public override string Id { get; set; }
        public string Numero{ get; set; }
        [BsonElement("desc"), BsonRequired]
        public string Description { get; set; }
        [BsonElement("type"), BsonRequired]
        public TypeParametre TypeParametre { get; set; }
        [BsonElement("aide"), BsonIgnoreIfNull]
        public string Aide { get; set; }
    }
    public enum TypeParametre
    {
        Entier,
        Double,
        Booleen,
        Texte

    }
    public class ParameterIdGenerator : IIdGenerator
    {

        public object GenerateId(object container, object document)
        {
            var art = document as Parametre;
            return string.Format("{0}-{1}", art.Societe, art.Numero);
            //return "" + Guid.NewGuid().ToString();
        }

        public bool IsEmpty(object id)
        {
            return id == null || String.IsNullOrEmpty(id.ToString());
        }
    }
}
