using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reactive.Linq;
using System.ComponentModel;

namespace Webcorp.rx_mvvm
{
    public class ViewModelStatus<T> : ReactiveObject, IViewModelStatus<T>, IDisposable where T : class
    {

        public const string Aucun = "Aucun";
        public const string Liste = "Liste";
        public const string Creation = "Creation";
        public const string Edition = "Edition";
        public const string Lecture = "Lecture";

        Dictionary<string, Action<ReactiveViewModel<T>>> cache = new Dictionary<string, Action<ReactiveViewModel<T>>>();
        IDisposable obs;
       
        public ViewModelStatus()
        {
            var p = Observable.FromEventPattern<PropertyChangedEventArgs>(this, "PropertyChanged").Select(pp => pp.EventArgs.PropertyName == "Status");

            obs = p.Subscribe(_ =>
            {
                if (cache.ContainsKey(_status))
                    cache[_status](ViewModel);
            });
        }
        string _status;
        public string Status { get { return _status; } set { this.RaiseAndSetIfChanged(ref _status, value); } }

        public ReactiveViewModel<T> ViewModel
        {
            get;  set;
        }

        public void Register(string v, Action<ReactiveViewModel<T>> p)
        {
            ViewModel.ThrowIfNull<NullReferenceException>("You must set ViewModel before Rregister Action");
            cache[v] = p;
        }

        public void Dispose()
        {

        }
    }
}
