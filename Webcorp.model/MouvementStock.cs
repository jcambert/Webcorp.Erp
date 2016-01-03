using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Diagnostics;

namespace Webcorp.Model
{

    public enum MouvementSens
    {
        Entree,
        Sortie,
        Inventaire  
    }

    public enum MouvementGenre
    {
        Expedition,
        Fabrication,
        Reception,
        Consommation,
        Manuel,
        Inventaire
    }
    [DebuggerDisplay("Quantite={Quantite},LastStock={LastStock}")]
    public class MouvementStock:CustomReactiveObject
    {

        [BsonRequired]
        [BsonElement("mvtsen")]
        public MouvementSens Sens { get; set; }
        [BsonRequired]
        [BsonElement("mvtdat")]
        public DateTime Date { get; set; } = DateTime.Now;
        [BsonRequired]
        [BsonElement("mvtqte")]
        public int Quantite { get; set; }
        [BsonElement("lassto")]
        public int LastStock { get;  set; }
        [BsonElement("mvtref"),BsonIgnoreIfNull]
        public string Reference { get; set; }
        [BsonElement("mvtgen"),BsonRequired]
        public MouvementGenre Genre { get; set; }
        [BsonElement("crepar"),BsonRequired]
        public string CreerPar { get; set; }
        [BsonElement("crele"), BsonRequired]
        public DateTime CreerLe { get; set; } = DateTime.Now;
    }
}