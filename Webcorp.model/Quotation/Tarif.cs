using MongoDB.Bson.Serialization.Attributes;
using Webcorp.unite;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using ReactiveUI;
using System.Reactive.Linq;

namespace Webcorp.Model.Quotation
{
    public class Tarif:CustomReactiveObject
    {
        public Tarif()
        {
            _cPrest = Configuration.DefaultCoeficientPrestation;
            _cMat = Configuration.DefaultCoeficientMatiere;
            _cComp = Configuration.DefaultCoeficientComposant;
            _cVente = 0;

           // var p=Observable.FromEventPattern<PropertyChangingEventArgs>(this, "PropertyChanged").Select(x => x.EventArgs.PropertyName == "PuBrut").ObserveOnDispatcher(System.Windows.Threading.DispatcherPriority.Normal);
           // ShouldDispose(p.Subscribe(_=> { }));
        }
        public Tarif(EntityQuotation entity): this()
        {
            
            entity.Tarifs.Add(this);
        }
        public List<TarifPrestation> Prestations { get; set; } = new List<TarifPrestation>();
        public int Quantite { get; set; }

        internal void Update(EntityQuotation entity)
        {
            double result0 = 0.0;
            Prestations.ForEach(p => result0 = result0 + p.Tarif.Value);
            result0 *= (1+CoeficientPrestation);
            Currency result = new Currency(result0);
            result = result + entity.CoutOperation + entity.CoutComposant*(1+CoeficientComposant) + entity.CoutMatiere * (1+CoeficientMatiere) + (entity.CoutPreparation + entity.CoutMethodes + entity.FAD + entity.Outillage) / Quantite;
            PuBrut = result;
        }

        Currency _pubrut;
        public Currency PuBrut
        {
            get { return _pubrut; } private set { this.RaiseAndSetIfChanged(ref _pubrut, value); }
        }


        double _cPrest;
        public double CoeficientPrestation { get { return _cPrest; }  set { this.RaiseAndSetIfChanged(ref _cPrest, value); } } 

        double _cMat;
        public double CoeficientMatiere { get { return _cMat; }  set { this.RaiseAndSetIfChanged(ref _cMat, value); } } 

        double _cComp;
        public double CoeficientComposant { get { return _cComp; } set { this.RaiseAndSetIfChanged(ref _cComp, value); } }

        double _cVente;
        public double CoeficientVente { get { return _cVente; } set { this.RaiseAndSetIfChanged(ref _cVente, value); } } 

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