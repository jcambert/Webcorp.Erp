using MongoDB.Bson.Serialization.Attributes;

using System.Diagnostics;
using Webcorp.reactive;
using Webcorp.unite;
using System.Linq;
using System.Reactive.Subjects;
using System.Reactive.Linq;
namespace Webcorp.Model
{
    using ReactiveUI;   
    [DebuggerDisplay("Article Code={Code},Libelle={Libelle}")]
    public class Article:Entity
    {
        MouvementsStocks _mvtStocks;
        public Article()
        {

            this.Changed.Where(x => x.PropertyName == "MouvementsStocks").Subscribe(x => {  _mvtStocks.Article = this; });
           // this.Changing.Subscribe(x => { }) ;
        }

        [BsonRequired]
        [KeyProvider]
        [BsonElement("artcod")]
        public string Code { get; set; }
        [BsonIgnoreIfNull]
        [BsonElement("artlib")]
        public string Libelle { get; set; } = "";
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
        public int StockMini { get; set; } = 0;
        [BsonElement("stomax")]
        public int StockMaxi { get; set; } = 0;
        [BsonElement("lotapp")]
        public int QuantiteMiniReappro { get; set; } = 0;
        [BsonElement("stophy")]
        public int StockPhysique { get; set; } = 0;
        [BsonElement("stores")]
        public int StockReservee { get; set; } = 0;
        [BsonElement("stoatt")]
        public int StockAttendu { get; set; } = 0;

        public int StockDisponible => StockPhysique - StockReservee;

        Density _density=new Density(0);
        [BsonElement("density")]
        public Density Density { get { return _density; } set { this.SetAndRaise(ref _density, value); } } 
        [BsonIgnoreIfNull]
        public MassLinear MassLinear { get; set; }
        [BsonIgnoreIfNull]
        public AreaLinear AreaLinear { get; set; }
        [BsonIgnoreIfNull]
        public AreaMass AreaMass { get; set; }
        [BsonIgnoreIfNull]
        public MassCurrency MassCurrency { get; set; }
        [BsonIgnore]
        public Currency CostLinear => MassLinear * MassCurrency;
        [BsonElement("mvtsto")]
        [BsonIgnoreIfNull]
        public MouvementsStocks MouvementsStocks { get { return _mvtStocks; } set { this.SetAndRaise(ref _mvtStocks, value); } }

        public Nomenclatures Nomenclatures { get; set; } = new Nomenclatures();
    }
}
