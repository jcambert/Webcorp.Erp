using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Webcorp.Dal;
using Webcorp.Model;

namespace Webcorp.Controller
{
    public interface IEntityController<T,TKey>
        where T :IEntity<TKey>
    {
        /// <summary>
        /// Repository attached to controller
        /// </summary>
        IRepository<T,TKey> Repository { get; }
        /// <summary>
        /// Get all entity
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<T>> GetAll();
        /// <summary>
        /// Get Entity by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<T> Get(TKey id);
        /// <summary>
        /// Upsert Entity
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<ActionResult<T,TKey>> Post(T entity);


        

    }

    public interface IEntityController<T>: IEntityController<T,string> where T : IEntity<string> { }

    public interface IEntityController : IEntityController<IEntity> { };
    
}
