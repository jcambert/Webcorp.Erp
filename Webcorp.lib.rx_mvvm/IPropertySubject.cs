using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Threading.Tasks;

namespace Webcorp.rx_mvvm
{
    public interface IPropertySubject<T> : ISubject<T>
    {
        T Value { get; set; }
    }
}
