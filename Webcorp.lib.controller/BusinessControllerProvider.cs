using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Webcorp.Model;

namespace Webcorp.Controller
{
/*
    public class BusinessControllerProvider<T> : IBusinessControllerProvider<T>, IInitializable where T : IEntity<string>
    {

        [Inject]
        public IKernel Container { get; set; }

        [Inject]
        public IBusinessControllerAssemblyProvider BusinessAssemblies{get;set;}

        public IEnumerable<IBusinessController<T, string>> Controllers=> Container.GetAll<IBusinessController<T>>();

        public void Initialize()
        {

            BusinessAssemblies.BusinessControllers<T>().ForEach(a => Register(a));

        }

        

        private  void Register(Type t)
        {
            
            Container.Bind(typeof(IBusinessController<T>)).To(t);
        }

       
    }*/
}
