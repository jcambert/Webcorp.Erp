using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Webcorp.Controller;
using Webcorp.erp.quotation.ViewModel;
using Webcorp.erp.quotation.ViewModel.impl;
using Webcorp.Model.Quotation;
using Webcorp.rx_mvvm;

namespace Webcorp.erp.quotation.DesignData
{
    public class DQuotationViewModel : NavigationViewModel<Quotation> , IQuotationViewModel
    {

        #region ctor
        public DQuotationViewModel():base()
        {
            for (int i = 1; i < 100; i++)
            {
                Models.Add(new Quotation() { Numero = i, Client = "Samoa_" + i, Commentaire = "DP N°" + i });
            }

            Model = Models[0];
            

        }

        public override Quotation CreateModel()
        {
            throw new NotImplementedException();
        }


        #endregion



    }
}
