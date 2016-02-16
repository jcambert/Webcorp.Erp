using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Webcorp.Model;

namespace Webcorp.erp.rest.Controllers
{
    public class ArticleController : ApiController
    {
        public ArticleController()
        {
           
        }
        [Inject]
        public IEntityProvider<Article,string> Provider { get; set; }

        /// <summary>
        /// List of all articles
        /// </summary>
        /// <returns>OData Articles</returns>
        public IQueryable<Article> GetAllArticles() => Provider.Entities.AsQueryable();


    }
}
