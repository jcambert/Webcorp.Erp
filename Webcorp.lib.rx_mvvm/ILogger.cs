using Prism.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webcorp.rx_mvvm
{
    public interface ILogger
    {
        ILoggerFacade Logger { get; set; }

        string MyName { get; }

        void Debug(string message);
        void Info(string message);
        void Warn(string message);
        void Exception(string message);
    }
}
