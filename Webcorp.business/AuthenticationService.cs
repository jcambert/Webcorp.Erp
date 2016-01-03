using MongoDB.Driver;
using Ninject;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Webcorp.common;
using Webcorp.Controller;
using Webcorp.Model;

namespace Webcorp.Business
{
    public class AuthenticationService : IAuthenticationService,IInitializable
    {
        [Inject]
        public IUtilisateurBusinessHelper<Utilisateur> Helper { get; set; }

        public async Task<bool> Login(string societe,string username, string password)
        {
            var builder = Builders<Utilisateur>.Filter;
            var filter = builder.Eq("soc", societe) & builder.Eq("identifiant", username);// & builder.Eq("password", Cypher.Encrypt( password,ConfigurationManager.AppSettings["passphrase"] ));
            var result = await Helper.Find(filter);
            Utilisateur u= result.FirstOrDefault();
            if (u.IsNull()) return false;

            Principal.Identity = new CustomIdentity(u.Nom, u.Email, u.Roles.ToArray());
            Utilisateur = u;
            return true;

        }

        public async Task<bool> Logout()
        {
           await Task.Factory.StartNew(() => { Principal.Identity = new AnonymousIdentity(); });
           
            return true;
        }

        public void Initialize()
        {
            Principal = new CustomPrincipal();
            Principal.Identity = new AnonymousIdentity();
            Utilisateur = new Utilisateur();
            AppDomain.CurrentDomain.SetThreadPrincipal(Principal);
        }

       

        public Utilisateur Utilisateur { get; private set; }
        private CustomPrincipal Principal { get; set; }
    }
}
