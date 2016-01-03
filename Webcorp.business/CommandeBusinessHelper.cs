using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;
using Webcorp.Controller;
using Webcorp.Dal;
using Webcorp.Model;
using Ninject;
using System.Security.Permissions;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Attributes;

namespace Webcorp.Business
{
    public interface ICommandeBusinessHelper<T> : IBusinessHelper<T>, IActionRepository<T> where T : Commande
    {
        Task<T> Create(CommandeType type);

    }
    public class CommandeBusinessHelper<T> : BusinessHelper<T>, ICommandeBusinessHelper<T> where T : Commande
    {

        public CommandeBusinessHelper([Named("CommandeController")]IBusinessController<T> ctrl) : base(ctrl)
        {

        }

        [Inject]
        public IArticleBusinessHelper<Article> ArticleHelper { get; set; }

        [PrincipalPermission(SecurityAction.Demand, Authenticated = true)]
        public async Task<T> Create(CommandeType type)
        {
            T result = base.Create();

            result.Numero = await GenerateNewNumero();
            result.Societe = AuthService.Utilisateur.Societe;
            result.CommandeType = type;
            result.Statut = StatutCommande.Creer;



            return result;
        }

        public override void Attach(T entity)
        {
            base.Attach(entity);

            entity.ShouldDispose(entity.Lignes.CountChanged.Subscribe(x =>
            {
                entity.IsChanged = true;
            }));

            entity.ShouldDispose(entity.Lignes.ItemsAdded.Subscribe( l =>
            {
                
               /* string _ref = entity.Id ?? GenerateId(entity);
                Article art = await ArticleHelper.GetById(l.ArticleInner.Id);
               
                TypeBesoin tb = entity.CommandeType == CommandeType.Fournisseur ? TypeBesoin.Fournisseur : entity.CommandeType == CommandeType.Production ? TypeBesoin.Production : TypeBesoin.Null;
                if (tb != TypeBesoin.Null)
                    ArticleHelper.AddBesoin(art, DateTime.Now, l.QuantiteCommandee, entity.Id, tb);
               
                ArticleHelper.Save().Wait();*/
            }));
        }

        private async Task<int> GenerateNewNumero()
        {
            var ph = Kernel.Get<IParametreBusinessHelper<Parametre>>();
            var param = await ph.GetParametre(PParametres.COMPTEURCOMMANDE);
            param.ValeurEntier += 1;
            await ph.Save();
            return param.ValeurEntier ?? 0;
        }
        public async Task<long> Count(Expression<Func<T, bool>> predicate = null)
        {
            return await Controller.Repository.Count(predicate);
        }

        public async Task<long> CountAll()
        {
            return await Controller.Repository.CountAll();
        }


        public async Task<bool> Delete(Expression<Func<T, bool>> predicate = null)
        {
            return await Controller.Repository.Delete(predicate);
        }

        public async Task<bool> Delete(T entity)
        {
            return await Controller.Repository.Delete(entity);
        }

        public async Task<bool> Delete(string id)
        {
            return await Controller.Repository.Delete(id);
        }

        public async Task<bool> DeleteAll()
        {
            return await Controller.Repository.DeleteAll();
        }

        public async Task<bool> Exists(Expression<Func<T, bool>> predicate)
        {
            return await Controller.Repository.Exists(predicate);
        }

        public async Task<IEnumerable<T>> Find(FilterDefinition<T> filter)
        {
            return await Controller.Repository.Find(filter);
        }

        public async Task<IEnumerable<T>> Find(Expression<Func<T, bool>> predicate = null)
        {
            return await Controller.Repository.Find(predicate);
        }

        public async Task<T> GetById(string id)
        {
            return await Controller.Repository.GetById(id);
        }

        public async Task<bool> Upsert(T entity)
        {
            return await Controller.Repository.Upsert(entity);
        }
    }
}
