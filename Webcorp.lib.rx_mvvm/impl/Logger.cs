using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Prism.Logging;
using Ninject;

namespace Webcorp.rx_mvvm
{
    public class Logger : ILogger
    {
      
        

        [Inject]
        public ILoggerFacade LoggerFacade { get; set; }
        [Inject]
        public ILoggerFormatter Formatter { get; set; }

        protected virtual string Format(string message,string caller = "")
        {
            return Formatter.Format(message, caller);
        }

        public void Debug([CallerMemberName] string message = "")
        {
            LoggerFacade.Log(Format(message), Category.Debug, Priority.Low);
        }

        public void Debug(string message, [CallerMemberName] string caller = "")
        {
            LoggerFacade.Log(Format(message,caller), Category.Debug, Priority.Low);
        }

        public void Exception([CallerMemberName] string message = "")
        {
            
            LoggerFacade.Log(Format(message), Category.Exception, Priority.High);
        }

        public void Exception(string message, [CallerMemberName] string caller = "")
        {
            LoggerFacade.Log(Format(message,caller), Category.Exception, Priority.High);
        }

        public void Info([CallerMemberName] string message = "")
        {
            LoggerFacade.Log(Format(message), Category.Info, Priority.Low);
        }

        public void Info(string message, [CallerMemberName] string caller = "")
        {
            LoggerFacade.Log(Format(message,caller), Category.Info, Priority.Low);
        }

        public void Warn([CallerMemberName] string message = "")
        {
            LoggerFacade.Log(Format(message), Category.Warn, Priority.Medium);
        }

        public void Warn(string message, [CallerMemberName] string caller = "")
        {
            LoggerFacade.Log(Format(message,caller), Category.Warn, Priority.Medium);
        }
    }

    public class LoggerFormatter : ILoggerFormatter
    {
        public string Format(string message, string caller = "")
        {

            if (caller.IsNullOrEmpty()) return message;
            return caller + "-" + message;
        }
    }
}
