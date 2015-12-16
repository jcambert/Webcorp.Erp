using MongoDB.Bson.Serialization.Attributes;
using Webcorp.unite;
using ReactiveUI;
namespace Webcorp.Model
{
    public class Nomenclature:CustomReactiveObject
    {
        int  _ordre,_trans;
        double _qte;
        Time _prep, _exe;
        string _code, _lib,_typ;
        TauxHorraire _th;

        // [BsonElement("code")]
        //[BsonRequired]
        // public string Code { get { return _code; } set { this.RaiseAndSetIfChanged(ref _code, value); } }
        Article _article;
        [BsonElement("article")]
        public Article Article { get { return _article; } set { this.RaiseAndSetIfChanged(ref _article, value); } }

        [BsonElement("ordre")]
        public int Ordre { get { return _ordre; } set { this.RaiseAndSetIfChanged(ref _ordre, value); } }

        [BsonElement("qte")]
        [BsonRequired]
        public double Quantite { get { return _qte; } set { this.RaiseAndSetIfChanged(ref _qte, value); } }

        [BsonElement("prep")]
        [BsonIgnoreIfNull]
        public unite.Time Preparation { get { return _prep; } set { this.RaiseAndSetIfChanged(ref _prep, value); } }
        
        [BsonElement("exec")]
        [BsonIgnoreIfNull]
        public unite.Time Execution { get { return _exe; } set { this.RaiseAndSetIfChanged(ref _exe, value); } }

        [BsonElement("libe")]
        [BsonIgnoreIfNull]
        public string Libelle { get { return _lib; } set { this.RaiseAndSetIfChanged(ref _lib, value); } }

        [BsonElement("th")]
        [BsonIgnoreIfNull]
        public unite.TauxHorraire TauxHorraire { get { return _th; } set { this.RaiseAndSetIfChanged(ref _th, value); } }

        [BsonElement("trans")]
        [BsonIgnoreIfNull]
        public int TempsTransfert { get { return _trans; } set { this.RaiseAndSetIfChanged(ref _trans, value); } }

        [BsonElement("type")]
        [BsonIgnoreIfNull]
        public  string Type { get { return _typ; } set { this.RaiseAndSetIfChanged(ref _typ, value); } }

        [BsonElement("version")]
        public int Version { get; set; }

        bool _isActive = true;
        [BsonElement("active")]
        public bool IsActive { get { return _isActive; } set { this.RaiseAndSetIfChanged(ref _isActive, value); } } 
    }
}
