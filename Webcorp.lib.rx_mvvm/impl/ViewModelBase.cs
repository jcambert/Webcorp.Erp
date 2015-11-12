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
#if DEBUG
using System.Diagnostics;
#endif

namespace Webcorp.rx_mvvm
{


    public abstract class MenuViewModel
    {
        private readonly CompositeDisposable _disposables = new CompositeDisposable();
        private readonly Guid _serial = Guid.NewGuid();
        private readonly string _myName;


        private ICommandObserver<Unit> _addCommand;
        private ICommandObserver<Unit> _editCommand;
        private ICommandObserver<Unit> _saveCommand;
        private ICommandObserver<Unit> _deleteCommand;


        private IPropertySubject<bool> _canAdd;
        private IPropertySubject<bool> _canEdit;
        private IPropertySubject<bool> _canSave;
        private IPropertySubject<bool> _canDelete;



        public MenuViewModel()
        {
            _myName = GetType().Name;
        }


        #region Properties
        public string MyName => _myName;

        [Inject]
        public ILoggerFacade Logger { get; set; }
        [Inject]
        public IMessageBus MessageBus { get; set; }
        [Inject]
        public IDialogService DialogService { get; set; }
        [Inject]
        public IKernel Container { get; set; }

        public ICommand AddCommand => _addCommand;



        public ICommand DeleteCommand => _deleteCommand;

        public ICommand EditCommand => _editCommand;

        public ICommand SaveCommand => _saveCommand;

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


        #endregion

        #region Ninject
        public virtual void Initialize()
        {
            Debug("Start Initialize");
            Debug("creating properties");
            CreateProperties<MenuViewModel>();
            Debug("End Initialize");
        }
        #endregion

        #region protected function

        protected IPropertyProvider<W> Get<W>() => Container.Resolve<IPropertyProvider<W>>(this);

        protected virtual void CreateProperties<W>() where W : MenuViewModel
        {

            CreateProperty<W>(i => i.CanAdd, i => i.AddCommand, CanAdd, ref _canAdd, ref _addCommand, OnAdd);
            CreateProperty<W>(i => i.CanEdit, i => i.EditCommand, CanEdit, ref _canEdit, ref _editCommand, OnEdit);
            CreateProperty<W>(i => i.CanSave, i => i.SaveCommand, CanSave, ref _canSave, ref _saveCommand, OnSave);
            CreateProperty<W>(i => i.CanDelete, i => i.DeleteCommand, CanDelete, ref _canDelete, ref _deleteCommand, OnDelete);

        }

        protected virtual void CreateProperty<W>(Expression<Func<W, bool>> property, Expression<Func<W, ICommand>> cmd, bool canCmd, ref IPropertySubject<bool> _canSubject, ref ICommandObserver<Unit> _canCommand, Action action) where W : MenuViewModel
        {
            _canSubject = Get<W>().CreateProperty(property, canCmd);
            _canCommand = CreateCommand<W>(cmd, canCmd);
            ShouldDispose(_canSubject.Subscribe(_canCommand.SetCanExecute));
            ShouldDispose(_canSubject.Subscribe(_ =>
            {
                //Debugger.Break();
            }));
            ShouldDispose(_canCommand.OnExecute.Subscribe(_ => action()));
            //   _canSubject.Value = false;
        }

        protected ICommandObserver<Unit> CreateCommand<W>(Expression<Func<W, ICommand>> e, bool IsEnabled = true) where W : MenuViewModel
        {
            return Get<W>().CreateCommand<Unit>(e, IsEnabled);
        }
        #endregion

        #region public function

        public virtual void OnAdd()
        {
#if DEBUG
            if (Debugger.IsAttached) Debugger.Break();
#endif

        }

        public virtual void OnEdit()
        {
#if DEBUG
            if (Debugger.IsAttached) Debugger.Break();
#endif
        }

        public virtual void OnSave()
        {
#if DEBUG
            if (Debugger.IsAttached) Debugger.Break();
#endif
        }

        public virtual void OnDelete()
        {
#if DEBUG
            if (Debugger.IsAttached) Debugger.Break();
#endif

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
        public void Debug(string message)
        {
            Logger.Log(MyName + "-" + _serial.ToString() + "-" + message, Category.Debug, Priority.Low);
        }
        public void Info(string message)
        {
            Logger.Log(MyName + "-" + _serial.ToString() + "-" + message, Category.Info, Priority.Low);
        }
        public void Warn(string message)
        {
            Logger.Log(MyName + "-" + _serial.ToString() + "-" + message, Category.Warn, Priority.Medium);
        }
        public void Exception(string message)
        {
            Logger.Log(MyName + "-" + _serial.ToString() + "-" + message, Category.Exception, Priority.High);
        }
        #endregion

    }
    public abstract class ViewModelBase<T> : MenuViewModel, IEntityViewModel<T>, INavigationAware where T : IEntity
    {

        public event PropertyChangedEventHandler PropertyChanged;
        private readonly ISubject<bool> _closeSubject = new Subject<bool>();

        private ICommandObserver<Unit> _closeCommand;
        private IPropertySubject<bool> _canClose;

        #region ctor
        public ViewModelBase() : base()
        {

#if DEBUG
            PropertyChanged += ViewModelBase_PropertyChanged;
#endif
        }
#if DEBUG
        private void ViewModelBase_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            Debug(e.PropertyName + "has changed");
        }
#endif

        public ViewModelBase(T entity) : this()
        {
            Model = entity;
        }
        #endregion

        #region properties


        public T Model { get; set; }

        public ICommand CloseCommand => _closeCommand;

        [Inject]
        public IEntityController<T> Controller
        {
            get;

            set;
        }


        public bool CanClose
        {
            get { return _canClose?.Value ?? false; }
            set { _canClose.Value = value; }
        }

        #endregion

        protected override void CreateProperties<W>()
        {
            base.CreateProperties<W>();
            CreateProperty<ViewModelBase<T>>(i => i.CanClose, i => i.CloseCommand, CanClose, ref _canClose, ref _closeCommand, CloseView);
        }


        #region ICloseable
        public IObservable<bool> Close => _closeSubject.AsObservable();

        #endregion

        public IDoFluidCommand<T> For(ICommandObserver<T> cmd)
        {
            var fluidCommand = new FluidCommand<T>(cmd);
            ShouldDispose(fluidCommand);

            return fluidCommand;
        }

        #region Ninject
        #endregion

        #region action command executing


        /// <summary>
        /// Close the view
        /// </summary>
        protected virtual void CloseView()
        {
#if DEBUG
            if (Debugger.IsAttached) Debugger.Break();
#endif
        }

        


        #endregion

        public void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }


        #region INavigationAware

        public virtual void OnNavigatedTo(NavigationContext navigationContext)
        {

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


        #region   IRegionMemberLifetime
        public virtual bool KeepAlive => true;
        #endregion
    }
}
