using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Webcorp.Model.Quotation;
using Webcorp.rx_mvvm;

namespace Webcorp.erp.quotation.ViewModel
{
    public interface IQuotationViewModel:IEntityViewModel<Quotation>
    {
        ICommand OpenQuotationCommand { get;  }
    }
}
