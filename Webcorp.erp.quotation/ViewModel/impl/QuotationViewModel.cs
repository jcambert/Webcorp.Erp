using Webcorp.Model.Quotation;
using ReactiveUI;
using Webcorp.reactive;
using Webcorp.Model;
using Ninject;
using Webcorp.unite;

namespace Webcorp.erp.quotation.ViewModel.impl
{




    public class QuotationReactiveViewModel : ReactiveViewModel<Quotation>
    {

        public QuotationReactiveViewModel()
        {

        }

        public override void Initialize()
        {
            base.Initialize();

            for (int i = 1; i <= 10; i++)
            {
                var q = new Quotation() { Numero = i, Client = "Samoa_" + i, Commentaire = "DP N°" + i };
                for (int j = 1; j <= 10; j++)
                {
                    var e = new EntityQuotation() { Reference = "ref" + i + "_" + j, Designation = "desi" + i + "_" + j };
                    q.Entities.Add(e);
                }
                Add(q);
            }


            AddEntityCommand = CreateCommand(this.WhenAnyValue(x => x.CanAddEntity), this.OnAddEntity);

            CanAdd = true;

            CanAddEntity = true;

            
        }

        public override void OnAdd(object arg)
        {
            base.OnAdd(arg);
            NavigateTo(QuotationRegions.Main, "QuotationFormView");
        }
        public override void OnRead(object arg)
        {
            base.OnRead(arg);
            NavigateTo(QuotationRegions.Main, "QuotationFormView");
        }

        public override void OnViewList(object arg)
        {
            base.OnViewList(arg);
            NavigateTo(QuotationRegions.Main, "QuotationSummaryView");
        }

        public override void OnCancel(object arg)
        {
            base.OnCancel(arg);
            if (Status == ViewModelStatus<Quotation>.Creation)
            {
                NavigateTo(QuotationRegions.Main, "QuotationSummaryView");
            }
            else if (Status == ViewModelStatus<Quotation>.Edition)
            {
                NavigateTo(QuotationRegions.Main, "QuotationFormView");
            }
        }

        bool _canAddEntity;
        public bool CanAddEntity { get { return _canAddEntity; } set { this.RaiseAndSetIfChanged(ref _canAddEntity, value); } }
        public ReactiveCommand<object> AddEntityCommand { get; protected set; }
        public void OnAddEntity(object arg)
        {
            var pcp = Container.Get<IEntityProvider<PosteCharge, int>>();
            var vdlp = Container.Get<IEntityProvider<VitesseDecoupeLaser, string>>();
            var mpp = Container.Get<IEntityProvider<MaterialPrice, string>>();
            var mp = Container.Get<IEntityProvider<Material, string>>();

            var e = new EntityQuotation(Model) { Reference = "reftest", Designation = "refdesi", };
            e.Outillage = 50 * Currency.Euro;
            e.FAD = 45.0555 * Currency.Euro;
            var op1 = new Operation(e) { Poste = pcp.Find(300), Nombre = 2 };
            var op2 = new Operation(e) { Poste = pcp.Find(101), Nombre = 5 };
            var op3 = new Operation(e) { Poste = pcp.Find(215), Nombre = 1 };
            var decoupe = new OperationLaser(op3) { Epaisseur = 1, Gaz = GazDecoupe.Oxygene, Longueur = 600 * Length.Millimetre, NombreAmorcage = 3, NombrePetitDiametre = 2, SqueletteX = 20 * Length.Millimetre, SqueletteY = 20 * Length.Millimetre, Pince = 0 * Length.Millimetre };
            decoupe.FormatPiece = new Format() { Longueur = 200 * Length.Millimetre, Largeur = 100 * Length.Millimetre, Epaisseur = 1 * Length.Millimetre, Matiere = mp.Find("1.0035") };
            decoupe.FormatTole = new Format() { Longueur = 222 * Length.Millimetre, Largeur = 125 * Length.Millimetre, Epaisseur = 1 * Length.Millimetre, Matiere = mp.Find("1.0035"), PrixMatiere = mpp.Find("1.0035") };

            decoupe.Laser = vdlp.Find(215, MaterialGroup.P, 1, GazDecoupe.Oxygene);

            new Tarif(e) { Quantite = 1, CoeficientVente = 0.4 };
            new Tarif(e) { Quantite = 5, CoeficientVente = 0.35 };
            new Tarif(e) { Quantite = 10, CoeficientVente = 0.30 };
            new Tarif(e) { Quantite = 20, CoeficientVente = 0.20 };
            new Tarif(e) { Quantite = 50, CoeficientVente = 0.20 };
            new Tarif(e) { Quantite = 100, CoeficientVente = 0.20 };

        }

        EntityQuotation _currentEntity;
        public EntityQuotation CurrentEntity { get { return _currentEntity; } set { this.RaiseAndSetIfChanged(ref _currentEntity, value); } }
    }
}

