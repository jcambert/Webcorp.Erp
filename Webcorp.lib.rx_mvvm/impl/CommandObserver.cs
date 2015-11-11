using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Webcorp.rx_mvvm
{
    public class CommandObserver<T> : ICommandObserver<T>
    {
        private bool _canExecute;
        private readonly ISubject<T> _executeSubject = new Subject<T>();
        private readonly ISubject<bool> _canExecuteSubject = new Subject<bool>();

        public CommandObserver() : this(true)
        {
        }

        public CommandObserver(bool value)
        {
            _canExecute = value;
            _canExecuteSubject.DistinctUntilChanged().Subscribe(v =>
            {
                _canExecute = v;
                if (CanExecuteChanged != null)
                    CanExecuteChanged(this, EventArgs.Empty);
            });
        }

        void ICommand.Execute(object parameter)
        {
            if (parameter is T)
                _executeSubject.OnNext((T)parameter);
            else
                _executeSubject.OnNext(default(T));
        }

        bool ICommand.CanExecute(object parameter)
        {
            return _canExecute;
        }

        public event EventHandler CanExecuteChanged;

        public IObservable<T> OnExecute
        {
            get { return _executeSubject.AsObservable(); }
        }

        public IObserver<bool> SetCanExecute
        {
            get { return _canExecuteSubject.AsObserver(); }
        }
    }
}
