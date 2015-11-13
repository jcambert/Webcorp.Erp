using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Webcorp.rx_mvvm;

namespace Webcorp.erp.quotation.ViewModel.impl
{
    public class QuotationMenuViewModel:ViewModelBase
    {
        public override void Initialize()
        {
            base.Initialize();
            CanSave = true;
        }
        public override void OnSave()
        {
            base.OnSave();
            CanSave = false;
        }
    }
}
