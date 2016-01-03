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


    public interface IUtilisateurBusinessController<T> : IBusinessController<T> where T : Utilisateur
    {

    }
    public class UtilisateurBusinessController<T> : BusinessController<T>, IUtilisateurBusinessController<T> where T : Utilisateur
    {
        [Inject]
        public UtilisateurBusinessController(IRepository<T> repo) : base(repo)
        {

        }

        public override Task<ActionResult<T, string>> OnAfterUpsert(T entity)
        {
            entity.IsChanged = false;
            return base.OnAfterUpsert(entity);

        }
    }
}
