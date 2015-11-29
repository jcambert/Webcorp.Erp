using Webcorp.Model.Quotation;
using Webcorp.reactive;

namespace Webcorp.erp.quotation.DesignData
{
    public class DQuotationViewModel : ReactiveViewModel<Quotation> //, IQuotationViewModel
    {

        #region ctor
        public DQuotationViewModel():base()
        {
            for (int i = 1; i < 100; i++)
            {
                Add(new Quotation() { Numero = i, Client = "Samoa_" + i, Commentaire = "DP N°" + i });
            }

            GoFirst.Execute(null);
            

        }

       


        #endregion



    }
}
