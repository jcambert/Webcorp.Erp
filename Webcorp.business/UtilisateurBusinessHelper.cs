using MongoDB.Driver;
using Ninject;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Webcorp.common;
using Webcorp.Controller;
using Webcorp.Dal;
using Webcorp.Model;

namespace Webcorp.Business
{
 

    public interface IUtilisateurBusinessHelper<T> : IBusinessHelper<T>, IActionRepository<T> where T : Utilisateur
    {
        Task<T> Create(string societe,string id, string password, string nom, string prenom = "", string email = "");
        void AddRole(T user, string role);
        void RemoveRole(T user, string role);

    }
    public class UtilisateurBusinessHelper<T> : BusinessHelper<T>, IUtilisateurBusinessHelper<T> where T : Utilisateur
    {
        public UtilisateurBusinessHelper([Named("UtilisateurController")]IBusinessController<T> ctrl) : base(ctrl)
        {

        }
        
        public async Task<T> Create(string societe,string id,string password,string nom,string prenom="",string email="")
        {
            T result = await CreateAsync();
            result.Societe = societe;
            result.Identifiant = id;
            result.Password = Cypher.Encrypt(password, ConfigurationManager.AppSettings["passphrase"]);
            result.Nom = nom;
            result.Prenom = prenom;
            result.Email = email;
            return result;
        }

        public void AddRole(T user,string role)
        {
            user.Roles.Add(role);
        }

        public void RemoveRole(T user,string role)
        {
            user.Roles.Remove(role);
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
