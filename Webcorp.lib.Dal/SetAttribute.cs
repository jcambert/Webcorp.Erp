﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webcorp.Dal
{
    [AttributeUsage(AttributeTargets.Property,AllowMultiple = false,Inherited = false)]
    public class SetAttribute:Attribute
    {
    }
}
