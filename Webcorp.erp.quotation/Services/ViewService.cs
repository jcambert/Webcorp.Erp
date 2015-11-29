using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Webcorp.erp.quotation.ViewModel;
using Webcorp.erp.quotation.ViewModel.impl;

namespace Webcorp.erp.quotation.Services
{
    public class ViewService
    {
        [Inject]
        public IKernel Container { get; set; }
        
       // public QuotationViewModel QuotationViewModel => (QuotationViewModel)Container.Get<IQuotationViewModel>();
    }
}
