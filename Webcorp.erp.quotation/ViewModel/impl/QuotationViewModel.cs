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
using Webcorp.Model;
using System.Collections.ObjectModel;
using System.Reactive.Subjects;
using System.Reactive.Linq;

namespace Webcorp.erp.quotation.ViewModel.impl
{
    public class NavigationViewModel<T> : ViewModelBase<T> where T : IEntity
    {

        private ICommandObserver<Unit> _readCommand;
        private IPropertySubject<bool> _canRead;

        private ICommandObserver<Unit> _listQuotationCommand;
        private IPropertySubject<bool> _canList;

        private ICommandObserver<Unit> _cancelCommand;
        private IPropertySubject<bool> _canCancel;

        private IPropertySubject<bool> _isReadOnly;
        private IPropertySubject<Status> _propStatus;

        ObservableCollection<T> Models { get; set; }

        public T LastModel { get; set; }

        public int SelectedIndex { get; set; }

        public Status Status { get; set; }

        public bool IsReadonly { get { return _isReadOnly.Value; } set { _isReadOnly.Value = value; OnPropertyChanged(); } }

        public bool CanRead { get { return _canRead?.Value ?? false; } set { _canRead.Value = value; } }

        public bool CanList { get { return _canList?.Value ?? false; } set { _canList.Value = value; } }

        public bool CanCancel { get { return _canCancel?.Value ?? false; } set { _canCancel.Value = value; } }

        public ICommand ReadCommand => _readCommand;

        public ICommand ListQuotationCommand => _listQuotationCommand;

        public ICommand CancelCommand => _cancelCommand;

        public override bool KeepAlive => true;

        public override void Initialize()
        {
            base.Initialize();

            _propStatus = new PropertySubject<Status>();
           ShouldDispose(  _propStatus.Subscribe(p => _onStatusChanged(p)));

            EventAggregator.GetEvent<PubSubEvent<QuotationMessage<QuotationViewModel>>>().Subscribe(OnMessage);

            Status = Status.Liste;


        }

        public override void OnAdd()
        {
            base.OnAdd();
            Status = Status.Creation;


        }

        public override void OnEdit()
        {
            base.OnEdit();
            Status = Status.Edition;
        }
        public void OnRead()
        {

            Status = Status.Lecture;
        }

        public void OnListQuotation()
        {
            Status = Status.Liste;
        }

        public void OnCancel()
        {
            if (Status == Status.Creation)
            {

                Models.Remove(Model);
                Reset();
                Status = Status.Liste;
            }
            else if (Status == Status.Edition)
            {
                Models.Remove(Model);
                Reset();
                Models.Insert(SelectedIndex, Model);
                Status = Status.Lecture;
            }
        }

        public override void OnSave()
        {
            base.OnSave();
            CanSave = false;
            Status = Status.Lecture;

        }


        #region Protected
        protected virtual void OnMessage(QuotationMessage<QuotationViewModel> msg)
        {
            Debug("Receive PubSub Message:" + msg);
            msg.Action(this);
        }

        protected virtual void OnStatusChanged(Status p)
        {
            switch (p)
            {
                case Status.Aucun:
                    CanSave = false;
                    CanAdd = true;
                    CanRead = true;
                    IsReadonly = true;
                    CanList = true;
                    CanCancel = false;
                    break;
                case Status.Creation:
                    LastModel = Model.Clone();
                    Model = Container.Resolve<T>();
                    Models.Add(Model);
                    NavigateTo(QuotationRegions.Main, "QuotationFormView");
                    CanSave = true;
                    IsReadonly = false;
                    CanCancel = true;
                    break;
                case Status.Edition:
                    LastModel = Model.Clone();
                    CanSave = true;
                    CanCancel = true;
                    CanEdit = false;
                    IsReadonly = false;
                    CanList = false;
                    break;
                case Status.Lecture:
                    NavigateTo(QuotationRegions.Main, "QuotationFormView");
                    CanSave = false;
                    CanList = true;
                    CanEdit = true;
                    IsReadonly = true;
                    CanCancel = false;
                    break;
                case Status.Liste:
                    NavigateTo(QuotationRegions.Main, "QuotationSummaryView");
                    CanAdd = true;
                    CanRead = true;
                    IsReadonly = true;
                    CanEdit = true;
                    break;
                default:
                    break;
            }
        }


        protected override void CreateProperties<W>()
        {
            base.CreateProperties<W>();
            CreateProperty<QuotationViewModel>(i => i.CanRead, CanRead, ref _canRead, ref _readCommand, OnRead);
            CreateProperty<QuotationViewModel>(i => i.CanList, CanList, ref _canList, ref _listQuotationCommand, OnListQuotation);
            CreateProperty<QuotationViewModel>(i => i.CanCancel, CanCancel, ref _canCancel, ref _cancelCommand, OnCancel);

            _isReadOnly = Get<QuotationViewModel>().CreateProperty<bool>(i => i.IsReadonly, !CanSave);
        }

        protected override void Reset()
        {

            Model = LastModel;

        }

        #endregion
        #region Private 
        private void _onStatusChanged(Status p)
        {
            OnStatusChanged(p);
        }
        
        #endregion


    }
    public class QuotationViewModel : ViewModelBase<Quotation>, IQuotationViewModel
    {
        private ICommandObserver<Unit> _readCommand;
        private IPropertySubject<bool> _canRead;

        private ICommandObserver<Unit> _listQuotationCommand;
        private IPropertySubject<bool> _canList;

