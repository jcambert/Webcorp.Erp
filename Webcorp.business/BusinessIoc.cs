using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Webcorp.Controller;
using Webcorp.Model;

namespace Webcorp.Business
{
    public class BusinessIoc : NinjectModule
    {
        public override void Load()
        {
            Bind(typeof(IBusinessHelper<>)).To(typeof(BusinessHelper<>)).InSingletonScope();
            Bind(typeof(IArticleBusinessHelper<>)).To(typeof(ArticleBusinessHelper<>)).InSingletonScope();
           // Bind(typeof(IBusinessController<>)).To(typeof(BusinessController<>)).InSingletonScope();
            Bind(typeof(IBusinessController<>)).To(typeof(ArticleBusinessController<>)).InSingletonScope();
        }
    }
}
