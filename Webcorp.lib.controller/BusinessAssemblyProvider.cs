using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Webcorp.Model;

namespace Webcorp.Controller
{
   /* public class BusinessAssemblyProvider:IBusinessAssemblyProvider,IInitializable
    {
        private readonly List<Type> _businesses;
        private readonly List<Assembly> assemblies;

        public BusinessAssemblyProvider()
        {
            assemblies = new List<Assembly>();
          
        }

        public List<Assembly> Assemblies => assemblies;

        public List<Type> Businesses<T>() where T : IEntity<string>
        {
            List<Type> result = new List<Type>();
            Assemblies.ForEach(a =>
            {
                foreach (var t in a.GetTypes().Where(t => t.IsPublic & !t.IsAbstract))
                {

                    var attr = t.GetCustomAttribute<BusinessAttribute>(true);
                    if (typeof(IBusiness<T>).IsAssignableFrom(t) & attr != null)
                    {

                        result.Add(t);
                    }
                }
            });

            return result.Distinct().ToList();
        }

        public void Initialize()
        {
         
        }
    }*/
}
