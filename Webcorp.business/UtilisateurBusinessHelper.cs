using MongoDB.Driver;
using Ninject;
using ReactiveUI;
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
 

    public interface IUtilisateurBusinessHelper<T> : IBusinessHelper<T> where T : Utilisateur
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
            Attach(result);
            return result;
        }

        public void AddRole(T user,string role)
        {
            user.Roles.Add(role);
            user.IsChanged = true;
        }

        public void RemoveRole(T user,string role)
        {
            user.Roles.Remove(role);
            user.IsChanged = true;
        }    
    }
}
