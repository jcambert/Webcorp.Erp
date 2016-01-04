using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Webcorp.Model;

namespace Webcorp.Dal
{
    public interface IDbContext:IDisposable
    {
        IMongoDatabase Database { get;  }

        bool IsAlive();

        int SaveChanges();

        Task<int> SaveChangesAsync(CancellationToken token=default(CancellationToken));

        EntityEntry Upsert<TEntity>(TEntity entity) where TEntity : IEntity;

        TEntity Remove<TEntity>(TEntity entity) where TEntity : IEntity;

        TEntity Attach<TEntity>(TEntity entity) where TEntity : IEntity;

        
    }
}
