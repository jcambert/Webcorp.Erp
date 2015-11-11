using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Threading.Tasks;

namespace Webcorp.rx_mvvm
{
    public class MessageBus : IMessageBus
    {
        private readonly ISubject<object> _messageBus = new Subject<object>();

        public IDisposable Subscribe<T>(Action<T> action)
        {
            return _messageBus.OfType<T>().Subscribe(action);
        }

        public void Publish<T>(T item)
        {
            _messageBus.OnNext(item);
        }
    }
}
