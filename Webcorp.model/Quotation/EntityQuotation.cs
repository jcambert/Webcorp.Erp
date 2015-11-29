using ReactiveUI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using Webcorp.reactive;
using Webcorp.unite;

namespace Webcorp.Model.Quotation
{
    [Serializable]
    public class EntityQuotation : CustomReactiveObject
    {
        public EntityQuotation()
        {

        }
        public EntityQuotation(Quotation q)
        {
            ShouldDispose(this.Operations.Changed.Subscribe(x => Tarifs.ForEach(t => t.Update(this))));
            var pp = Observable.FromEventPattern<PropertyChangedEventArgs>(this, "PropertyChanged").ObserveOnDispatcher(System.Windows.Threading.DispatcherPriority.Normal);
            ShouldDispose(pp.Subscribe(_ =>
            {
                Tarifs.ForEach(t => t.Update(this));
            }));

            q.Entities.Add(this);
            Commentaire = q.Commentaire;
            TauxHorraireMethodes = q.TauxHorraireMethodes;
            TempsMethodes = q.TempsMethodes;
            FAD = q.FAD;
            Outillage = q.Outillage;
            Difficulte = q.Difficulte;
            Delai = q.Delai;
            Ts = q.Ts;
            /* var p = Observable.FromEventPattern<CollectionChangeEventArgs>(this.Operations, "CollectionChanged").ObserveOnDispatcher(System.Windows.Threading.DispatcherPriority.Normal);
             ShouldDispose( p.Subscribe(_ => {
                 Tarifs.ForEach(t => t.Update(this));
             }));*/
            
        }

        string _reference;
        public string Reference { get { return _reference; } set { this.RaiseAndSetIfChanged(ref _reference, value); } }

        string _plan;
        public string Plan { get { return _plan; } set { this.RaiseAndSetIfChanged(ref _plan, value); } }

        string _designation;
        public string Designation { get { return _designation; } set { this.RaiseAndSetIfChanged(ref _designation, value); } }

        string _indice;
        public string Indice { get { return _indice; } set { this.RaiseAndSetIfChanged(ref _indice, value); } }

        //public FormatTole FormatTole { get; set; }
        string _commentaire;
        public string Commentaire { get { return _commentaire; } set { this.RaiseAndSetIfChanged(ref _commentaire, value); } }

        TauxHorraire _thmethodes;
        public TauxHorraire TauxHorraireMethodes { get { return _thmethodes; } set { this.RaiseAndSetIfChanged(ref _thmethodes, value); } }

        Time _tpsMethodes;
        public Time TempsMethodes { get { return _tpsMethodes; } set { this.RaiseAndSetIfChanged(ref _tpsMethodes, value); } }

        Currency _fad;
        public Currency FAD { get { return _fad; } set { this.RaiseAndSetIfChanged(ref _fad, value); } }

        Currency _outillage;
        public Currency Outillage { get { return _outillage; } set { this.RaiseAndSetIfChanged(ref _outillage, value); } }

        Difficulte _difficulte;
        public Difficulte Difficulte { get { return _difficulte; }  set { this.RaiseAndSetIfChanged(ref _difficulte, value); } }

        Delai _delai;
        public Delai Delai { get { return _delai; } set { this.RaiseAndSetIfChanged(ref _delai, value); } }

        TraitementSurface _ts;
        public TraitementSurface Ts { get { return _ts; } set { this.RaiseAndSetIfChanged(ref _ts, value); } }

        public ReactiveCollection<Operation> Operations { get; set; } = new ReactiveCollection<Operation>();

        public ReactiveCollection<Tarif> Tarifs { get; set; } = new ReactiveCollection<Tarif>();

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
