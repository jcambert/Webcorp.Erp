using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Webcorp.unite;

namespace Webcorp.Model.Quotation
{
    public class EntityQuotation
    {
        public EntityQuotation()
        {

        }
        public EntityQuotation(Quotation q)
        {
            q.Entities.Add(this);
            Commentaire = q.Commentaire;
            TauxHorraireMethodes = q.TauxHorraireMethodes;
            TempsMethodes = q.TempsMethodes;
            FAD = q.FAD;
            Outillage = q.Outillage;
            Difficulte = q.Difficulte;
            Delai = q.Delai;
            Ts = q.Ts;

        }
        public string Reference { get; set; }

        public string Plan { get; set; }

        public string Designation { get; set; }

        public string Indice { get; set; }



        //public FormatTole FormatTole { get; set; }

        public string Commentaire { get; set; }

        public TauxHorraire TauxHorraireMethodes { get; set; }

        public Time TempsMethodes { get; set; }

        public Currency FAD { get; set; }

        public Currency Outillage { get; set; }

        public Difficulte Difficulte { get; set; }

        public Delai Delai { get; set; }

        public TraitementSurface Ts { get; set; }

        public List<Operation> Operations { get; set; } = new List<Operation>();

        public List<Tarif> Tarifs { get; set; } = new List<Tarif>();

        public Currency CoutMethodes => TauxHorraireMethodes * TempsMethodes;

        public Currency CoutPreparation
        {
            get
            {
                Currency result = 0 * Currency.Euro;
                Operations.ForEach(op => result += op.CoutPreparation);
                return result;
            }
        }

        public Currency CoutOperation
        {
            get
            {
                Currency result = 0 * Currency.Euro;
                Operations.ForEach(op => result += op.CoutOperation);
                return result;
            }
        }

        public Currency CoutComposant => 0 * Currency.Euro;
        public Currency CoutMatiere
        {
            get
            {
                var result = 0 * Currency.Euro;
                Operations.ForEach(op => result += op.CoutMatiere);
                return result;
            }

        }

        
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            Tarifs.ForEach(t =>
            {
                t.Update(this);
                sb.AppendLine(t.ToString());
                sb.AppendLine("Pu Brut:" + t.PuBrut);
                sb.AppendLine("Qte:" + t.Quantite);
                sb.AppendLine("Coef:" + t.CoeficientVente);
                sb.AppendLine("Pu Vente:" + t.PuVente);

            });
            return sb.ToString();
        }
    }
}
