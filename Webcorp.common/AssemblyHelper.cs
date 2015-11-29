using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public static class AssemblyHelper
{
    public static bool IsRunningUnderTest=> AppDomain.CurrentDomain.GetAssemblies().Any(ass => ass.FullName.StartsWith("Microsoft.VisualStudio.TestPlatform"));
}

