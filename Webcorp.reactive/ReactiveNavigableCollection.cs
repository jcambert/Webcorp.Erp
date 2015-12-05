using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using Webcorp.common;
using ReactiveUI;
using MongoDB.Bson.Serialization.Attributes;
using System.Windows.Threading;
using Ninject;
using System.Runtime.CompilerServices;

namespace Webcorp.reactive
{
    [Serializable]
    public class ReactiveNavigableCollection<T> : ReactiveCollection<T>, INavigable<T>/*, IDisposable, IShouldDisposable*/
    {
        private readonly CompositeDisposable _disposables = new CompositeDisposable();


        public ReactiveNavigableCollection(IList<T> list) : base(list)
        {
            Initialize();
        }
        public ReactiveNavigableCollection() : base()
        {
            Initialize();
        }
        private void Initialize()
        {
            GoFirst = CreateCommand(this.WhenAnyValue(x => x.CanGoFirst), this.OnGoFirst);
            GoNext = CreateCommand(this.WhenAnyValue(x => x.CanGoNext), this.OnGoNext);
            GoPrevious = CreateCommand(this.WhenAnyValue(x => x.CanGoPrevious), this.OnGoPrevious);
            GoLast = CreateCommand(this.WhenAnyValue(x => x.CanGoLast), this.OnGoLast);

            if (AssemblyHelper.IsRunningUnderTest)
            {
                var p = Observable.FromEventPattern<PropertyChangedEventArgs>(this, "PropertyChanged").Where(_ => _.EventArgs.PropertyName == "SelectedIndex").ObserveOn(Dispatcher.CurrentDispatcher);


                ShouldDispose(p.Subscribe(_ => { Model = this[_selectedIndex]; }));

                var c = Observable.FromEventPattern<PropertyChangedEventArgs>(this, "PropertyChanged").Where(x => x.EventArgs.PropertyName == "Model").ObserveOn(Dispatcher.CurrentDispatcher);
                ShouldDispose(c.Subscribe(_ =>
                {
                    //_selectedIndex = IndexOf(Model);
                    UpdateCapabilities();
                }));
            }
            else
            {


                var p = Observable.FromEventPattern<PropertyChangedEventArgs>(this, "PropertyChanged").Where(_ => _.EventArgs.PropertyName == "SelectedIndex").ObserveOnDispatcher(System.Windows.Threading.DispatcherPriority.Normal);


                ShouldDispose(p.Subscribe(_ => { Model = this[_selectedIndex]; }));

                var c = Observable.FromEventPattern<PropertyChangedEventArgs>(this, "PropertyChanged").Where(x => x.EventArgs.PropertyName == "Model").ObserveOnDispatcher(System.Windows.Threading.DispatcherPriority.Normal);
                ShouldDispose(c.Subscribe(_ =>
                {
                    //_selectedIndex = IndexOf(Model);
                    UpdateCapabilities();
                }));
            }
        }


        protected ReactiveCommand<object> CreateCommand(IObservable<bool> canExecute, Action<object> action)
        {
            var result = ReactiveCommand.Create(canExecute);
            result.Subscribe(action);
            ShouldDispose(result);
            return result;
        }
        #region Action Methods
        public void OnGoFirst(object arg)
        {
            MoveTo(0);
        }
        public void OnGoNext(object arg)
        {
            MoveTo(SelectedIndex + 1);
        }
        public void OnGoPrevious(object arg)
        {
            MoveTo(SelectedIndex - 1);
        }
        public void OnGoLast(object arg)
        {
            MoveTo(Count - 1);
        }
        #endregion

