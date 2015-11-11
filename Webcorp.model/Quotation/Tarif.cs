using MongoDB.Bson.Serialization.Attributes;
using Webcorp.unite;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Webcorp.Model.Quotation
{
    public class Tarif
    {
        public Tarif()
        {
            
        }
        public Tarif(EntityQuotation entity)
        {
            
            entity.Tarifs.Add(this);
        }
        public List<TarifPrestation> Prestations { get; set; } = new List<TarifPrestation>();
        public int Quantite { get; set; }

        public void Update(EntityQuotation entity)
        {
            double result0 = 0.0;
            Prestations.ForEach(p => result0 = result0 + p.Tarif.Value);
            result0 *= CoeficientPrestation;
            Currency result = new Currency(result0);
            result = result + entity.CoutOperation + entity.CoutComposant + entity.CoutMatiere * CoeficientMatiere + (entity.CoutPreparation + entity.CoutMethodes + entity.FAD + entity.Outillage) / Quantite;
            PuBrut = result;
        }

       
        public Currency PuBrut
        {
            get; set;
        }

      

        public double CoeficientPrestation { get;  set; } = Configuration.DefaultCoeficientPrestation;

        public double CoeficientMatiere { get;  set; } = Configuration.DefaultCoeficientMatiere;

        public double CoeficientVente { get; set; } = 0;

        [BsonIgnore]
        public Currency PuVente=> PuBrut * (1 + CoeficientVente);

        

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("------------------------------");
            sb.AppendLine("Tarif Pour quantite de " + Quantite);
            Prestations.ForEach(p =>
            {
                sb.AppendLine("Prestation:" + p.Prestation + " Prix:" + p.Tarif.ToString("0.00 [eur]"));
            });

            return sb.ToString();
        }
    }
}