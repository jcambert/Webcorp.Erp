﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Webcorp.Model;

namespace Webcorp.lib.onedcut
{
    [DebuggerDisplay("Quantity={ Quantity} / Length={Length}")]
    public class CutBeam:CustomReactiveObject
    {
        public int Quantity { get; set; }

        public int Length { get; set; }

        public override string ToString()
        {
            return "Quantity:" + Quantity + "/Length:" + Length;
        }
    }
}