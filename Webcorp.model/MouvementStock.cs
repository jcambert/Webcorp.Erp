using MongoDB.Bson.Serialization.Attributes;
using System;

namespace Webcorp.Model
{

    public enum MouvementSens
    {
        Entree,
        Sortie,
        Inventaire  
    }
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
    }
}