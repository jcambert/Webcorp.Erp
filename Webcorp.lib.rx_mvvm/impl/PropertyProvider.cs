using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Webcorp.Model;

namespace Webcorp.rx_mvvm
{
    public class PropertyProvider<T,E> : IPropertyProvider<T,E> where T :IEntityViewModel<E> where E :IEntity
    {
        private  T _viewModel;
        
        private readonly CompositeDisposable _disposables = new CompositeDisposable();

        public T ViewModel
        {
            get
            {
                return _viewModel;
            }

            private set
            {
                _viewModel = value;
                _viewModel.ShouldDispose(_disposables);
            }
        }

        public PropertyProvider(T viewModel /*ISchedulers schedulers*/)
        {
            this.ViewModel = viewModel;
           /* _schedulers = schedulers*/;

            
        }
        [Inject]
        public ISchedulers Schedulers { get; set; }

        private PropertySubject<K> GetProperty<K>(Expression<Func<T, K>> expr)
        {
            var propertyName = ((MemberExpression)expr.Body).Member.Name;
            var propSubject = new PropertySubject<K>();

            _disposables.Add(propSubject.ObserveOn(Schedulers.Dispatcher)
                                        .Subscribe(v => _viewModel.OnPropertyChanged(propertyName)));

            return propSubject;
        }

        public void Dispose()
        {
            if (!_disposables.IsDisposed)
                _disposables.Dispose();
        }


        public IPropertySubject<K> CreateProperty<K>(Expression<Func<T, K>> expression)
        {
            return GetProperty(expression);
        }

        public IPropertySubject<K> CreateProperty<K>(Expression<Func<T, K>> expression, K value)
        {
            var propSubject = GetProperty(expression);
            propSubject.Value = value;
            return propSubject;
        }

        public IPropertySubject<K> CreateProperty<K>(Expression<Func<T, K>> expression, IObservable<K> values)
        {
            var propSubject = GetProperty(expression);
            _disposables.Add(values.Subscribe(v => propSubject.Value = v));
            return propSubject;
        }

        public ICommandObserver<K> CreateCommand<K>(Expression<Func<T, ICommand>> expression)
        {
            return new CommandObserver<K>(true);
        }

        public ICommandObserver<K> CreateCommand<K>(Expression<Func<T, ICommand>> expression, bool isEnabled)
        {
            return new CommandObserver<K>(isEnabled);
        }

        public ICommandObserver<K> CreateCommand<K>(Expression<Func<T, ICommand>> expression, IObservable<bool> isEnabled)
        {
            var cmd = new CommandObserver<K>(true);
            _disposables.Add(isEnabled.Subscribe(cmd.SetCanExecute));
            return cmd;
        }
    }
}
