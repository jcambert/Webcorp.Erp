using System;
using System.Collections.Generic;
using Webcorp.Model;

namespace Webcorp.Controller
{
    public class ActionResult
    {
        static ActionResult()
        {
            Ok = new ActionResult(true, "");
        }
       
        public ActionResult(bool result,Exception e)
        {
            this.Result = result;
            this.Message = e==null?"": e.Message??"";
            this.Exception = e;
        }

        public ActionResult(bool result,string message)
        {
            this.Result = result;
            this.Message = message;
        }

        public bool Result { get; protected set; }

        public string Message { get; protected  set; }

        public Exception Exception { get; private set; }

        public virtual void Throw()
        {
            if (Result) return;
            Exception.Throw();
            throw new Exception(Message);
        }

        public static ActionResult Ok { get; private set; }

        public static ActionResult Create(bool result, Exception lastError)
        {
            return new ActionResult(result, lastError);
        }
    }

    public class ActionResult<T,TKey>:ActionResult where T : IEntity<TKey>
    {
        Exception _innerException;
        public ActionResult(List<ActionResult<T,TKey>> inner,T entity):base(true, "See Inner actions results")
        {
            this.InnerActionsResults = inner;
            foreach (var item in inner)
            {
                if (!item.Result) {
                    Result = false;
                    _innerException = item.Exception??new Exception(item.Message);
                    Message = item.Message;
                    break;
                }
            }
            
        }


        public ActionResult(bool result, Exception e,T entity):base(result,e)
        {
            this.Entity = entity;
        }

        public ActionResult(bool result, string message,T entity):base(result,message)
        {
            this.Entity = entity;
        }

        public T Entity { get; private set; }
        public List<ActionResult<T, TKey>> InnerActionsResults { get; private set; }

        public static ActionResult<T,TKey> Create(bool result, Exception lastError,T entity)
        {
            return new ActionResult<T,TKey>(result, lastError,entity);
        }

        public static ActionResult<T, TKey> Create(List<ActionResult<T, TKey>> inner, T entity)
        {
            return new ActionResult<T, TKey>(inner, entity);
       
        }
        public new static ActionResult<T, TKey> Ok(T entity) => new ActionResult<T, TKey>(true,"",entity);

        public override void Throw()
        {
            if (Result) return;
            if (InnerActionsResults != null & InnerActionsResults.Count > 0) throw new Exception(Message,_innerException);
            base.Throw();
        }
    }

    public class ActionResult<T> : ActionResult<T,string> where T : IEntity<string>
    {
        public ActionResult(bool result, Exception e, T entity) : base(result, e,entity)
        {
            
        }

        public ActionResult(bool result, string message, T entity) : base(result, message,entity)
        {
            
        }

        
    }


    /*public class InnerActionException<T,TKey> : Exception where T :IEntity<TKey>
    {
        public InnerActionException(string message, List<ActionResult<T, TKey>> inner):base(message)
        {
            this.Inner
        }
    }*/
}
