using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webcorp.rx_mvvm
{
    public interface IReactiveViewModel<T> where T :class
    {
        bool CanAdd { get; set; }

        bool CanEdit { get; set; }

        bool CanRead { get; set; }

        bool CanCancel { get; set; }

        bool CanDelete { get; set; }

        bool CanSave { get; set; }

        bool CanViewList { get; set; }

        bool CanPrint { get; set; }

        ReactiveCommand<object> AddCommand { get; }

        ReactiveCommand<object> EditCommand { get; }

        ReactiveCommand<object> ReadCommand { get; }

        ReactiveCommand<object> DeleteCommand { get; }

        ReactiveCommand<object> CancelCommand { get; }

        ReactiveCommand<object> SaveCommand { get; }

        ReactiveCommand<object> ViewListCommand { get; }

        ReactiveCommand<object> PrintCommand { get; }

        void OnAdd(object arg);

        void OnEdit(object arg);

        void OnRead(object arg);

        void OnDelete(object arg);

        void OnCancel(object arg);

        void OnSave(object arg);

        void OnViewList(object arg);

        void OnPrint(object arg);

    }
}
