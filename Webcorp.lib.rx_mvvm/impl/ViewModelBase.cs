using Ninject;
using Prism.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Subjects;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Webcorp.Controller;
using Webcorp.Model;
using System.Linq.Expressions;
using Prism.Regions;
using System.Runtime.CompilerServices;
using Prism.Events;
#if DEBUG
using System.Diagnostics;
#endif

namespace Webcorp.rx_mvvm
{


    public class ViewModelBase : /*CustomReactiveObject,*/ IViewModel, ILoggable
    {
        private readonly CompositeDisposable _disposables = new CompositeDisposable();
        private readonly Guid _serial = Guid.NewGuid();
        private readonly string _myName;


        private ICommandObserver<Unit> _addCommand;
        private ICommandObserver<Unit> _editCommand;
        private ICommandObserver<Unit> _saveCommand;
        private ICommandObserver<Unit> _deleteCommand;
        private ICommandObserver<Unit> _closeCommand;

        private IPropertySubject<bool> _canAdd;
        private IPropertySubject<bool> _canEdit;
        protected IPropertySubject<bool> _canSave;
        private IPropertySubject<bool> _canDelete;
        private IPropertySubject<bool> _canClose;

        private readonly ISubject<bool> _closeSubject = new Subject<bool>();




        //public event PropertyChangedEventHandler PropertyChanged;

        public ViewModelBase()
        {
            _myName = GetType().Name;
#if DEBUG
            PropertyChanged += ViewModelBase_PropertyChanged;
#endif
        }
#if DEBUG
        private void ViewModelBase_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            Debug(e.PropertyName + " has changed");
        }
#endif

        public ViewModelBase(object model) : base()
        {
            this.Model = model;
        }


        protected void NavigateTo(string region, string uri)
        {

            RegionManager.Regions[region].RequestNavigate(new Uri(uri, UriKind.Relative));
        }

        #region Properties
        public string MyName => _myName;

        [Inject]
        public ILogger Logger { get; set; }
        [Inject]
        public IMessageBus MessageBus { get; set; }
        [Inject]
        public IDialogService DialogService { get; set; }
        [Inject]
        public IEventAggregator EventAggregator { get; set; }
        [Inject]
        public IRegionManager RegionManager { get; set; }

        [Inject]
        public IKernel Container { get; set; }



        public ICommand AddCommand => _addCommand;

        public ICommand DeleteCommand => _deleteCommand;

        public ICommand EditCommand => _editCommand;

        public ICommand SaveCommand => _saveCommand;

        public virtual ICommand CloseCommand => _closeCommand;

        public virtual bool CanSave
        {
            get { return _canSave?.Value ?? false; }
            set { _canSave.Value = value; }
        }

        public bool CanAdd
        {
            get { return _canAdd?.Value ?? false; }
            set { _canAdd.Value = value; }
        }

        public bool CanEdit
        {
            get { return _canEdit?.Value ?? false; }
            set { _canEdit.Value = value; }
        }

        public bool CanDelete
        {
            get { return _canDelete?.Value ?? false; }
            set { _canDelete.Value = value; }
        }

        public bool CanClose
        {
            get { return _canClose?.Value ?? false; }
            set { _canClose.Value = value; }
        }

        public object Model
        {
            get;

            set;
        }



        public IObservable<bool> Close => _closeSubject.AsObservable();


        public virtual bool KeepAlive => false;



        #endregion

        #region Ninject
        public virtual void Initialize()
        {
            Debug("Start Initialize");
            Debug("creating properties");
            CreateProperties<ViewModelBase>();
            Debug("End Initialize");
        }
        #endregion

        #region protected function

        protected IPropertyProvider<W> Get<W>() where W : IViewModel => Container.Resolve<IPropertyProvider<W>>(this);

