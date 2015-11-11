using Prism.Modularity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Webcorp.rx_mvvm;

namespace Webcorp.erp.article
{
    [Module(ModuleName = "ArticleModule")]
    [ModuleDependency("ErpApplicationModule")]
    public class ArticleModule:BaseModule
    {
    }
}
