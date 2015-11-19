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
using Webcorp.Model.Quotation;
using Webcorp.rx_mvvm;

namespace Webcorp.erp.quotation.DesignData
{
    public class DQuotationViewModel : IQuotationViewModel
    {

        #region ctor
        public DQuotationViewModel()
        {
            Quotations = new List<Quotation>();
            Model = new Quotation() { Numero = 1234,Client="Samoa", Commentaire="DP N° 1234" };

            Quotations.Add(Model);

        }

        
        #endregion

        public Quotation Model { get; set; }
        public int Numero { get;  set; }

         
        public List<Quotation> Quotations { get; set; }

        public ICommand SaveCommand
        {
            get
            {
                throw new NotImplementedException();
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public void Initialize()
        {
            throw new NotImplementedException();
        }

        public void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            throw new NotImplementedException();
        }

        public void ShouldDispose(IDisposable disposable)
        {
            throw new NotImplementedException();
        }

        #region IQuotationViewModel
        public ICommand AddCommand
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public bool CanAdd
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public bool CanClose
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public bool CanDelete
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public bool CanEdit
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public bool CanSave
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public IObservable<bool> Close
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public ICommand CloseCommand
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public IEntityController<Quotation> Controller
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public ICommand DeleteCommand
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public ICommand EditCommand
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public bool KeepAlive => false;
        #endregion
    }
}
