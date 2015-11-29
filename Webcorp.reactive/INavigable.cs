using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webcorp.reactive
{
    public interface INavigable
    {
        ReactiveCommand<object> GoFirst { get; }

        ReactiveCommand<object> GoPrevious { get; }

        ReactiveCommand<object> GoNext { get; }

        ReactiveCommand<object> GoLast { get; }

        bool CanGoFirst { get; set; }

        bool CanGoPrevious { get; set; }

        bool CanGoNext { get; set; }

        bool CanGoLast { get; set; }
    }

    public interface INavigable<T> : INavigable
    {

        int SelectedIndex { get; }

        T Model { get; set; }

    }
}
