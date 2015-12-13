using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Webcorp.Controller;
using Webcorp.Model;

namespace Webcorp.Business
{

    public interface IArticleBusinessHelper<T>:IBusinessHelper<T> where T : Article
    {
        void AjouterInventaire(T article);
    }
    public class ArticleBusinessHelper<T> : BusinessHelper<T>,IArticleBusinessHelper<T> where T : Article
    {

    }
}
