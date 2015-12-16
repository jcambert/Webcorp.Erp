using Splat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Diagnostics;

namespace Webcorp.erp.tests
{
    internal class DebugLogger : ILogger
    {
        public LogLevel Level
        {
            get; set;
        }

        public void Write([Localizable(false)] string message, LogLevel logLevel)
        {
            Trace.WriteLine(message);
        }
    }
}
