using MongoDB.Bson.Serialization.Attributes;
using Webcorp.unite;

namespace Webcorp.Model.Quotation
{
    public class TarifPrestation
    {
        [BsonIgnoreIfNull]
        public string Prestataire { get; set; }
        [BsonIgnoreIfNull]
        public string Prestation { get;  set; }
        [BsonRequired]
        public Currency Tarif { get;  set; }
        [BsonIgnoreIfNull]
        public string Commentaire { get; set; }
        [BsonDefaultValue(false)]
        public bool Estime { get; set; }
    }
}