        #region IModelNavigationService<T>
        private bool _canGofirst;
        private bool _canGoLast;
        private bool _canGoNext;
        private bool _canGoPrevious;
        private T _current;
        [BsonIgnore]
        public bool CanGoFirst
        {
            get { return _canGofirst; }
            set { this.RaiseAndSetIfChanged(ref _canGofirst, value); }
        }
        [BsonIgnore]
        public bool CanGoLast
        {
            get { return _canGoLast; }
            set { this.RaiseAndSetIfChanged(ref _canGoLast, value); }
        }
        [BsonIgnore]
        public bool CanGoNext
        {
            get { return _canGoNext; }
            set { this.RaiseAndSetIfChanged(ref _canGoNext, value); }
        }
        [BsonIgnore]
        public bool CanGoPrevious
        {
            get { return _canGoPrevious; }
            set { this.RaiseAndSetIfChanged(ref _canGoPrevious, value); }
        }
        [BsonIgnore]
        public virtual T Model
        {
            get { return _current; }
            set { this.RaiseAndSetIfChanged(ref _current, value); }
        }
        [BsonIgnore]
        public ReactiveCommand<object> GoFirst
        {
            get; private set;
        }
        [BsonIgnore]
        public ReactiveCommand<object> GoLast
        {
            get; private set;
        }
        [BsonIgnore]
        public ReactiveCommand<object> GoNext
        {
            get; private set;
        }
        [BsonIgnore]
        public ReactiveCommand<object> GoPrevious
        {
            get; private set;
        }

        int _selectedIndex;
        [BsonIgnore]
        public int SelectedIndex
        {
            get { return _selectedIndex; }
            set
            {
                (!CanMoveTo(value)).ThrowIf<ArgumentOutOfRangeException>("Cannot move to Position:{0}".FormatWith(value));
                this.RaiseAndSetIfChanged(ref _selectedIndex, value);
            }
        }

        protected bool CanMoveTo(int index) => index.IsBound(0, Count - 1);

        public void MoveTo(int index)
        {

            SelectedIndex = index;
        }

        public void UpdateCapabilities()
        {
            CanGoFirst = (SelectedIndex != 0 && Count > 0);
            CanGoNext = (SelectedIndex < Count - 1);
            CanGoPrevious = (SelectedIndex - 1 >= 0);
            CanGoLast = (SelectedIndex != Count - 1);
        }

        #endregion


    }


    public class ReactiveVMCollection<T> :ReactiveNavigableCollection<T>, IReactiveViewModel<T>, IInitializable, ILoggable where T : class
    {

        public ReactiveVMCollection():base()
        {
           
        }
        public ReactiveVMCollection(IList<T> list):base(list)
        {
            
        }
        public T LastModel { get; set; }

        bool _canAdd;
        public bool CanAdd { get { return _canAdd; } set { this.RaiseAndSetIfChanged(ref _canAdd, value); } }

        bool _canEdit;
        public bool CanEdit { get { return _canEdit; } set { this.RaiseAndSetIfChanged(ref _canEdit, value); } }

        bool _canRead;
        public bool CanRead { get { return _canRead; } set { this.RaiseAndSetIfChanged(ref _canRead, value); } }

        bool _canDelete;
        public bool CanDelete { get { return _canDelete; } set { this.RaiseAndSetIfChanged(ref _canDelete, value); } }

        bool _canCancel;
        public bool CanCancel { get { return _canCancel; } set { this.RaiseAndSetIfChanged(ref _canCancel, value); } }

        bool _canSave;
        public bool CanSave { get { return _canSave; } set { this.RaiseAndSetIfChanged(ref _canSave, value); } }

        bool _canViewList;
        public bool CanViewList { get { return _canViewList; } set { this.RaiseAndSetIfChanged(ref _canViewList, value); } }

        bool _canPrint;
        public bool CanPrint { get { return _canPrint; } set { this.RaiseAndSetIfChanged(ref _canPrint, value); } }

        public ReactiveCommand<object> AddCommand { get; protected set; }

        public ReactiveCommand<object> EditCommand { get; protected set; }

        public ReactiveCommand<object> ReadCommand { get; protected set; }

        public ReactiveCommand<object> DeleteCommand { get; protected set; }

        public ReactiveCommand<object> CancelCommand { get; protected set; }

        public ReactiveCommand<object> SaveCommand { get; protected set; }

        public ReactiveCommand<object> ViewListCommand { get; protected set; }

        public ReactiveCommand<object> PrintCommand { get; protected set; }