        private ICommandObserver<Unit> _cancelCommand;
        private IPropertySubject<bool> _canCancel;

        private IPropertySubject<bool> _isReadOnly;
        private IPropertySubject<Status> _propStatus;

        public QuotationViewModel()
        {
            Quotations = new ObservableCollection<Quotation>();
            Model = new Quotation() { Numero = 1234, Client = "Souchier0", Commentaire = "DP N° 1234" };
            Quotations.Add(Model);
            Model = new Quotation() { Numero = 4567, Client = "Souchier1", Commentaire = "DP N° 1234" };
            Quotations.Add(Model);
            Model = new Quotation() { Numero = 8910, Client = "Souchier2", Commentaire = "DP N° 1234" };
            Quotations.Add(Model);
            Model = new Quotation() { Numero = 111213, Client = "Souchier3", Commentaire = "DP N° 1234" };
            Quotations.Add(Model);

        }

        public Quotation LastSelected { get; set; }

        public int Index { get; set; }

        public ObservableCollection<Quotation> Quotations { get; set; }

        public Status Status { get { return _propStatus.Value; } set { _propStatus.Value = value; } }

        public ICommand ReadCommand => _readCommand;

        public ICommand ListQuotationCommand => _listQuotationCommand;

        public ICommand CancelCommand => _cancelCommand;

        public bool IsReadonly
        {
            get { return _isReadOnly.Value; }
            set { _isReadOnly.Value = value; OnPropertyChanged(); }
        }

        public bool CanRead { get { return _canRead?.Value ?? false; } set { _canRead.Value = value; } }

        public bool CanList { get { return _canList?.Value ?? false; } set { _canList.Value = value; } }

        public bool CanCancel { get { return _canCancel?.Value ?? false; } set { _canCancel.Value = value; } }

        public override void Initialize()
        {
            base.Initialize();

            _propStatus = new PropertySubject<Status>();
            ShouldDispose( _propStatus.Subscribe(p => _onStatusChanged(p)));

            EventAggregator.GetEvent<PubSubEvent<QuotationMessage<QuotationViewModel>>>().Subscribe(OnPubSub);
            /*    _canSave.Subscribe(_ =>
                     IsReadonly = !CanSave
                );
                */
            Status = Status.Liste;


        }
        private void _onStatusChanged(Status p)
        {
            OnStatusChanged(p);
        }
        protected virtual void OnStatusChanged(Status p)
        {
            switch (p)
            {
                case Status.Aucun:
                    CanSave = false;
                    CanAdd = true;
                    CanRead = true;
                    IsReadonly = true;
                    CanList = true;
                    CanCancel = false;
                    break;
                case Status.Creation:
                    LastSelected = Model.Clone();
                    Model = new Quotation();
                    Quotations.Add(Model);
                    NavigateTo(QuotationRegions.Main, "QuotationFormView");
                    CanSave = true;
                    IsReadonly = false;
                    CanCancel = true;
                    break;
                case Status.Edition:
                    LastSelected = Model.Clone();
                    CanSave = true;
                    CanCancel = true;
                    CanEdit = false;
                    IsReadonly = false;
                    CanList = false;
                    break;
                case Status.Lecture:
                    NavigateTo(QuotationRegions.Main, "QuotationFormView");
                    CanSave = false;
                    CanList = true;
                    CanEdit = true;
                    IsReadonly = true;
                    CanCancel = false;
                    break;
                case Status.Liste:
                    NavigateTo(QuotationRegions.Main, "QuotationSummaryView");
                    CanAdd = true;
                    CanRead = true;
                    IsReadonly = true;
                    CanEdit = true;
                    break;
                default:
                    break;
            }
        }

        protected override void CreateProperties<W>()
        {
            base.CreateProperties<W>();
            CreateProperty<QuotationViewModel>(i => i.CanRead, CanRead, ref _canRead, ref _readCommand, OnRead);
            CreateProperty<QuotationViewModel>(i => i.CanList, CanList, ref _canList, ref _listQuotationCommand, OnListQuotation);
            CreateProperty<QuotationViewModel>(i => i.CanCancel, CanCancel, ref _canCancel, ref _cancelCommand, OnCancel);

            _isReadOnly = Get<QuotationViewModel>().CreateProperty<bool>(i => i.IsReadonly, !CanSave);
        }

        protected override void Reset()
        {

            Model = LastSelected;

        }

        public override void OnEdit()
        {
            base.OnEdit();
            Status = Status.Edition;
        }
        public void OnRead()
        {

            Status = Status.Lecture;
        }

        public void OnListQuotation()
        {
            Status = Status.Liste;
        }

        public void OnCancel()
        {
            if (Status == Status.Creation)
            {

                Quotations.Remove(Model);
                Reset();
                Status = Status.Liste;
            }
            else if (Status == Status.Edition)
            {
                Quotations.Remove(Model);
                Reset();
                Quotations.Insert(Index, Model);
                Status = Status.Lecture;
            }
        }

        public override void OnSave()
        {
            base.OnSave();
            CanSave = false;
            Status = Status.Lecture;

        }

        protected void OnPubSub(QuotationMessage<QuotationViewModel> msg)
        {
            Debug("Receive PubSub Message:" + msg);
            msg.Action(this);
        }

        public override void OnAdd()
        {
            base.OnAdd();
            Status = Status.Creation;


        }



        public override bool KeepAlive => true;



    }
}
