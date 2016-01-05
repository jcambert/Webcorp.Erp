using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Webcorp.Controller;
using Webcorp.Model;
using ReactiveUI;
using MongoDB.Driver;
using System.Reactive.Linq;
using Webcorp.Dal;
using System.Linq.Expressions;
using System.Security.Permissions;
using System.Diagnostics.Contracts;

namespace Webcorp.Business
{

    public interface IArticleBusinessHelper<T> : IBusinessHelper<T> where T : Article
    {
        Task<T> Create(string code, ArticleType type);
        void AddMouvementStock(T entity, DateTime date, int quantity, MouvementSens sens, string reference);
        void AddBesoin(T entity, DateTime now, int quantite, string reference, TypeBesoin tb);
    }

    public class ArticleBusinessHelper<T> : BusinessHelper<T>, IArticleBusinessHelper<T> where T : Article
    {

        public ArticleBusinessHelper([Named("ArticleController")]IBusinessController<T> ctrl) : base(ctrl)
        {

        }



        public void AddMouvementStock(T entity, DateTime date, int quantity, MouvementSens sens, string reference)
        {
            Contract.Requires(entity != null);
            var mvt = new MouvementStock() { Date = date, Quantite = quantity, Sens = sens, Reference = reference };
            entity.MouvementsStocks.Add(mvt);
            entity.IsChanged = true;
        }

        public void AddBesoin(T entity, DateTime now, int quantity, string reference, TypeBesoin tb)
        {
            Contract.Requires(entity != null);
            var besoin = new Besoin() { Date = now, QuantiteBesoin = quantity, Reference = reference, TypeBesoin = tb };
            entity.Besoins.Add(besoin);
            entity.IsChanged = true;
        }



        public override void Attach(T entity)
        {


            base.Attach(entity);
            if (entity.MouvementsStocks.IsNotNull())
            {
                entity.ShouldDispose(entity.MouvementsStocks.CountChanged.Subscribe(_ => { entity.IsChanged = true; }));
                entity.ShouldDispose(entity.MouvementsStocks.ItemChanged.Subscribe(_ => entity.IsChanged = true));
                entity.ShouldDispose(entity.MouvementsStocks.ItemsAdded.Subscribe(_ =>
                {
                    entity.StockPhysique = entity.MouvementsStocks.StockPhysique;

                }));
                entity.ShouldDispose(entity.MouvementsStocks.ItemsRemoved.Subscribe(_ =>
                {
                    entity.StockPhysique = entity.MouvementsStocks.StockPhysique;

                }));

            }

            if (entity.Nomenclatures.IsNotNull())
            {

                entity.ShouldDispose(entity.ObservableForProperty(x => x.NomenclatureVersion).Subscribe(_ => UpdateCout(entity)));
                entity.ShouldDispose(entity.Nomenclatures.CountChanged.Subscribe(_ =>
                {
                    entity.IsChanged = true;
                    UpdateCout(entity);
                }));

                entity.ShouldDispose(entity.Nomenclatures.ItemChanged.Subscribe(_ =>
                {

                    entity.IsChanged = true;
                    UpdateCout(entity);

                }));
            }

            /* entity.Changed.Where(x => x.PropertyName == "Source").Subscribe(_ =>
             {
                 SourceChanged(_.Sender as T);
             });*/


        }

        private void UpdateCout(T entity)
        {
            Contract.Requires(entity != null);
            entity.CoutMP = new unite.Currency(0);
            foreach (var nome in entity.Nomenclatures.Where(v => v.Version == entity.NomenclatureVersion))
            {

            }
        }



        [PrincipalPermission(SecurityAction.Demand, Authenticated = true)]
        public async Task<T> Create(string code, ArticleType type)
        {
            Contract.Requires(code != null);
            Contract.Requires(code.Trim().Length > 0);
            T result = await base.CreateAsync();
            result.Code = code;
            result.TypeArticle = type;
            result.Societe = AuthService.Utilisateur.Societe;



            if (result.IsMakeable() && result.Nomenclatures.IsNull())
            {
                result.Nomenclatures = new Nomenclatures();
                result.GererEnstock = true;
                result.ArticleFantome = false;
                result.AutoriseStockNegatif = true;
                result.GestionParLot = false;
                result.StockMini = 0;
                result.StockMaxi = 0;
                result.QuantiteMiniReappro = 0;
                if (result.GererEnstock ?? false)
                {
                    result.StockPhysique = 0;
                    result.StockReservee = 0;
                    result.StockAttendu = 0;

                }

                result.CoutPreparation = new unite.Currency(0);
                result.CoutMP = new unite.Currency(0);
                result.CoutMO = new unite.Currency(0);
                result.CoutST = new unite.Currency(0);
                result.CoutFG = new unite.Currency(0);

            }

            if (result.IsMakeable() && result.Productions.IsNull())
                result.Productions = new Productions();

            if (result.IsStockable() && result.MouvementsStocks.IsNull())
                result.MouvementsStocks = new MouvementsStocks();




            Attach(result);

            return result;
        }


        public override async Task<T> GetById(string id)
        {
            
            T result = await base.GetById(id);
           
            result.ShouldDispose(result.Nomenclatures.ItemRequested.Subscribe(async x =>
            {
                x.Article = await this.Controller.Repository.GetById(x.ArticleId);
            }));
            return result;
        }
        
    }
}
