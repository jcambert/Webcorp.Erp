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

        IRepository<T> Repository<T>() where T : IEntity;

        Task<int> SaveChangesAsync(CancellationToken token=default(CancellationToken));

        Task<int> SaveChangesAsync<TEntity>(TEntity entity, CancellationToken token = default(CancellationToken)) where TEntity : IEntity;

        EntityEntry Upsert<TEntity>(TEntity entity) where TEntity : IEntity;

        TEntity Remove<TEntity>(TEntity entity) where TEntity : IEntity;

        TEntity Attach<TEntity>(TEntity entity) where TEntity : IEntity;

        int Count<TEntity>() where TEntity : IEntity;

        EntityEntry Entry<TEntity>(TEntity entity) where TEntity : IEntity;

        int DeleteAll<TEntity>() where TEntity : IEntity;
    }
}
