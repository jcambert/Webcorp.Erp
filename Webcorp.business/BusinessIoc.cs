using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Webcorp.Model;

namespace Webcorp.Business
{
    public class BusinessIoc : NinjectModule
    {
        public override void Load()
        {
            Bind(typeof(IArticleBusinessHelper<>)).To(typeof(ArticleBusinessHelper<>));
        }
    }
}
