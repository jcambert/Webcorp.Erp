using Prism.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Webcorp.rx_mvvm
{
    public interface ILoggable
    {
        void Debug([CallerMemberName] string message = "");
        void Debug(string message, [CallerMemberName] string caller = "");
        void Info([CallerMemberName] string message = "");
        void Info(string message, [CallerMemberName] string caller = "");
        void Warn([CallerMemberName] string message = "");
        void Warn(string message, [CallerMemberName] string caller = "");
        void Exception([CallerMemberName] string message = "");
        void Exception(string message, [CallerMemberName] string caller = "");
    }
    public interface ILogger:ILoggable
    {
        ILoggerFacade LoggerFacade { get; set; }

        ILoggerFormatter Formatter {get;set;}
        //string MyName { get; }

        
    }

    public interface ILoggerFormatter
    {
        string Format(string message,string caller="");
    }
}
