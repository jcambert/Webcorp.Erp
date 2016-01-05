using MongoDB.Driver;
using Ninject;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;
using Webcorp.Controller;
using Webcorp.Dal;
using Webcorp.Model;

namespace Webcorp.Business
{


    public interface IParametreBusinessHelper<T> : IBusinessHelper<T> where T : Parametre
    {
        Task<T> Create(PParametre pp, string value);

        Task<Parametre> GetParametre(string numero);

    }
    public class ParametreBusinessHelper<T> : BusinessHelper<T>, IParametreBusinessHelper<T> where T : Parametre
    {


        public ParametreBusinessHelper([Named("ParametreController")]IBusinessController<T> ctrl) : base(ctrl)
        {

        }
        [PrincipalPermission(SecurityAction.Demand, Authenticated = true)]
        public async Task<T> Create(PParametre pp, string value)
        {
            var result = CreateAsync().ContinueWith(t =>
           {
               t.Result.Societe = AuthService.Utilisateur.Societe;
               t.Result.Numero = pp.Id;
               switch (pp.TypeParametre)
               {
                   case TypeParametre.Entier:
                       t.Result.ValeurEntier = Int32.Parse(value);
                       break;
                   case TypeParametre.Double:
                       t.Result.ValeurDouble = Double.Parse(value);
                       break;
                   case TypeParametre.Booleen:
                       t.Result.ValeurBooleen = bool.Parse(value);
                       break;
                   case TypeParametre.Texte:
                       t.Result.ValeurTexte = value;
                       break;
                   default:
                       break;
               }

               return t;
           });

            var res = await result.Result;
            Attach(res);
            return res;
        }

        


        public async Task<Parametre> GetParametre(string numero)
        {
            T p = Kernel.Get<T>();
            p.Societe = AuthService.Utilisateur.Societe;
            p.Numero = numero;
            var id = GenerateId(p);
            var result = await this.GetById(id);
            Attach(result);
            return result;
        }

    }

}
