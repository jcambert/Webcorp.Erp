using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Concurrency;
using System.Text;
using System.Threading.Tasks;

namespace Webcorp.rx_mvvm
{
    public class Schedulers : ISchedulers
    {


        public IScheduler Dispatcher
        {
            get { return DispatcherScheduler.Current; }
        }

        public IScheduler NewThread
        {
            get { return NewThreadScheduler.Default; }
        }

        public IScheduler NewTask
        {
            get { return TaskPoolScheduler.Default; }
        }

        public IScheduler ThreadPool
        {
            get { return ThreadPoolScheduler.Instance; }
        }

        public IScheduler Timer
        {
            get { return ImmediateScheduler.Instance; }
        }
    }
}
