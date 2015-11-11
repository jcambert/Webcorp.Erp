using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Webcorp.Model;

namespace Webcorp.Controller
{
    public class BusinessAssemblyProvider : IBusinessAssemblyProvider
    {


        private readonly List<Assembly> assemblies;

        public BusinessAssemblyProvider()
        {
            assemblies = new List<Assembly>();
        }

        public List<Assembly> Assemblies => assemblies;

        public List<Type> BusinessControllers<T>() where T : IEntity<string>
        {
            List<Type> result = new List<Type>();
            Assemblies.ForEach(a =>
            {
                foreach (var t in a.GetTypes().Where(t => t.IsPublic & !t.IsAbstract))
                {

                    var attr = t.GetCustomAttribute<BusinessControllerAttribute>(true);
                    if (typeof(IBusinessController<T>).IsAssignableFrom(t) & attr != null)
                    {

                        result.Add(t);
                    }
                }
            });

            return result.Distinct().ToList() ;
        }
    }
}
