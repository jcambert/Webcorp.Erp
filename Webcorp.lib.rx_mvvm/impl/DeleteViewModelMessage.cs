using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Webcorp.Model;

namespace Webcorp.rx_mvvm
{
    public class DeleteViewModelMessage<T>: IDeleteViewModelMessage<T> where T :IEntity
    {
        public DeleteViewModelMessage()
        {

        }
        public DeleteViewModelMessage(T viewModel)
        {
            Model = viewModel;
        }

        public T Model { get;  set; }
    }
}
