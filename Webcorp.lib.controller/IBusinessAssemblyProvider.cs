﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Webcorp.Model;

namespace Webcorp.Controller
{
    public interface IBusinessAssemblyProvider
    {
        List<Assembly> Assemblies { get; }

        List<Type> Businesses<T>() where T : IEntity<string>;
    }
}
