using MongoDB.Bson.Serialization.Attributes;

using System.Diagnostics;
using Webcorp.unite;
using System.Linq;
using System.Reactive.Linq;
using System;
using ReactiveUI;
using Webcorp.Model.Quotation;
using MongoDB.Bson.Serialization;
using Webcorp.common;
using System.Runtime.Serialization;

namespace Webcorp.Model
{
    [DebuggerDisplay("Societe={Societe} Code={Code},Libelle={Libelle}"),CollectionName("article")]
    [DataContract]
    [Serializable]
    public class Article:ErpEntity
    {

        Nomenclatures _nomenclatures ;
        MouvementsStocks _mvtStocks;
        Besoins _besoins;
        Productions _productions;
        Format _format ;
        Currency _tarif0;
        string _material;
      


       
        [BsonId(IdGenerator = typeof(ArticleIdGenerator))]
        public override string Id { get; set; }

       
        string _code, _libelle;
        [BsonRequired]
        [KeyProvider]
        [BsonElement("artcod")]
        public string Code { get { return _code; } set { this.SetAndRaise(ref _code, value); } }
        [BsonIgnoreIfNull]
        [BsonElement("artlib")]
        public string Libelle { get { return _libelle; } set { this.SetAndRaise(ref _libelle, value); } } 
        [BsonRequired]
        [BsonElement("typart")]
        public ArticleType TypeArticle { get; set; }

        [BsonElement("artmat")]
        [BsonIgnoreIfNull]
        public string Material { get{ return _material; } set { this.SetAndRaise(ref _material, value); } }

        [BsonElement("gesto")]
        [BsonIgnoreIfNull]
        public bool? GererEnstock { get; set; } = null;
        [BsonElement("artfan")]
        [BsonIgnoreIfNull]
        public bool? ArticleFantome { get; set; } = null;
        [BsonElement("stoneg")]
        [BsonIgnoreIfNull]
        public bool? AutoriseStockNegatif { get; set; } = null;
        [BsonElement("geslot")]
        [BsonIgnoreIfNull]
        public bool? GestionParLot { get; set; } = null;
        [BsonElement("stomin")]
        [BsonIgnoreIfNull]
        public int? StockMini { get; set; } = null;
        [BsonElement("stomax")]
        [BsonIgnoreIfNull]
        public int? StockMaxi { get; set; } = null;
        [BsonElement("lotapp")]
        [BsonIgnoreIfNull]
        public int? QuantiteMiniReappro { get; set; } = null;
        [BsonElement("stophy")]
        [BsonIgnoreIfNull]
        public int? StockPhysique { get; set; } = null;
        [BsonElement("stores")]
        [BsonIgnoreIfNull]
        public int? StockReservee { get; set; } = null;
        [BsonElement("stoatt")]
        [BsonIgnoreIfNull]
        public int? StockAttendu { get; set; } = null;

        public int? StockDisponible => StockPhysique - StockReservee;

        [BsonElement("ctprep")]
        [BsonIgnoreIfNull]
        public Currency CoutPreparation { get; set; } = null;
        [BsonElement("ctmp")]
        [BsonIgnoreIfNull]
        public Currency CoutMP { get; set; } = null;
        [BsonElement("ctmo")]
        [BsonIgnoreIfNull]
        public Currency CoutMO { get; set; } = null;
        [BsonElement("ctst")]
        [BsonIgnoreIfNull]
        public Currency CoutST { get; set; } =null;
        [BsonElement("ctfg")]
        [BsonIgnoreIfNull]
        public Currency CoutFG { get; set; } = null;

        public Currency CoutTotal => CoutMP + CoutMO+CoutST+CoutFG;

       
        [BsonElement("format")]
        [BsonIgnoreIfNull]
        public Format Format { get { return _format; } set { this.SetAndRaise(ref _format, value); } }
        [BsonIgnoreIfNull]
        public MassLinear MassLinear { get; set; } = null;
        [BsonIgnoreIfNull]
        public AreaLinear AreaLinear { get; set; } = null;
        [BsonIgnoreIfNull]
        public AreaMass AreaMass { get; set; } = null;
        [BsonIgnoreIfNull]
        public MassCurrency MassCurrency { get; set; } = null;
        [BsonIgnore]
        public Currency CostLinear => MassLinear * MassCurrency;
        [BsonElement("mvtsto")]
        [BsonIgnoreIfNull]
        public MouvementsStocks MouvementsStocks { get { return _mvtStocks; } set { this.SetAndRaise(ref _mvtStocks, value); } }
        [BsonElement("besoins"),BsonIgnoreIfNull]
        public Besoins Besoins { get { return _besoins; } set { this.SetAndRaise(ref _besoins, value); } }
        [BsonIgnoreIfNull]
        [BsonElement("nomenc")]
        public Nomenclatures Nomenclatures { get { return _nomenclatures; } set { this.SetAndRaise(ref _nomenclatures, value); } }
        [BsonElement("vernom")]
        [BsonIgnoreIfNull]
        public int? NomenclatureVersion { get; set; } = null;

        [BsonElement("tarif0")]
        [BsonIgnoreIfNull]
        public Currency Tarif0 { get { return _tarif0; } set { this.SetAndRaise(ref _tarif0, value); } }
        [BsonElement("prod"),BsonIgnoreIfNull]
        public Productions Productions { get { return _productions; } set { this.SetAndRaise(ref _productions, value); } }
    }

    public class ArticleIdGenerator : IIdGenerator
    {

        public object GenerateId(object container, object document)
        {
            var art = document as Article;
            return string.Format("{0}-{1}", art.Societe, art.Code);
            //return "" + Guid.NewGuid().ToString();
        }

        public bool IsEmpty(object id)
        {
            return id == null || String.IsNullOrEmpty(id.ToString());
        }
    }
}
