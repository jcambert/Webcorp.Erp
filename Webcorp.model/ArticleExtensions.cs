using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webcorp.Model
{
    public static class ArticleExtensions
    {
        public static bool IsMakeable<T>(this T entity) where T:Article
        {
            return entity.TypeArticle == ArticleType.ProduitFini || entity.TypeArticle == ArticleType.ProduitSemiFini;
        }

        public static bool IsStockable<T>(this T entity) where T:Article
        {
            return (entity?.GererEnstock ?? false) && (entity.TypeArticle == ArticleType.SousTraitance || entity.TypeArticle == ArticleType.MatièrePremiere || IsMakeable(entity));
        }

        public static bool IsAbstract<T>(this T entity) where T : Article
        {
            return entity.TypeArticle == ArticleType.FraisGeneraux || entity.TypeArticle == ArticleType.Libelle;
        }

        public static bool MustBeExternal<T>(this T entity) where T : Article
        {
            return entity.IsAbstract();
        }
    }
}
