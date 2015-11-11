using MongoDB.Bson;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Driver;
using Ninject;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Webcorp.Model;

namespace Webcorp.Dal
{

    public class Repository<T, TKey> : IRepository<T, TKey>
        where T : IEntity<TKey>
    {
        /// <summary>
        /// MongoCollection field.
        /// </summary>
        protected IMongoCollection<T> collection;
        private readonly IDbContext context;
        private Exception _lastError;

        [Inject]
        public Repository(IDbContext context)
        {
            this.context = context;

            this.CollectionName = Util<TKey>.GetCollectionName<T>();
            this.collection = this.context.Database.GetCollection<T>(this.CollectionName);
        }

        public IDbContext Context => context;

        /// <summary>
        /// Gets the Mongo collection (to perform advanced operations).
        /// </summary>
        /// <remarks>
        /// One can argue that exposing this property (and with that, access to it's Database property for instance
        /// (which is a "parent")) is not the responsibility of this class. Use of this property is highly discouraged;
        /// for most purposes you can use the MongoRepositoryManager&lt;T&gt;
        /// </remarks>
        /// <value>The Mongo collection (to perform advanced operations).</value>
        public IMongoCollection<T> Collection => this.collection;


        /// <summary>
        /// Gets the name of the collection
        /// </summary>
        public string CollectionName
        {
            get; private set;
        }

        /// <summary>
        /// Returns the T by its given id.
        /// </summary>
        /// <param name="id">The Id of the entity to retrieve.</param>
        /// <returns>The Entity T.</returns>
        public virtual async Task<T> GetById(TKey id)
        {
          /*  if (typeof(T).IsSubclassOf(typeof(Entity)))
            {
                return await this.GetById(new ObjectId(id as string));
            }*/
            return await this.collection.Find(entity => entity.Id.Equals(id)).FirstOrDefaultAsync();
        }

        /// <summary>
        /// Returns the T by its given id.
        /// </summary>
        /// <param name="id">The Id of the entity to retrieve.</param>
        /// <returns>The Entity T.</returns>
        public virtual async Task<T> GetById(ObjectId id) => await this.collection.Find(entity => entity.Id.Equals(id)).FirstOrDefaultAsync();


        /// <summary>
        /// Adds the new entity in the repository.
        /// </summary>
        /// <param name="entity">The entity T.</param>
        /// <returns>The added entity including its new ObjectId.</returns>
        public virtual async Task<bool> Upsert(T entity)
        {
            if (entity.Id == null)
            {

                try
                {
                    await this.collection.InsertOneAsync(entity);
                }

#pragma warning disable CS0168 // La variable est déclarée mais jamais utilisée
                catch (Exception ex)
#pragma warning restore CS0168 // La variable est déclarée mais jamais utilisée
                {
#if DEBUG
                    _lastError = ex;
                    if (Debugger.IsAttached) Debugger.Break();
#endif
                    return false;
                   
                }
               
                return true;
            }
            else
            {
                var result = await this.collection.ReplaceOneAsync(
                    filter: Builders<T>.Filter.Eq("_id", entity.Id),
                    replacement: entity,
                    options: new UpdateOptions() { IsUpsert = true });
                return result.ModifiedCount == 1;
            }

            //
            /* var id =  result.UpsertedId.AsObjectId ;
             if (typeof(T).IsSubclassOf(typeof(Entity)))
                 (entity as Entity).Id = id.ToString();*/



            //return true;
        }



        /// <summary>
        /// Deletes an entity from the repository by its id.
        /// </summary>
        /// <param name="id">The entity's id.</param>
        public virtual async Task<bool> Delete(TKey id)
        {

            var result = await this.collection.DeleteOneAsync(Builders<T>.Filter.Eq("_id", id));
            return result.DeletedCount == 1;
        }



        /// <summary>
        /// Deletes the given entity.
        /// </summary>
        /// <param name="entity">The entity to delete.</param>
        public virtual async Task<bool> Delete(T entity) => await this.Delete(entity.Id);


        /// <summary>
        /// Deletes the entities matching the predicate.
        /// </summary>
        /// <param name="predicate">The expression.</param>
        public virtual async Task<bool> Delete(Expression<Func<T, bool>> predicate = null)
        {
            var count = await this.collection.CountAsync(predicate);
            var result = await this.Collection.DeleteManyAsync(predicate);
            return result.DeletedCount == count;
        }


        /// <summary>
        /// Find all documents according to predicate
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public virtual async Task<IEnumerable<T>> Find(Expression<Func<T, bool>> predicate = null) => await this.collection.Find(predicate).ToListAsync();

        /// <summary>
        /// Deletes all entities in the repository.
        /// </summary>
        public virtual async Task<bool> DeleteAll() => await Delete(p => true);

        /// <summary>
        /// Counts the total entities in the repository.
        /// </summary>
        /// <returns>Count of entities in the collection.</returns>
        public virtual async Task<long> Count(Expression<Func<T, bool>> predicate = null) => await this.Collection.CountAsync(predicate ?? ((e) => true));


        public virtual async Task<long> CountAll() => await Count(p => true);

        /// <summary>
        /// Checks if the entity exists for given predicate.
        /// </summary>
        /// <param name="predicate">The expression.</param>
        /// <returns>True when an entity matching the predicate exists, false otherwise.</returns>
        public virtual async Task<bool> Exists(Expression<Func<T, bool>> predicate)
        {

            var result = await this.collection.FindAsync(predicate ?? (e => true));
            var list = await result.ToListAsync();
            return list.Count > 0;
        }



        #region IQueryable<T>
        /// <summary>
        /// Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>An IEnumerator&lt;T&gt; object that can be used to iterate through the collection.</returns>
        public virtual IEnumerator<T> GetEnumerator() => this.collection.AsQueryable<T>().GetEnumerator();

        /// <summary>
        /// Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>An IEnumerator object that can be used to iterate through the collection.</returns>
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => this.collection.AsQueryable<T>().GetEnumerator();




        /// <summary>
        /// Gets the type of the element(s) that are returned when the expression tree associated with this instance of IQueryable is executed.
        /// </summary>
        public virtual Type ElementType => this.collection.AsQueryable<T>().ElementType;


        /// <summary>
        /// Gets the expression tree that is associated with the instance of IQueryable.
        /// </summary>
        public virtual Expression Expression => this.collection.AsQueryable<T>().Expression;


        /// <summary>
        /// Gets the query provider that is associated with this data source.
        /// </summary>
        public virtual IQueryProvider Provider => this.collection.AsQueryable<T>().Provider;

        /// <summary>
        /// Get Last Error
        /// </summary>
        public Exception LastError => _lastError;

        #endregion
    }

    /// <summary>
    /// Deals with entities in MongoDb.
    /// </summary>
    /// <typeparam name="T">The type contained in the repository.</typeparam>
    /// <remarks>Entities are assumed to use strings for Id's.</remarks>
    public class Repository<T> : Repository<T, string>, IRepository<T>
        where T : IEntity<string>
    {
        /// <summary>
        /// Initializes a new instance of the MongoRepository class.
        /// Uses the Default App/Web.Config connectionstrings to fetch the connectionString and Database name.
        /// </summary>
        /// <remarks>Default constructor defaults to "MongoServerSettings" key for connectionstring.</remarks>
        [Inject]
        public Repository(IDbContext context)
            : base(context)
        { }


    }
}