        protected virtual void CreateProperties<W>() where W : ViewModelBase
        {

            CreateProperty<W>(i => i.CanAdd, CanAdd, ref _canAdd, ref _addCommand, OnAdd);
            CreateProperty<W>(i => i.CanEdit, CanEdit, ref _canEdit, ref _editCommand, OnEdit);
            CreateProperty<W>(i => i.CanSave, CanSave, ref _canSave, ref _saveCommand, OnSave);
            CreateProperty<W>(i => i.CanDelete, CanDelete, ref _canDelete, ref _deleteCommand, OnDelete);
            CreateProperty<W>(i => i.CanClose, CanClose, ref _canClose, ref _closeCommand, CloseView);

        }

        protected virtual void CreateProperty<W>(Expression<Func<W, bool>> property, bool canCmd, ref IPropertySubject<bool> _canSubject, ref ICommandObserver<Unit> _canCommand, Action action) where W : ViewModelBase
        {
            _canSubject = Get<W>().CreateProperty(property, canCmd);
            _canCommand = CreateCommand<W>(canCmd);
            ShouldDispose(_canSubject.Subscribe(_canCommand.SetCanExecute));
            ShouldDispose(_canSubject.Subscribe(_ =>
            {
                //Debugger.Break();
            }));
            ShouldDispose(_canCommand.OnExecute.Subscribe(_ => action()));
            //   _canSubject.Value = false;
        }

        protected ICommandObserver<Unit> CreateCommand<W>(bool IsEnabled = true) where W : ViewModelBase
        {
            return Get<W>().CreateCommand<Unit>(IsEnabled);
        }

        protected virtual void Reset()
        {

        }
        #endregion

        #region public function

        public virtual void OnAdd()
        {
            Debug();

        }

        public virtual void OnEdit()
        {
            Debug();
        }

        public virtual void OnSave()
        {
            Debug();

        }

        public virtual void OnDelete()
        {
            Debug();

        }

        public virtual void CloseView()
        {
            Debug();
        }


        public void ShouldDispose(IDisposable disposable)
        {
            _disposables.Add(disposable);
        }

        public void Dispose()
        {
            _disposables.Dispose();
        }

        #endregion

        #region Logger
        public void Debug(string message, [CallerMemberName] string caller = "")
        {
            Logger?.Debug(message, caller);
        }
        public void Debug([CallerMemberName] string message = "")
        {
            Logger?.Debug(message);
        }
        public void Info(string message, [CallerMemberName] string caller = "")
        {
            Logger?.Info(message, caller);
        }
        public void Info([CallerMemberName] string message = "")
        {
            Logger?.Info(message);
        }
        public void Warn(string message, [CallerMemberName] string caller = "")
        {
            Logger?.Warn(message, caller);
        }
        public void Warn([CallerMemberName] string message = "")
        {
            Logger?.Warn(message);
        }
        public void Exception(string message, [CallerMemberName] string caller = "")
        {
            Logger?.Exception(message, caller);
        }
        public void Exception([CallerMemberName] string message = "")
        {
            Logger?.Exception(message);
        }


        #endregion
        #region PropertyChanged
        public event PropertyChangedEventHandler PropertyChanged = delegate { };
        public virtual void OnPropertyChanged([CallerMemberName]  string propertyName = "")
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

    }
    public class ViewModelBase<T> : ViewModelBase, IEntityViewModel<T>, INavigationAware where T : IEntity
    {



        #region ctor
        public ViewModelBase() : base()
        {
        }


        public ViewModelBase(T entity) : base(entity)
        {
            Model = entity;
        }
        #endregion

        #region properties


        public virtual new T Model
        {
            get { return (T)base.Model; }
            set
            {
                // T bm =(T) base.Model;

                base.Model = value;
                OnPropertyChanged();

            }
        }


        [Inject]
        public IEntityController<T> Controller
        {
            get;

            set;
        }




        #endregion


        public IDoFluidCommand<T> For(ICommandObserver<T> cmd)
        {
            var fluidCommand = new FluidCommand<T>(cmd);
            ShouldDispose(fluidCommand);

            return fluidCommand;
        }






        #region INavigationAware

        public virtual void OnNavigatedTo(NavigationContext navigationContext)
        {
            Debug();
        }

        public virtual bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public virtual void OnNavigatedFrom(NavigationContext navigationContext)
        {
            Debug("OnNavigatedFrom");
        }
        #endregion

    }
}
