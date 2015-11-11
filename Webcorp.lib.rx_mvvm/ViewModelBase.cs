using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reactive.Disposables;
using System.Text;
using System.Threading.Tasks;

namespace Webcorp.rx_mvvm
{
    public abstract class ViewModelBase : INotifyPropertyChanged, IDisposable
    {
        private readonly CompositeDisposable _disposables = new CompositeDisposable();
        public event PropertyChangedEventHandler PropertyChanged;

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

        public IDoFluidCommand<T> For<T>(ICommandObserver<T> cmd)
        {
            var fluidCommand = new FluidCommand<T>(cmd);
            _disposables.Add(fluidCommand);

            return fluidCommand;
        }

        public void Dispose()
        {
            _disposables.Dispose();
        }
    }
}
