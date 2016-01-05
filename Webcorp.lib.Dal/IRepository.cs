using MongoDB.Bson;
using MongoDB.Driver;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Webcorp.Model;

namespace Webcorp.Dal
{

    public interface IActionRepository<T,TKey> where T : IEntity<TKey>
    {

        /// <summary>
        /// Returns the T by its given id.
        /// </summary>
        /// <param name="id">The value representing the ObjectId of the entity to retrieve.</param>
        /// <returns>The Entity T.</returns>
        Task<T> GetById(TKey id);

        /// <summary>
        /// Find All document according to predicate
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        Task<IEnumerable<T>> Find(Expression<Func<T, bool>> predicate = null);

        /// <summary>
        /// Find All documents according to Filter
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        Task<IEnumerable<T>> Find(FilterDefinition<T> filter);


        /// <summary>
        /// Adds the new entity in the repository.
        /// </summary>
        /// <param name="entity">The entity to add.</param>
        /// <returns>The added entity including its new ObjectId.</returns>
        Task<bool> Upsert(T entity);

        Task<bool> Add(T entity);

        Task<bool> Update(T entity, ReactiveList<KeyValue> properties);

        /// <summary>
        /// Deletes an entity from the repository by its id.
        /// </summary>
        /// <param name="id">The entity's id.</param>
        Task<bool> Delete(TKey id);

        /// <summary>
        /// Deletes the given entity.
        /// </summary>
        /// <param name="entity">The entity to delete.</param>
        Task<bool> Delete(T entity);

        /// <summary>
        /// Deletes the entities matching the predicate.
        /// </summary>
        /// <param name="predicate">The expression.</param>
        Task<bool> Delete(Expression<Func<T, bool>> predicate = null);

        /// <summary>
        /// Deletes all entities in the repository.
        /// </summary>
        Task<bool> DeleteAll();

        /// <summary>
        /// Counts the total entities in the repository.
        /// </summary>
        /// <returns>Count of entities in the repository.</returns>
        Task<long> Count(Expression<Func<T, bool>> predicate = null);

        Task<long> CountAll();

        /// <summary>
        /// Checks if the entity exists for given predicate.
        /// </summary>
        /// <param name="predicate">The expression.</param>
        /// <returns>True when an entity matching the predicate exists, false otherwise.</returns>
        Task<bool> Exists(Expression<Func<T, bool>> predicate);
    }
    public interface IRepository<T, TKey> : IQueryable<T>,IActionRepository<T,TKey>
        where T : IEntity<TKey>
    {

        /// <summary>
        /// Get Last error
        /// </summary>
        Exception LastError { get; }

        /// <summary>
        /// Gets the Mongo collection (to perform advanced operations).
        /// </summary>
        /// <remarks>
        /// One can argue that exposing this property (and with that, access to it's Database property for instance
        /// (which is a "parent")) is not the responsibility of this class. Use of this property is highly discouraged;
        /// for most purposes you can use the MongoRepositoryManager&lt;T&gt;
        /// </remarks>
        /// <value>The Mongo collection (to perform advanced operations).</value>
        IMongoCollection<T> Collection { get; }


    }

    /// <summary>
    /// IRepository definition.
    /// </summary>
    /// <typeparam name="T">The type contained in the repository.</typeparam>
    /// <remarks>Entities are assumed to use strings for Id's.</remarks>
    public interface IActionRepository<T> : IActionRepository<T, string>
        where T : IEntity<string>
    { }

    /// <summary>
    /// IRepository definition.
    /// </summary>
    /// <typeparam name="T">The type contained in the repository.</typeparam>
    /// <remarks>Entities are assumed to use strings for Id's.</remarks>
    public interface IRepository<T> : IQueryable<T>, IRepository<T, string>
        where T : IEntity<string>
    { }


}
