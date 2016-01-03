using MongoDB.Driver;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Webcorp.Controller;
using Webcorp.Dal;
using Webcorp.Model;

namespace Webcorp.Business
{
    

    public interface IParametreBusinessHelper<T> : IBusinessHelper<T>, IActionRepository<T> where T : Parametre
    {
        Task<T> Create(PParametre pp,string value);

        Task<Parametre> GetParametre(string numero);

    }
    public class ParametreBusinessHelper<T> : BusinessHelper<T>, IParametreBusinessHelper<T> where T : Parametre
    {

        
        public ParametreBusinessHelper([Named("ParametreController")]IBusinessController<T> ctrl) : base(ctrl)
        {

        }

        public async Task<T> Create(PParametre pp,string value)
        {
            var result =  CreateAsync().ContinueWith(t=> 
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

           return await  result.Result;
           
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

        public async Task<Parametre> GetParametre(string numero)
        {
            T p = Kernel.Get<T>();
            p.Societe = AuthService.Utilisateur.Societe;
            p.Numero = numero;
            var id = GenerateId(p);
            var result= await this.GetById(id);
            Attach(result);
            return result;
        }
    }

}
