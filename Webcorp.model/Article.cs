using MongoDB.Bson.Serialization.Attributes;
using System.Diagnostics;
using Webcorp.unite;

namespace Webcorp.Model
{
    [DebuggerDisplay("Article Code={Code},Libelle={Libelle}")]
    public class Article:Entity
    {
        [BsonRequired]
        [KeyProvider]
        [BsonElement("artcod")]
        public string Code { get; set; }
        [BsonIgnoreIfNull]
        [BsonElement("artlib")]
        public string Libelle { get; set; }
        [BsonRequired]
        [BsonElement("typart")]
        public string TypeArticle { get; set; }
        [BsonElement("gesto")]
        public bool GererEnstock { get; set; } = true;
        [BsonElement("artfan")]
        public bool ArticleFantome { get; set; } = false;
        [BsonElement("stoneg")]
        public bool AutoriseStockNegatif { get; set; } = true;
        [BsonElement("geslot")]
        public bool GestionParLot { get; set; } = false;
        [BsonElement("stomin")]
        public int StockMini { get; set; }
        [BsonElement("stomax")]
        public int StockMaxi { get; set; }
        [BsonElement("lotapp")]
        public int QuantiteMiniReappro { get; set; }
        [BsonElement("stophy")]
        public int StockPhysique { get; set; }
        [BsonElement("stores")]
        public int StockReservee { get; set; }
        [BsonElement("stoatt")]
        public int StockAttendu { get; set; }

        public int StockDisponible => StockPhysique - StockReservee;

        [BsonIgnoreIfNull]
        public MassLinear MassLinear { get; set; }
        [BsonIgnoreIfNull]
        public AreaLinear AreaLinear { get; set; }
        [BsonIgnoreIfNull]
        public AreaMass AreaMass { get; set; }
        [BsonIgnoreIfNull]
        public MassCurrency MassCurrency { get; set; }

        public Currency CostLinear => MassLinear * MassCurrency;
    }
}
