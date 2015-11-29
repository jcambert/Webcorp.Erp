using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webcorp.reactive
{
    public class ReactiveModule : NinjectModule
    {
        public override void Load()
        {

            Bind<ILoggerFormatter>().To<LoggerFormatter>().InSingletonScope();
            Bind<ILogger>().To<Logger>().InSingletonScope();

            Bind(typeof(IReactiveViewModel<>)).To(typeof(ReactiveVMCollection<>));
            Bind(typeof(IViewModelStatus<>)).To(typeof(ViewModelStatus<>));
        }
    }
}
