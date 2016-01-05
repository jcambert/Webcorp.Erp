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
      


       
        [BsonId(IdGenerator = typeof(ArticleIdGenerator)), DataMember(Name = "id")]
        public override string Id { get; set; }

       
        string _code, _libelle;
        [BsonRequired]
        [KeyProvider]
        [BsonElement("artcod"), DataMember(Name = "artcod")]
        public string Code { get { return _code; } set { this.SetAndRaise(ref _code, value); } }
        [BsonIgnoreIfNull]
        [BsonElement("artlib"), DataMember(Name = "artlib")]
        public string Libelle { get { return _libelle; } set { this.SetAndRaise(ref _libelle, value); } } 
        [BsonRequired]
        [BsonElement("typart"), DataMember(Name = "typart")]
        public ArticleType TypeArticle { get; set; }

        [BsonElement("artmat"), DataMember(Name = "aertmat")]
        [BsonIgnoreIfNull]
        public string Material { get{ return _material; } set { this.SetAndRaise(ref _material, value); } }

        [BsonElement("gesto"), DataMember(Name = "gesto")]
        [BsonIgnoreIfNull]
        public bool? GererEnstock { get; set; } = null;
        [BsonElement("artfan"), DataMember(Name = "artfan")]
        [BsonIgnoreIfNull]
        public bool? ArticleFantome { get; set; } = null;
        [BsonElement("stoneg"), DataMember(Name = "stoneg")]
        [BsonIgnoreIfNull]
        public bool? AutoriseStockNegatif { get; set; } = null;
        [BsonElement("geslot"), DataMember(Name = "geslot")]
        [BsonIgnoreIfNull]
        public bool? GestionParLot { get; set; } = null;
        [BsonElement("stomin"), DataMember(Name = "stomin")]
        [BsonIgnoreIfNull]
        public int? StockMini { get; set; } = null;
        [BsonElement("stomax"), DataMember(Name = "stomax")]
        [BsonIgnoreIfNull]
        public int? StockMaxi { get; set; } = null;
        [BsonElement("lotapp"), DataMember(Name = "lotapp")]
        [BsonIgnoreIfNull]
        public int? QuantiteMiniReappro { get; set; } = null;
        [BsonElement("stophy"), DataMember(Name = "stophy")]
        [BsonIgnoreIfNull]
        public int? StockPhysique { get; set; } = null;
        [BsonElement("stores"), DataMember(Name = "stores")]
        [BsonIgnoreIfNull]
        public int? StockReservee { get; set; } = null;
        [BsonElement("stoatt"), DataMember(Name = "stoatt")]
        [BsonIgnoreIfNull]
        public int? StockAttendu { get; set; } = null;

        public int? StockDisponible => StockPhysique - StockReservee;

        [BsonElement("ctprep"), DataMember(Name = "ctprep")]
        [BsonIgnoreIfNull]
        public Currency CoutPreparation { get; set; } = null;
        [BsonElement("ctmp"), DataMember(Name = "ctmp")]
        [BsonIgnoreIfNull]
        public Currency CoutMP { get; set; } = null;
        [BsonElement("ctmo"), DataMember(Name = "ctmo")]
        [BsonIgnoreIfNull]
        public Currency CoutMO { get; set; } = null;
        [BsonElement("ctst"),DataMember(Name ="ctst")]
        [BsonIgnoreIfNull]
        public Currency CoutST { get; set; } =null;
        [BsonElement("ctfg"),DataMember(Name ="ctfg")]
        [BsonIgnoreIfNull]
        public Currency CoutFG { get; set; } = null;

        [IgnoreDataMember]
        public Currency CoutTotal => CoutMP + CoutMO+CoutST+CoutFG;

       
        [BsonElement("format"), DataMember(Name = "format")]
        [BsonIgnoreIfNull]
        public Format Format { get { return _format; } set { this.SetAndRaise(ref _format, value); } }
        [BsonElement("masslinear"), DataMember(Name = "masslinear")]
        [BsonIgnoreIfNull]
        public MassLinear MassLinear { get; set; } = null;
        [BsonElement("arealinear"), DataMember(Name = "arealinear")]
        [BsonIgnoreIfNull]
        public AreaLinear AreaLinear { get; set; } = null;
        [BsonElement("areamass"), DataMember(Name = "areamass")]
        [BsonIgnoreIfNull]
        public AreaMass AreaMass { get; set; } = null;
        [BsonElement("masscurrency"), DataMember(Name = "masscurency")]
        [BsonIgnoreIfNull]
        public MassCurrency MassCurrency { get; set; } = null;
        [BsonIgnore]
        public Currency CostLinear => MassLinear * MassCurrency;
        [BsonElement("mvtsto")]
        [BsonIgnoreIfNull,DataMember(Name ="mvtsto")]
        public MouvementsStocks MouvementsStocks { get { return _mvtStocks; } set { this.SetAndRaise(ref _mvtStocks, value); } }
        [BsonElement("besoins"),BsonIgnoreIfNull,DataMember(Name ="besoins")]
        public Besoins Besoins { get { return _besoins; } set { this.SetAndRaise(ref _besoins, value); } }
        [BsonIgnoreIfNull]
        [BsonElement("nomenc"),DataMember(Name ="nomenc")]
        public Nomenclatures Nomenclatures { get { return _nomenclatures; } set { this.SetAndRaise(ref _nomenclatures, value); } }
        [BsonElement("vernom"), DataMember(Name = "vernom")]
        [BsonIgnoreIfNull]
        public int? NomenclatureVersion { get; set; } = null;

        [BsonElement("tarif0"),DataMember(Name ="tarif0")]
        [BsonIgnoreIfNull]
        public Currency Tarif0 { get { return _tarif0; } set { this.SetAndRaise(ref _tarif0, value); } }
        [BsonElement("prod"),BsonIgnoreIfNull,DataMember(Name ="prod")]
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
