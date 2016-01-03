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

    

    public interface IParametreBusinessController<T> : IBusinessController<T> where T : Parametre
    {

    }
    public class ParametreBusinessController<T> : BusinessController<T>, IParametreBusinessController<T> where T : Parametre
    {
        [Inject]
        public ParametreBusinessController(IRepository<T> repo) : base(repo)
        {

        }

        public override Task<ActionResult<T, string>> OnAfterUpsert(T entity)
        {
            entity.IsChanged = false;
            return base.OnAfterUpsert(entity);

        }
    }
}
