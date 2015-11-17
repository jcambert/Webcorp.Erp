using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Webcorp.Model.Quotation;
using Webcorp.rx_mvvm;

namespace Webcorp.erp.quotation.ViewModel.impl
{
    public class QuotationMenuViewModel:ViewModelBase
    {
        public override void Initialize()
        {
            base.Initialize();
            CanAdd = true;
            CanSave = true;
        }
        public override void OnSave()
        {
            base.OnSave();
            CanSave = false;
        }

        public override void OnAdd()
        {
            base.OnAdd();
            EventAggregator.GetEvent<PubSubEvent<QuotationMessage<QuotationViewModel>>>().Publish(QuotationMessage<QuotationViewModel>.ADD);
        }
    }

    public class QuotationMessage <T>{

        public const string C_ADD = "add";
        public static QuotationMessage<QuotationViewModel> ADD = new QuotationMessage<QuotationViewModel>(QuotationMessage<QuotationViewModel>.C_ADD,i=>i.OnAdd());
        private QuotationMessage(string message,Expression<Action<T>> expr)
        {
            Message = message;
            Action = expr.Compile();
        }

        public Action<T> Action { get; private set; }

        public string Message { get; private set; }
        /*public void t(Expression<Action<T>> expr)
        {
            var propertyName = ((MemberExpression)expr.Body).Member.Name;
            Action<T> a= expr.Compile();
            
        }*/


    }
}
