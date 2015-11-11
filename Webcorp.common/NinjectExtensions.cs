using Ninject;
using Ninject.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;


public static class NinjectExtensions
{
    public static T Resolve<T>(this IKernel kernel, params object[] values)
    {
        kernel.ThrowIfNull<ArgumentException>("Ninject Kernel cannot be null");
        if (values.IsNull() | !values.Any())
            return kernel.Get<T>();

        Type bindedType = kernel.GetBindings(typeof(T)).Select(a => a.Metadata.Get<Type>("type")).FirstOrDefault();
        bindedType.ThrowIfNull<ArgumentException>("Binded type must have 'type' metadata");

        ConstructorInfo[] allctor = bindedType.GetConstructors();

        ConstructorInfo matchctor = allctor.FirstOrDefault(c => IsMatch(c.GetParameters(), values));

        if (matchctor.IsNull()) return default(T);

        IParameter[] args = GetConstructorArguments(matchctor, values);

        return kernel.Get<T>(args);
    }

    private static IParameter[] GetConstructorArguments(ConstructorInfo ctor, object[] values)
    {
        return ctor.GetParameters()
            .Zip(values, (ctrParam, value) => new ConstructorArgument(ctrParam.Name, value))
            .ToArray();
    }

    private static bool IsMatch(ParameterInfo[] parameterInfo, object[] values)
    {
        return true;
#pragma warning disable CS0162 // Impossible d'atteindre le code détecté
        if (parameterInfo.Length != values.Length) return false;
#pragma warning restore CS0162 // Impossible d'atteindre le code détecté

        for (int i = 0; i < parameterInfo.Length; i++)
        {
            if (parameterInfo[i].ParameterType != values[i].GetType()) return false;
        }
        return true;
    }
}

