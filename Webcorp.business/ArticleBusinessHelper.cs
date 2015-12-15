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

    public interface IArticleBusinessHelper<T> : IBusinessHelper<T> where T : Article
    {

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
            if (IsAttached(entity)) return;
            base.Attach(entity);
            
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

            entity.ShouldDispose(entity.ObservableForProperty(x => x.NomenclatureVersion).Subscribe(_ => UpdateCout(entity)));
            entity.ShouldDispose(entity.Nomenclatures.CountChanged.Subscribe(_ =>
            {
                entity.IsChanged = true;
                UpdateCout(entity);
            }));
            entity.ShouldDispose(entity.Nomenclatures.ItemChanged.Subscribe(_=>{
                 entity.IsChanged = true;
                UpdateCout(entity);
            } ));

        }

        private void UpdateCout(T entity)
        {
            entity.CoutMP = new unite.Currency(0);
            foreach (var nome in entity.Nomenclatures.Where(v => v.Version == entity.NomenclatureVersion))
            {
                
            }
        }
    }
}
