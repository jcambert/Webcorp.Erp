using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Webcorp.Dal;
using Webcorp.Model;

namespace Webcorp.Controller
{
    public class BusinessEntityController<T, TKey> : EntityController<T, TKey>, IBusinessProvider<T, TKey>
        where T : IEntity<TKey>
    {
        private readonly IEnumerable<IBusinessController<T, TKey>> _business;

        [Inject]
        public BusinessEntityController(IRepository<T, TKey> repo, IBusinessControllerProvider<T, TKey> business) : base(repo)
        {

            this._business = business.Controllers;
            foreach (var item in this._business)
            {
                item.User = this.User;
            }

        }

        public IEnumerable<IBusinessController<T, TKey>> Business => _business;

        public override async Task<ActionResult<T,TKey>> Post(T entity)
        {
            List<ActionResult<T, TKey>> results = new List<ActionResult<T, TKey>>();
            await Business.ForEachAsync(async b => {
                results.Add(await b.OnBeforeUpsert(entity));
            });
            results.Add(await base.Post(entity));
            
            await Business.ForEachAsync(async b => {
                results.Add(await b.OnBeforeUpsert(entity));
            });
            return ActionResult<T,TKey>.Create(results, entity);
        }
    }

    public class BusinessEntityController<T> : BusinessEntityController<T, string>, IEntityController<T> where T : IEntity<string>
    {
        [Inject]
        public BusinessEntityController(IRepository<T> repo, IBusinessControllerProvider<T> business) : base(repo, business)
        {

        }
    }

}
