using AutoMapper;
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
            Bind(typeof(ICommandeBusinessHelper<>)).To(typeof(CommandeBusinessHelper<>)).InSingletonScope();
            Bind(typeof(IUtilisateurBusinessHelper<>)).To(typeof(UtilisateurBusinessHelper<>)).InSingletonScope();
            Bind(typeof(IParametreBusinessHelper<>)).To(typeof(ParametreBusinessHelper<>)).InSingletonScope();

            Bind(typeof(IAuthenticationService)).To(typeof(AuthenticationService)).InSingletonScope();


            Bind(typeof(IBusinessController<>)).To(typeof(ArticleBusinessController<>)).InSingletonScope().Named("ArticleController");
            Bind(typeof(IBusinessController<>)).To(typeof(CommandeBusinessController<>)).InSingletonScope().Named("CommandeController");
            Bind(typeof(IBusinessController<>)).To(typeof(UtilisateurBusinessController<>)).InSingletonScope().Named("UtilisateurController");
            Bind(typeof(IBusinessController<>)).To(typeof(ParametreBusinessController<>)).InSingletonScope().Named("ParametreController");


            Mapper.CreateMap<Article, ArticleCommande>();

            ArticleBusinessExtensions.Kernel = Kernel;
        }
    }
}
