using MongoDB.Bson.Serialization.Attributes;

using Webcorp.unite;

namespace Webcorp.Model.Quotation
{
    public class Format
    {
        public Length Longueur { get; set; }

        public Length Largeur { get; set; }

        public Length Epaisseur { get; set; }

        public Material Matiere { get; set; }

        [BsonIgnoreIfNull]
        public MaterialPrice PrixMatiere { get; set; }

        [BsonIgnore]
        public Mass Poids => (Longueur * Largeur * Epaisseur) * Matiere.Density;

        [BsonIgnore]
        public Area Surface => (Longueur * Largeur);

        [BsonIgnore]
        public Area SurfaceAPeindre => Surface * 2;

        [BsonIgnore]
        public Currency CoutMatiere => Poids * PrixMatiere.Cout;
       
    }
}