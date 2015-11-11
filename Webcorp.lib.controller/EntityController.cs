using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Webcorp.Dal;
using Webcorp.Model;

namespace Webcorp.Controller
{
    public class EntityController<T, TKey> : ApiController, IEntityController<T, TKey>
        where T : IEntity<TKey>
    {
       
        [Inject]
        public EntityController(IRepository<T,TKey> repo)
        {
            this._repo = repo;
        }
       
        private readonly IRepository<T, TKey> _repo;

        public IRepository<T, TKey> Repository => _repo;

        public virtual async  Task<T> Get(TKey id)=>await Repository.GetById(id);
        
        public virtual async Task<IEnumerable<T>> GetAll()=>await  Repository.Find();

        public virtual async Task<ActionResult<T,TKey>> Post(T entity)
        {
            var result= await Repository.Upsert(entity);
            return ActionResult<T,TKey>.Create(result, Repository.LastError,entity);
        }
        
    }
    public class EntityController<T> : EntityController<T, string>, IEntityController<T>
        where T : IEntity<string>
    {
        [Inject]
        public EntityController(IRepository<T> repo):base(repo)
        {
        }
    }
   }
