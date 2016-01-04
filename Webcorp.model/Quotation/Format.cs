using MongoDB.Bson.Serialization.Attributes;

using Webcorp.unite;
using ReactiveUI;
using System;
using System.Runtime.Serialization;

namespace Webcorp.Model.Quotation
{
    [DataContract]
    [Serializable]
    public class Format:CustomReactiveObject
    {
        Length _longueur, _largeur, _epaisseur;
        [BsonElement("lon")]
        [BsonIgnoreIfNull]
        public Length Longueur { get { return _longueur; } set { this.RaiseAndSetIfChanged(ref _longueur, value); } }
        [BsonElement("lar")]
        [BsonIgnoreIfNull]
        public Length Largeur { get { return _largeur; }  set { this.RaiseAndSetIfChanged(ref _largeur, value); } }
        [BsonElement("epa")]
        [BsonIgnoreIfNull]
        public Length Epaisseur { get { return _epaisseur; } set { this.RaiseAndSetIfChanged(ref _epaisseur, value); } }
        

        [BsonIgnoreIfNull]
        public MaterialPrice PrixMatiere { get; set; }

        public Material Matiere { get; set; }

        [BsonIgnore]
        public Mass Poids
        {
            get
            {
                try
                {
                    return (Longueur * Largeur * Epaisseur) * Matiere.Density;
                }
                catch
                {
                    return new Mass(0);
                }
            }
        }


        [BsonIgnore]
        public Area Surface
        {
            get
            {
                try {
                    return (Longueur * Largeur);
                }
                catch
                {
                    return new Area(0);
                }
            }
        }

        [BsonIgnore]
        public Area SurfaceAPeindre
        {
            get
            {
                try {
                    return Surface * 2;
                }
                catch
                {
                    return new Area(0);
                }
            }
        }

        [BsonIgnore]
        public Currency CoutMatiere
        {
            get
            {
                try
                {
                    return Poids * PrixMatiere.Cout;
                }
                catch
                {
                    return new Currency(0);
                }
            }
        }

    }
}