using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Webcorp.Controller;
using Webcorp.Model;

namespace Webcorp.Business
{
    public static class ArticleBusinessExtensions
    {
        internal static IKernel Kernel {get;set;}

        private static object _helper;
        public static  void SetHelper<T> (IArticleBusinessHelper<T> helper)where T : Article
        {
            _helper = helper;
        }

        private static IArticleBusinessHelper<T> Helper<T>() where T : Article
        {
            if (_helper.IsNull()) _helper = Kernel.Get<IArticleBusinessHelper<T>>();
            return _helper as IArticleBusinessHelper<T>;
        }

        public static async Task<ActionResult<T, string>> Save<T>(this T entity) where T : Article
        {
           return await Helper<T>().Save(entity);
        }
    }
}
