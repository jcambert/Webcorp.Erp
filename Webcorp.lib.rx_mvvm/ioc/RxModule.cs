using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webcorp.rx_mvvm
{
    public class RxModule : NinjectModule
    {
        public override void Load()
        {
            Bind(typeof(IPropertyProvider<,>)).To(typeof(PropertyProvider<,>)).InSingletonScope().WithMetadata("type",typeof(PropertyProvider<,>));
            Bind<IPropertyProviderFactory>().To<PropertyProviderFactory>().InSingletonScope();
            Bind<ISchedulers>().To<Schedulers>().InSingletonScope();
            Bind<IMessageBus>().To<MessageBus>().InSingletonScope();
            Bind<IDialogService>().To<DialogService>().InSingletonScope();
            Bind(typeof(IDeleteViewModelMessage<>)).To(typeof(DeleteViewModelMessage<>));
        }
    }
}
