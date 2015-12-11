using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Webcorp.Model;

namespace Webcorp.Controller
{
    public interface IBusinessControllerAssemblyProvider
    {
        List<Assembly> Assemblies { get; }

        List<Type> BusinessControllers<T>() where T : IEntity<string>;
    }
}
