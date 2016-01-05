using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Webcorp.Model;

namespace Webcorp.Dal
{

    public interface IDbSet : IDisposable
    {
        Entries Entries { get; }

        Task<int> SaveChanges(CancellationToken token = default(CancellationToken));

        
    }
    public interface IDbSet<T, TKey> :IDbSet where T : IEntity<TKey>
    {
        T Attach(T entity);

        EntityEntry Upsert(T entity);

        T Remove(T entity);

        EntityEntry Entry(T entity);

        Task<int> SaveChanges(T entity, CancellationToken token = default(CancellationToken));
    }

    public interface IDbSet<T>:IDbSet<T,string> where T : IEntity<string>
    {

    }
}
