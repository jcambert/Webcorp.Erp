using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Webcorp.Model;

namespace Webcorp.Dal
{
    public interface IDbSet<T, TKey> :IDisposable where T : IEntity<TKey>
    {
        T Attach(T entity);

        EntityEntry Upsert(T entity);

        T Remove(T entity);
    }

    public interface IDbSet<T>:IDbSet<T,string> where T : IEntity<string>
    {

    }
}
