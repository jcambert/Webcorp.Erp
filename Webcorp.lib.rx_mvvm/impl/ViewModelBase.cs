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
    public abstract class ViewModelBase<T> : IEntityViewModel<T>, INavigationAware where T : IEntity
    {
        private readonly CompositeDisposable _disposables = new CompositeDisposable();
        public event PropertyChangedEventHandler PropertyChanged;

        private readonly string _myName;
        //private IPropertyProvider<ViewModelBase<T>> _propertyProvider;
        private readonly ISubject<bool> _closeSubject = new Subject<bool>();
        private ICommandObserver<Unit> _addCommand;
        private ICommandObserver<Unit> _editCommand;
        private ICommandObserver<Unit> _saveCommand;
        private ICommandObserver<Unit> _deleteCommand;
        private ICommandObserver<Unit> _closeCommand;

        private IPropertySubject<bool> _canAdd;
        private IPropertySubject<bool> _canEdit;
        private IPropertySubject<bool> _canSave;
        private IPropertySubject<bool> _canDelete;
        private IPropertySubject<bool> _canClose;

        IObservable<bool> __canSave;

        private readonly Guid _serial = Guid.NewGuid();

        #region ctor
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
            Debug(e.PropertyName + "has changed");
        }
#endif

        public ViewModelBase(T entity) : this()
        {
            Model = entity;
        }
        #endregion

        #region properties
        [Inject]
        public ILoggerFacade Logger { get; set; }
        [Inject]
        public IMessageBus MessageBus { get; set; }
        [Inject]
        public IDialogService DialogService { get; set; }
        [Inject]
        public IKernel Container { get; set; }

        public T Model { get; set; }

        [Inject]
        public IEntityController<T> Controller
        {
            get;

            set;
        }

        private object prop_provider;
        protected IPropertyProvider<W, T> Get<W>() where W : IEntityViewModel<T> => Container.Resolve<IPropertyProvider<W, T>>(this);
        /* {
             prop_provider= prop_provider.IfIsNull(() => Container.Resolve<IPropertyProvider<W, T>>(this));
             return (IPropertyProvider<W, T>)prop_provider;
         }*/
        #endregion


        #region ICloseable
        public IObservable<bool> Close => _closeSubject.AsObservable();


        #endregion

        #region IStandardCommand
        public ICommand AddCommand => _addCommand;

        public ICommand CloseCommand => _closeCommand;

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

        public bool CanClose
        {
            get { return _canClose?.Value ?? false; }
            set { _canClose.Value = value; }
        }
        #endregion

        #region Ninject
        public virtual void Initialize()
        {
            /*Debug("Start Initialize");
            Debug("creating properties");
            CreateProperties<ViewModelBase<T>>();
            Debug("creating commands");
            CreateCommands();
            Debug("observing commands");
            ObservingCommands();
            Debug("End Initialize");*/

            Link<ViewModelBase<T>>(i => i.CanAdd, i => i.AddCommand, CanAdd, ref _canAdd, ref _addCommand, AddModel);
            Link<ViewModelBase<T>>(i => i.CanEdit, i => i.EditCommand, CanEdit, ref _canEdit, ref _editCommand, EditModel);
            Link<ViewModelBase<T>>(i => i.CanSave, i => i.SaveCommand, CanSave, ref _canSave, ref _saveCommand, SaveModel);
            Link<ViewModelBase<T>>(i => i.CanDelete, i => i.DeleteCommand, CanDelete, ref _canDelete, ref _deleteCommand, DeleteModel);
            Link<ViewModelBase<T>>(i => i.CanClose, i => i.CloseCommand, CanClose, ref _canClose, ref _closeCommand, CloseView);


            /*  _canSave = Get<ViewModelBase<T>>().CreateProperty(i => i.CanSave, CanSave);
              _saveCommand = CreateCommand<ViewModelBase<T>>(i => i.SaveCommand, CanSave);
              ShouldDispose(_canSave.Subscribe(
                  _saveCommand.SetCanExecute
              ));
              ShouldDispose(_saveCommand.OnExecute.Subscribe(_ => SaveModel()));*/

        }

        private void Link<W>(Expression<Func<W, bool>> property, Expression<Func<W, ICommand>> cmd, bool canCmd, ref IPropertySubject<bool> _canSubject, ref ICommandObserver<Unit> _canCommand, Action action) where W : IEntityViewModel<T>
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

        private IDeleteViewModelMessage<T> createDeleteViewModelMessage()
        {
            var result = Container.Get<IDeleteViewModelMessage<T>>();
            result.Model = this.Model;
            return result;
        }

        protected virtual void CreateProperties<W>() where W : IEntityViewModel<T>
        {

            _canAdd = Get<W>().CreateProperty(i => i.CanAdd, CanAdd);
            _canEdit = Get<W>().CreateProperty(i => i.CanEdit, CanEdit);
            _canSave = Get<W>().CreateProperty(i => i.CanSave, CanSave);
            _canDelete = Get<W>().CreateProperty(i => i.CanDelete, CanDelete);
            _canClose = Get<W>().CreateProperty(i => i.CanClose, CanClose);
        }

        protected virtual void CreateCommands()
        {

            // creating base commands
            _addCommand = CreateCommand<ViewModelBase<T>>(i => i.AddCommand);
            _editCommand = CreateCommand<ViewModelBase<T>>(i => i.EditCommand);
            _saveCommand = CreateCommand<ViewModelBase<T>>(i => i.SaveCommand);
            _deleteCommand = CreateCommand<ViewModelBase<T>>(i => i.DeleteCommand);
            _closeCommand = CreateCommand<ViewModelBase<T>>(i => i.CloseCommand);
        }

        protected ICommandObserver<Unit> CreateCommand<W>(Expression<Func<W, ICommand>> e, bool IsEnabled = true) where W : IEntityViewModel<T>
        {
            return Get<W>().CreateCommand<Unit>(e, IsEnabled);
        }



        protected virtual void ObservingCommands()
        {
            // Observing commands
            ShouldDispose(_canAdd.Subscribe(_addCommand.SetCanExecute));
            ShouldDispose(_canEdit.Subscribe(_editCommand.SetCanExecute));
            ShouldDispose(_canSave.Subscribe(_saveCommand.SetCanExecute));
            ShouldDispose(_canDelete.Subscribe(_deleteCommand.SetCanExecute));
            ShouldDispose(_canClose.Subscribe(_closeCommand.SetCanExecute));

            //executing command
            ShouldDispose(_addCommand.OnExecute.Subscribe(_ => AddModel()));
            ShouldDispose(_editCommand.OnExecute.Subscribe(_ => EditModel())); //ShouldDispose(_editCommand.OnExecute.Subscribe(_ => DialogService.ShowViewModel("Edit Person", this)));
            ShouldDispose(_saveCommand.OnExecute.Subscribe(_ => SaveModel()));// ShouldDispose(_saveCommand.OnExecute.Subscribe(_ => { SaveModel(); _closeSubject.OnNext(true); }));
            ShouldDispose(_deleteCommand.OnExecute.Subscribe(_ => DeleteModel()));// ShouldDispose(_deleteCommand.OnExecute.Subscribe(_ => MessageBus.Publish(createDeleteViewModelMessage())));
            ShouldDispose(_closeCommand.OnExecute.Subscribe(_ => CloseView())); //ShouldDispose(_closeCommand.OnExecute.Subscribe(_ => { ResetModel(); _closeSubject.OnNext(false); }));
        }

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

        #region action command executing

        /// <summary>
        /// Add a new Model
        /// </summary>
        protected virtual void AddModel()
        {
#if DEBUG
            if (Debugger.IsAttached) Debugger.Break();
#endif
        }

        /// <summary>
        /// Edit the model
        /// </summary>
        protected virtual void EditModel()
        {
#if DEBUG
            if (Debugger.IsAttached) Debugger.Break();
#endif
        }

        /// <summary>
        /// Delete Model
        /// </summary>
        protected virtual void DeleteModel()
        {
#if DEBUG
            if (Debugger.IsAttached) Debugger.Break();
#endif
        }

        /// <summary>
        /// Update the model with ViewModel
        /// </summary>
        protected virtual void SaveModel()
        {
#if DEBUG
            if (Debugger.IsAttached) Debugger.Break();
#endif

            CanSave = false;
            // Controller.Repository.Upsert(Model);
        }

        /// <summary>
        /// Close the view
        /// </summary>
        protected virtual void CloseView()
        {

        }

        /// <summary>
        /// Reset the ViewModel with Model
        /// </summary>
        protected virtual void ResetModel()
        {

        }
        #endregion





        public string MyName => _myName;



        #endregion

        public void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }

        public void ShouldDispose(IDisposable disposable)
        {
            _disposables.Add(disposable);
        }

        public IDoFluidCommand<T> For(ICommandObserver<T> cmd)
        {
            var fluidCommand = new FluidCommand<T>(cmd);
            _disposables.Add(fluidCommand);

            return fluidCommand;
        }

        public void Dispose()
        {
            _disposables.Dispose();
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
        }
        #endregion
    }
}
