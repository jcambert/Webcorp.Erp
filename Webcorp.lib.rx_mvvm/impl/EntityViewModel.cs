using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Webcorp.Model;
using Webcorp.Controller;
using System.Linq.Expressions;
using Prism.Logging;

namespace Webcorp.rx_mvvm
{
   /* public class EntityViewModel<T> : ViewModelBase<T>,  IInitializable,ILogger where T : Entity
    {
        private readonly string _myName;
        private IPropertyProvider<IEntityViewModel<T>> _propertyProvider;
        private readonly ISubject<bool> _closeSubject = new Subject<bool>();
        private ICommandObserver<Unit> _editCommand;
        private ICommandObserver<Unit> _saveCommand;
        private ICommandObserver<Unit> _deleteCommand;
        private ICommandObserver<Unit> _closeCommand;

        private IPropertySubject<bool> _canSave;
        
        #region ctor
        public EntityViewModel()
        {
            _myName = GetType().Name;
        }
        public EntityViewModel(T entity):this()
        {
            Model = entity;
        }
        #endregion

        #region properties
        [Inject]
        public ILoggerFacade Logger { get; set; }
        [Inject]
        public IPropertyProviderFactory PropertyProviderFactory { get; set; }
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
        #endregion

       
        #region ICloseable
        public IObservable<bool> Close => _closeSubject.AsObservable();
        #endregion

        #region IEntityViewModel
        public ICommand CloseCommand => _closeCommand;

        public ICommand DeleteCommand => _deleteCommand;

        public ICommand EditCommand => _editCommand;

        public ICommand SaveCommand => _saveCommand;

        public IPropertyProvider<IEntityViewModel<T>,T> PropertyProvider => _propertyProvider;
       
        #endregion

        #region Ninject
        public virtual void Initialize()
        {
            Debug("Start Initialize");
            _propertyProvider = PropertyProviderFactory.Create(this);
            //_propertyProvider = PropertyProviderFactory.Create<IEntityViewModel<T>>(this);
            // creating properties
            CreateProperties();
            //creating commands
            CreateCommands();
            //observing commands
            ObservingCommands();
            Debug("End Initialize");

        }


        private IDeleteViewModelMessage<IEntityViewModel<T>, T> createDeleteViewModelMessage()
        {
            var result = Container.Get<IDeleteViewModelMessage<IEntityViewModel<T>, T>>();
            result.Model = this;
            return result;
        }

        protected virtual void CreateProperties()
        {
            _canSave = PropertyProvider.CreateProperty(i => i.CanSave, CanSave);
        }

        protected virtual void CreateCommands() 
        {

            // creating base commands
            _editCommand = CreateCommand( i => i.EditCommand);
            _saveCommand = CreateCommand( i => i.SaveCommand, CanSave);
            _deleteCommand = CreateCommand( i => i.DeleteCommand);
            _closeCommand = CreateCommand( i => i.CloseCommand);
        }

        protected ICommandObserver<Unit> CreateCommand( Expression<Func<IEntityViewModel<T>, ICommand>> e, bool IsEnabled = true)
        {
            return PropertyProvider.CreateCommand<Unit>(e);
        }

        protected ICommandObserver<Unit> CreateCommand<TV>(Expression<Func<TV, ICommand>> e, bool IsEnabled = true)where TV:IEntityViewModel<T>
        {
            return (PropertyProvider as IPropertyProvider<TV>).CreateCommand<Unit>(e);
        }

        protected virtual void ObservingCommands()
        {
            // Observing commands
            ShouldDispose(_canSave.Subscribe(_saveCommand.SetCanExecute));
            ShouldDispose(_saveCommand.OnExecute.Subscribe(_ =>
            {
                UpdateModel();
                _closeSubject.OnNext(true);
            }));
            //ShouldDispose(_editCommand.OnExecute.Subscribe(_ => DialogService.ShowViewModel("Edit Person", this)));
            ShouldDispose(_deleteCommand.OnExecute.Subscribe(_ => MessageBus.Publish(createDeleteViewModelMessage())));
            ShouldDispose(_closeCommand.OnExecute.Subscribe(_ =>
            {
                ResetData();
                _closeSubject.OnNext(false);
            }));
        }

        public void Debug(string message)
        {
            Logger.Log(MyName + "-" + message, Category.Debug, Priority.Low);
        }
        public void Info(string message)
        {
            Logger.Log(MyName + "-" + message, Category.Info, Priority.Low);
        }
        public void Warn(string message)
        {
            Logger.Log(MyName + "-" + message, Category.Warn, Priority.Medium);
        }
        public void Exception(string message)
        {
            Logger.Log(MyName + "-" + message, Category.Exception, Priority.High);
        }
        /// <summary>
        /// Update the model with ViewModel
        /// </summary>
        protected virtual void UpdateModel()
        {
            Controller.Repository.Upsert(Model);
        }

        /// <summary>
        /// Reset the ViewModel with Model
        /// </summary>
        protected virtual void ResetData()
        {

        }

        public virtual bool CanSave
        {
            get;
        }




        public string MyName => _myName;

        #endregion
    }*/
}
