using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using Ninject;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Linq.Expressions;
using System.Reactive.Disposables;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Webcorp.common;
using Webcorp.Controller;
using Webcorp.Dal;
using Webcorp.Model;

namespace Webcorp.Business
{


    public interface IBusinessHelper<T> where T : Entity
    {

        T Create();

        Task<T> CreateAsync();
        
        void Attach(T entity);

        void OnChanging(T entity, string propertyName);

        void OnChanged(T entity, string propertyName);

        Task<int> Save();

        Task<int> Save(T entity);

        Task<int> DeleteAll();

        Task<T> GetById(string id);

        Task<IEnumerable<T>> Find(Expression<Func<T, bool>> predicate = null);

        Task<IEnumerable<T>> Find(FilterDefinition<T> filter);

        Task<bool> Delete(string id);

        Task<bool> Delete(T entity);

        Task<bool> Delete(Expression<Func<T, bool>> predicate = null);

        Task<long> Count(Expression<Func<T, bool>> predicate = null);

        Task<bool> Exists(Expression<Func<T, bool>> predicate);

        IAuthenticationService AuthService { get; set; }

        IDbContext DbContext { get; set; }


    }
    public class BusinessHelper<T> : IBusinessHelper<T> where T : Entity
    {
        //WeakList<T> attached = new WeakList<T>();
        private IIdGenerator _idgen;
        public BusinessHelper(IBusinessController<T> controller)
        {
            Contract.Requires(controller != null);
            this.Controller = controller;

            var tmp = typeof(T).GetProperty("Id").GetCustomAttributes(false);
            var attr = tmp.Where(a => a is BsonIdAttribute).FirstOrDefault() as BsonIdAttribute;
            var gentype = attr.IdGenerator;
            _idgen = Activator.CreateInstance(gentype) as IIdGenerator;
        }

        public string GenerateId(T entity)
        {
            Contract.Requires(entity != null);
            return _idgen.GenerateId(this.Controller.Repository.Collection, entity) as string;
        }


        [Inject]
        public IKernel Kernel { get; set; }

        [Inject]
        public IAuthenticationService AuthService { get; set; }

        public IBusinessController<T> Controller { get; private set; }

        [Inject]
        public IDbContext DbContext { get; set; }

        public virtual T Create()
        {
            Contract.Ensures(Contract.Result<T>() != null);
            T result = Kernel.Get<T>();
            result.IsChanged = true;
            // Attach(result);

            return result;
        }
        public virtual async Task<T> CreateAsync()
        {
            Task<T> t = Task.Factory.StartNew(() =>
            {
                T result = Create();

                return result;
            });

            return await t;

        }

        public virtual void Attach(T entity)
        {
            Contract.Requires(entity != null);
            if (IsAttached(entity)) return;
            entity.ShouldDispose(entity.Changing.Subscribe(
                _ =>
                {
                    entity.EnableEvents = false;
                    this.OnChanging(entity, _.PropertyName);
                    entity.EnableEvents = true;
                }));
            entity.ShouldDispose(entity.Changed.Subscribe(_ =>
            {
                entity.EnableEvents = false;
                this.OnChanged(entity, _.PropertyName);
                entity.EnableEvents = true;
            }));
            DbContext.Upsert(entity);
            // attached.Add(entity);
        }


        public virtual void OnChanging(T entity, string propertyName)
        {
            Contract.Requires(entity != null);
            Contract.Requires(propertyName != null);
        }

        public virtual void OnChanged(T entity, string propertyName)
        {
            Contract.Requires(entity != null);
            Contract.Requires(propertyName != null);
            entity.IsChanged = true;
        }

        public virtual async Task<int> Save(T entity)
        {
            Contract.Requires(entity != null);
            return await DbContext.SaveChangesAsync(entity);

        }
        public virtual async Task<int> Save()
        {
            return await DbContext.SaveChangesAsync();

        }

        public bool IsAttached(T entity)
        {
            Contract.Requires(entity != null);
            return DbContext.Entry(entity) != null;
        }


        public virtual async Task<int> DeleteAll()
        {

            Task<int> t = Task.Factory.StartNew(() =>
            {
                return DbContext.DeleteAll<T>();


            });

           return  await t;
        }

        public virtual async Task<T> GetById(string id)
        {
            Contract.Requires(id != null);
            var result= await DbContext.Repository<T>().GetById(id);
            Attach(result);
            return result;
        }

        public virtual async Task<IEnumerable<T>> Find(Expression<Func<T, bool>> predicate = null)
        {
            return await DbContext.Repository<T>().Find(predicate);
        }

        public virtual async Task<IEnumerable<T>> Find(FilterDefinition<T> filter)
        {
            return await DbContext.Repository<T>().Find(filter);
        }

        public virtual async Task<bool> Delete(string id)
        {
            return await DbContext.Repository<T>().Delete(id);
        }

        public virtual async Task<bool> Delete(T entity)
        {
            return await DbContext.Repository<T>().Delete(entity);
        }

        public virtual async Task<bool> Delete(Expression<Func<T, bool>> predicate = null)
        {
            return await DbContext.Repository<T>().Delete(predicate);
        }

        public virtual async Task<long> Count(Expression<Func<T, bool>> predicate = null)
        {
            return await DbContext.Repository<T>().Count();
        }

        public async Task<bool> Exists(Expression<Func<T, bool>> predicate)
        {
            return await DbContext.Repository<T>().Exists(predicate);
        }
    }

}
