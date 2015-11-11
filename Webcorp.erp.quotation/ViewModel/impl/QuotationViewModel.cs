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

namespace Webcorp.erp.quotation.ViewModel.impl
{
    public class QuotationViewModel : ViewModelBase<Quotation>, IQuotationViewModel
    {
       
        public QuotationViewModel()
        {
            Model = new Quotation() { Numero = 1234, Commentaire = "test" };
        }
       

        public override void Initialize()
        {
            base.Initialize();
            //CanSave = true;
            CanSave = false;

        }



        
    }
}
