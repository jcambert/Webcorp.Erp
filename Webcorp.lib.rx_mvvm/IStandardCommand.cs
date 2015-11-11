using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Webcorp.rx_mvvm
{
    public interface IStandardCommand
    {
        ICommand AddCommand { get; }
        ICommand EditCommand { get; }
        ICommand SaveCommand { get; }
        ICommand DeleteCommand { get; }
        ICommand CloseCommand { get; }

        bool CanAdd { get; set; }
        bool CanEdit { get; set; }
        bool CanSave { get; set; }
        bool CanDelete { get; set; }
        bool CanClose { get; set; }

    }
}
