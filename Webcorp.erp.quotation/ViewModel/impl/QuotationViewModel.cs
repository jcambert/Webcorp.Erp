using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Webcorp.rx_mvvm;
using Webcorp.Model.Quotation;
using System.Windows.Input;
using System.Reactive;
using Ninject;
using Prism.Logging;
using Prism.Events;
using Webcorp.erp.common;
using Prism.Regions;
using Webcorp.erp.quotation.Views;

namespace Webcorp.erp.quotation.ViewModel.impl
{
    public class QuotationViewModel : ViewModelBase<Quotation>, IQuotationViewModel
    {

        public QuotationViewModel()
        {
            Quotations = new List<Quotation>();
            Model = new Quotation() { Numero = 1234,Client="Souchier0",  Commentaire = "DP N° 1234" };
            Quotations.Add(Model);
            Model = new Quotation() { Numero = 4567, Client = "Souchier1", Commentaire = "DP N° 1234" };
            Quotations.Add(Model);
            Model = new Quotation() { Numero = 8910, Client = "Souchier2", Commentaire = "DP N° 1234" };
            Quotations.Add(Model);
            Model = new Quotation() { Numero = 111213, Client = "Souchier3", Commentaire = "DP N° 1234" };
            Quotations.Add(Model);
        }

        public List<Quotation> Quotations { get; set; }

        public override void Initialize()
        {
            base.Initialize();
            EventAggregator.GetEvent<PubSubEvent<QuotationMessage<QuotationViewModel>>>().Subscribe(OnPubSub);
            CanAdd = true;
            CanSave = true;

        }

        public override void OnSave()
        {
            base.OnSave();
            CanSave = false;
        }

        protected void OnPubSub(QuotationMessage<QuotationViewModel> msg)
        {
            Debug("Receive PubSub Message:" + msg);
            msg.Action(this);
        }

        public override void OnAdd()
        {
            base.OnAdd();
            IRegion client = RegionManager.Regions[Regions.Client];

           
            client.NavigationService.NavigationFailed += NavigationService_NavigationFailed;
           

            NavigateTo(Regions.Client, "QuotationDetailView");
                       
        }

        private void NavigationService_NavigationFailed(object sender, RegionNavigationFailedEventArgs e)
        {
            Exception(e.Error.ToString());
            throw new NotImplementedException();
        }

        
    }
}
