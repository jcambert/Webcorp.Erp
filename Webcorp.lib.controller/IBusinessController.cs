﻿using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using Webcorp.Dal;
using Webcorp.Model;

namespace Webcorp.Controller
{
    public interface IBusinessController<T, TKey>:IEntityController<T,TKey> where T : IEntity<TKey>
    {
      //  IUser User { get; set; }

        Task<ActionResult<T,TKey>> OnBeforeUpsert(T entity);
        Task<ActionResult<T, TKey>> OnAfterUpsert(T entity);
        Task<ActionResult<T, TKey>> OnBeforeDelete(T entity);
        Task<ActionResult<T, TKey>> OnAfterDelete(T entity);
        Task<ActionResult<T, TKey>> OnBeforeRead(T entity);
        Task<ActionResult<T, TKey>> OnAfterRead(T entity);
    }

    public interface IBusinessController<T> : IBusinessController<T, string> where T : IEntity<string>
    {

    }

    /*public interface IBusinessProvider<T, TKey> where T : IEntity<TKey>
    {
        IEnumerable<IBusinessController<T, TKey>> Business { get; }
    }

    public interface IBusinessProvider<T> : IBusinessControllerProvider<T, string> where T : IEntity<string>
    {
    }*/
    
    public class BusinessController<T, TKey> :EntityController<T,TKey>,  IBusinessController<T, TKey> where T : IEntity<TKey>
    {
        [Inject]
        public BusinessController(IRepository<T, TKey> repo) :base(repo)
        {

        }

        public override async Task<ActionResult<T, TKey>> Post(T entity)
        {
            await OnBeforeUpsert(entity);
            var result= await base.Post(entity);
            await OnAfterUpsert(entity);
            return result;
        }
        //public IPrincipal User { get; set; }

#pragma warning disable CS1998 // Cette méthode async n'a pas d'opérateur 'await' et elle s'exécutera de façon synchrone
        public virtual async Task<ActionResult<T, TKey>> OnAfterDelete(T entity)
        {
            return ActionResult<T, TKey>.Ok(entity);
        }

        public virtual async Task<ActionResult<T,TKey>> OnAfterRead(T entity)
        {
            return ActionResult<T, TKey>.Ok(entity);
        }

        public virtual async Task<ActionResult<T, TKey>> OnAfterUpsert(T entity)
        {
            return ActionResult<T, TKey>.Ok(entity);
        }

        public virtual async Task<ActionResult<T, TKey>> OnBeforeDelete(T entity)
        {
            return ActionResult<T, TKey>.Ok(entity);
        }

        public virtual async Task<ActionResult<T, TKey>> OnBeforeRead(T entity)
        {
            return ActionResult<T, TKey>.Ok(entity);
        }

        public virtual async Task<ActionResult<T, TKey>> OnBeforeUpsert(T entity)
        {
            if (entity.Id == null)
            {
                entity.CreatedBy = User!=null?( User.Identity.IsAuthenticated ? User?.Identity?.Name : "UNAUTH"):"UNAUTH";
                entity.CreatedOn = DateTime.Now;
            }
            else
            {
                entity.ModifiedBy = User != null ? (User.Identity.IsAuthenticated ? User?.Identity?.Name : "UNAUTH") : "UNAUTH";
                entity.ModifiedOn = DateTime.Now;
            }
            
            return ActionResult<T, TKey>.Ok(entity);
        }
#pragma warning restore CS1998 // Cette méthode async n'a pas d'opérateur 'await' et elle s'exécutera de façon synchrone

    }

    public class BusinessController<T> : BusinessController<T, string>, IBusinessController<T> where T : IEntity<string>
    {
        [Inject]
        public BusinessController(IRepository<T> repo) : base(repo)
        {

        }
        
    }
}
