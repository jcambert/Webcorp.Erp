using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Webcorp.rx_mvvm.impl
{
    public class Message<T> : IMessage<T>
    {
        public Message(string message, Expression<Action<T>> expr)
        {
            this.Info = message;
            Action = expr.Compile();
        }


        public Action<T> Action
        {
            get;private set;
        }

        public string Info
        {
            get;private set;
        }
    }
}
