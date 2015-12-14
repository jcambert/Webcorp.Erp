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
 
    public interface IArticleBusinessController<T>: IBusinessController<T> where T : Article
    {

    }
    public  class ArticleBusinessController<T>: BusinessController<T>, IArticleBusinessController<T> where T :Article
    {
        [Inject]
        public ArticleBusinessController(IRepository<T> repo) : base(repo)
        {

        }

        public override Task<ActionResult<T, string>> OnAfterUpsert(T entity)
        {
            entity.IsChanged = false;
            return base.OnAfterUpsert(entity);
            
        }
    }
}
