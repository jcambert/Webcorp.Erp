using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Webcorp.Controller;
using Webcorp.Model;
using ReactiveUI;
namespace Webcorp.Business
{

    public interface IArticleBusinessHelper<T>:IBusinessHelper<T> where T : Article
    {
       
        //void AjouterMouvementStock(T article, DateTime dateTime, int value,MouvementSens sens);
    }
    public class ArticleBusinessHelper<T> : BusinessHelper<T>, IArticleBusinessHelper<T> where T : Article
    {
        /*public void AjouterMouvementStock(T article,DateTime dateTime, int value, MouvementSens sens)
        {
            MouvementStock mvt = new MouvementStock() { Date = dateTime, Quantite = value, Sens =sens };
            article.MouvementsStocks.Add(mvt);
            
            
        }*/

        public override void OnChanged(T article, string propertyName)
        {
            base.OnChanged(article, propertyName);
#if DEBUG
            if (article is Article && propertyName == "Density")
                article.Density =  article.Density / 2;
#endif
        }

        public override void OnChanging(T Article, string propertyName)
        {
            base.OnChanging(Article, propertyName);
        }

        public override void Attach(T entity)
        {
            if (IsAttached(entity)) return;
            base.Attach(entity);
            entity.ShouldDispose(entity.MouvementsStocks.CountChanged.Subscribe(_ => { entity.IsChanged = true; }));
            entity.ShouldDispose(entity.MouvementsStocks.ItemChanged.Subscribe(_ => entity.IsChanged = true));
            entity.ShouldDispose(entity.MouvementsStocks.ItemsAdded.Subscribe(_ => {
                entity.StockPhysique = entity.MouvementsStocks.StockPhysique;
            }));
            entity.ShouldDispose(entity.MouvementsStocks.ItemsRemoved.Subscribe(_ => {
                entity.StockPhysique = entity.MouvementsStocks.StockPhysique;
            }));
        }

    }
}
