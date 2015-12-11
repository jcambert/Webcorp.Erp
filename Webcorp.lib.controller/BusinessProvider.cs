using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Webcorp.Model;

namespace Webcorp.Controller
{
    public class BusinessProvider<T> : IBusinessProvider<T>, IInitializable where T : IEntity<string>
    {

        [Inject]
        public IKernel Container { get; set; }

        [Inject]
        public IBusinessAssemblyProvider BusinessAssemblies { get; set; }

        public IEnumerable<IBusiness<T, string>> Businesses => Container.GetAll<IBusiness<T>>();

        public void Initialize()
        {

            BusinessAssemblies.Businesses<T>().ForEach(a => Register(a));

        }



        private void Register(Type t)
        {

            Container.Bind(typeof(IBusinessController<T>)).To(t);
        }

      
    }
}