        #region Ninject Initialize
        [Inject]
        public IKernel Container { get; set; }
        public virtual void Initialize()
        {
            
            ViewModelStatus.ViewModel = this;

            AddCommand = CreateCommand(this.WhenAnyValue(x => x.CanAdd), this.OnAdd);
            EditCommand = CreateCommand(this.WhenAnyValue(x => x.CanEdit), this.OnEdit);
            ReadCommand = CreateCommand(this.WhenAnyValue(x => x.CanRead), this.OnRead);
            DeleteCommand = CreateCommand(this.WhenAnyValue(x => x.CanRead), this.OnDelete);
            CancelCommand = CreateCommand(this.WhenAnyValue(x => x.CanCancel), this.OnCancel);
            SaveCommand = CreateCommand(this.WhenAnyValue(x => x.CanSave), this.OnSave);
            ViewListCommand = CreateCommand(this.WhenAnyValue(x => x.CanViewList), this.OnViewList);
            PrintCommand = CreateCommand(this.WhenAnyValue(x => x.CanPrint), this.OnPrint);



            ViewModelStatus.Register(ViewModelStatus<T>.Aucun, (x) =>
            {
                Debug("Change status to Aucun");
            });
            ViewModelStatus.Register(ViewModelStatus<T>.Creation, (x) =>
            {
                Debug("Change status to Creation");
                x.LastModel = x.Model.Clone();
                x.Add(Container.Resolve<T>()); GoLast.Execute(null);
                x.CanAdd = false;
                x.CanEdit = false;
                x.CanRead = false;
                x.CanDelete = false;
                x.CanCancel = true;
                x.CanSave = true;
                x.CanViewList = false;
            });

            ViewModelStatus.Register(ViewModelStatus<T>.Edition, (x) =>
            {
                Debug("Change status to Edition");
                x.LastModel = Model.Clone();
                x.CanAdd = false;
                x.CanEdit = false;
                x.CanRead = false;
                x.CanDelete = true;
                x.CanCancel = true;
                x.CanSave = true;
                x.CanViewList = false;
            });

            ViewModelStatus.Register(ViewModelStatus<T>.Lecture, (x) =>
            {
                Debug("Change status to Lecture");
                x.CanAdd = false;
                x.CanEdit = true;
                x.CanRead = false;
                x.CanDelete = true;
                x.CanCancel = false;
                x.CanSave = false;
                x.CanViewList = true;

            });

            ViewModelStatus.Register(ViewModelStatus<T>.Liste, (x) =>
            {
                Debug("Change status to Liste");
                x.CanAdd = true;
                x.CanEdit = false;
                x.CanRead = true;
                x.CanDelete = true;
                x.CanCancel = false;
                x.CanSave = false;
                x.CanViewList = false;

            });

            Status = ViewModelStatus<T>.Liste;

        }
        #endregion

        #region Status 

        [Inject]
        public IViewModelStatus<T> ViewModelStatus { get; set; }
        public string Status { get { return ViewModelStatus.Status; } set { ViewModelStatus.Status = value; } }

        #endregion

        #region Command Actions
        public virtual void OnAdd(object arg)
        {
            Status = ViewModelStatus<T>.Creation;
        }

        public virtual void OnEdit(object arg)
        {
            Status = ViewModelStatus<T>.Edition;
        }

        public virtual void OnRead(object arg)
        {
            Status = ViewModelStatus<T>.Lecture;
        }

        public virtual void OnDelete(object arg)
        {
            if (Model.IsNull()) return;
            var index = SelectedIndex;
            var max = Count;
            Remove(Model);
            if (index == 0)
                SelectedIndex = 0;
            else if (index < max)
                SelectedIndex = max;
            else
                SelectedIndex = index - 1;
            //Model = Models.Count > 0 ? Models[0] : null;
        }


        public virtual void OnCancel(object arg)
        {
            if (Status == ViewModelStatus<T>.Creation)
            {

                Remove(Model);
                Model = LastModel;
                Status = ViewModelStatus<T>.Liste;
            }
            else if (Status == ViewModelStatus<T>.Edition)
            {
                Remove(Model);
                Model = LastModel;
                Insert(SelectedIndex, Model);
                Status = ViewModelStatus<T>.Lecture;
            }
        }

        public virtual void OnSave(object arg)
        {
            Status = ViewModelStatus<T>.Lecture;
        }

        public virtual void OnViewList(object arg)
        {
            Status = ViewModelStatus<T>.Liste;
        }

        public virtual void OnPrint(object arg)
        {

        }
        #endregion



        #region Logger
        [Inject]
        public ILogger Logger { get; set; }
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



    }
}
