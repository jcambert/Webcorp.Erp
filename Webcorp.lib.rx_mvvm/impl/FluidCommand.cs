using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Disposables;
using System.Text;
using System.Threading.Tasks;

namespace Webcorp.rx_mvvm
{
    public class FluidCommand<T> : IDoFluidCommand<T>, IWhereFluidCommand<T>
    {
        private readonly CompositeDisposable _compositeDisposable;
        private readonly ICommandObserver<T> _cmd;

        public FluidCommand(ICommandObserver<T> cmd)
        {
            _cmd = cmd;
            _compositeDisposable = new CompositeDisposable();
        }

        public IWhereFluidCommand<T> Do(Action<T> action)
        {
            _compositeDisposable.Add(_cmd.OnExecute.Subscribe(action));
            return this;
        }

        public void Where(IObservable<bool> observable)
        {
            _compositeDisposable.Add(observable.Subscribe(_cmd.SetCanExecute));
        }

        public void Dispose()
        {
            _compositeDisposable.Dispose();
        }
    }
}
