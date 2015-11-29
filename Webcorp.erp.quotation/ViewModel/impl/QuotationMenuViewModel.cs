using Webcorp.Model.Quotation;
using Webcorp.reactive;

namespace Webcorp.erp.quotation.ViewModel.impl
{
    public class QuotationMenuViewModel:ReactiveViewModel<Quotation>
    {
        public override void Initialize()
        {
            base.Initialize();
            CanAdd = true;
            CanSave = true;
        }
        
        public override void OnSave(object arg)
        {
            base.OnSave(arg);
            CanSave = false;
        } 
    } 
}
