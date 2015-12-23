using MongoDB.Bson.Serialization.Attributes;

using System.Diagnostics;
using Webcorp.unite;
using System.Linq;
using System.Reactive.Linq;
using System;
using ReactiveUI;
using Webcorp.Model.Quotation;
using MongoDB.Bson.Serialization;

namespace Webcorp.Model
{
    [DebuggerDisplay("Societe={Societe} Code={Code},Libelle={Libelle}")]
    public class Article:ErpEntity
    {

        Nomenclatures _nomenclatures ;
        MouvementsStocks _mvtStocks;
        Format _format ;
        Currency _tarif0;
        string _material;
        public Article()
        {

            // this.Changed.Where(x => x.PropertyName == "MouvementsStocks").Subscribe(x => {  _mvtStocks.Article = this; });
            // this.Changing.Subscribe(x => { }) ;

            //_source = ArticleSource.Interne;
          /*  _nomenclatures = new Nomenclatures(this);
            ShouldDispose(MouvementsStocks.CountChanged.Subscribe(_ => { IsChanged = true; }));
            ShouldDispose(MouvementsStocks.ItemChanged.Subscribe(_ => IsChanged = true));
            ShouldDispose(MouvementsStocks.ItemsAdded.Subscribe(_ =>
            {
                StockPhysique = MouvementsStocks.StockPhysique;

            }));
            ShouldDispose(MouvementsStocks.ItemsRemoved.Subscribe(_ =>
            {
                StockPhysique = MouvementsStocks.StockPhysique;

            }));
            
            ShouldDispose(this.ObservableForProperty(x => x.NomenclatureVersion).Subscribe(_ => UpdateCout()));
            ShouldDispose(Nomenclatures.CountChanged.Subscribe(_ =>
            {
                IsChanged = true;
                UpdateCout();
            }));
            ShouldDispose(Nomenclatures.ItemChanged.Where(x => x.PropertyName == "Source").Select(x => x.Sender).Subscribe(_ =>
            {
                IsChanged = true;
                UpdateCout();

            }));*/
        }


       
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
        /*[BsonRequired]
        [BsonElement("source")]
        public ArticleSource Source {
            get { return _source; }
            set { this.SetAndRaise(ref _source, value); } }
*/
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
        [BsonIgnoreIfNull]
        [BsonElement("nomenc")]
        public Nomenclatures Nomenclatures { get { return _nomenclatures; } set { this.SetAndRaise(ref _nomenclatures, value); } }
        [BsonElement("vernom")]
        [BsonIgnoreIfNull]
        public int? NomenclatureVersion { get; set; } = null;

        [BsonElement("tarif0")]
        [BsonIgnoreIfNull]
        public Currency Tarif0 { get { return _tarif0; } set { this.SetAndRaise(ref _tarif0, value); } }
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
