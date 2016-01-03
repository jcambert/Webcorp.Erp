using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Webcorp.Controller;
using Webcorp.Dal;
using Webcorp.Model;

namespace Webcorp.Business
{


    public interface ICommandeBusinessController<T> : IBusinessController<T> where T : Commande
    {

    }
    public class CommandeBusinessController<T> : BusinessController<T>, ICommandeBusinessController<T> where T : Commande
    {
        [Inject]
        public CommandeBusinessController(IRepository<T> repo) : base(repo)
        {

        }

        public override Task<ActionResult<T, string>> OnAfterUpsert(T entity)
        {
            entity.IsChanged = false;
            return base.OnAfterUpsert(entity);

        }
    }
}
