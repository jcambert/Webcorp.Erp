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

namespace Webcorp.Business
{

    public interface IArticleBusinessHelper<T> : IBusinessHelper<T>, IActionRepository<T> where T : Article
    {
        T Create(ArticleType type);
        // void ChangeSource(T article, ArticleSource source);
        //Task<T> Get(string id);
    }

    public class ArticleBusinessHelper<T> : BusinessHelper<T>, IArticleBusinessHelper<T> where T : Article
    {

        public override void OnChanged(T article, string propertyName)
        {
            base.OnChanged(article, propertyName);


        }

        public override void OnChanging(T Article, string propertyName)
        {
            base.OnChanging(Article, propertyName);
        }

        public override void Attach(T entity)
        {


            base.Attach(entity);
           // if (entity.Source == ArticleSource.Interne) Detach(entity);
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
                /*entity.ShouldDispose(entity.Nomenclatures.ItemChanged.Where(x => x.PropertyName == "Source").Select(x => x.Sender).Subscribe(_ =>
                    {
                        entity.IsChanged = true;
                        SourceChanged(entity);

                    }));*/
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

        private void UpdateCout(T article)
        {

            article.CoutMP = new unite.Currency(0);
            foreach (var nome in article.Nomenclatures.Where(v => v.Version == article.NomenclatureVersion))
            {

            }
        }

      /*  private void SourceChanged(T article)
        {

            if (article.Source == ArticleSource.Externe)
            {
                Attach(article);
                //  var builder = Builders<T>.Filter;
                //var parents = await this.Controller.Repository.Find(builder.Eq("nomenc.article.artcod", article.Code));
            }
            else
            {
                Detach(article);
            }
        }*/

        public T Create(ArticleType type)
        {
            T result = Kernel.Get<T>();
            result.TypeArticle = type;

#if DEBUG
            result.Societe = "999";
#endif
          /*  if (result.TypeArticle == ArticleType.ProduitFini || result.IsAbstract())
                result.Source = ArticleSource.Externe;*/

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

            if (result.IsStockable() && result.MouvementsStocks.IsNull())
                result.MouvementsStocks = new MouvementsStocks();



            Attach(result);

            return result;
        }


        public  async Task<T> GetById(string id)
        {
            T result = await Controller.Repository.GetById(id);
            result.ShouldDispose(result.Nomenclatures.ItemRequested.Subscribe(async x => {
                x.Article = await this.Controller.Repository.GetById(x.ArticleId);
            }));
            return result;
        }

        public async Task<IEnumerable<T>> Find(Expression<Func<T, bool>> predicate = null)
        {
            return await Controller.Repository.Find(predicate);
        }

        public async Task<IEnumerable<T>> Find(FilterDefinition<T> filter)
        {
            return await Controller.Repository.Find(filter);
        }

        public async Task<bool> Upsert(T entity)
        {
            return await Controller.Repository.Upsert(entity);
        }

        public async Task<bool> Delete(string id)
        {
            return await Controller.Repository.Delete(id);
        }

        public async Task<bool> Delete(T entity)
        {
            return await Controller.Repository.Delete(entity);
        }

        public async Task<bool> Delete(Expression<Func<T, bool>> predicate = null)
        {
            return await Controller.Repository.Delete(predicate);
        }

        public async Task<bool> DeleteAll()
        {
            return await Controller.Repository.DeleteAll();
        }

        public Task<long> Count(Expression<Func<T, bool>> predicate = null)
        {
            throw new NotImplementedException();
        }

        public async Task<long> CountAll()
        {
            return await Controller.Repository.CountAll();
        }

        public async Task<bool> Exists(Expression<Func<T, bool>> predicate)
        {
            return await Controller.Repository.Exists(predicate);
        }
    }
}
