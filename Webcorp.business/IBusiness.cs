using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Attributes;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Disposables;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Webcorp.common;
using Webcorp.Controller;
using Webcorp.Model;

namespace Webcorp.Business
{


    public interface IBusinessHelper<T> where T : Entity
    {

        T Create();

        Task<T> CreateAsync();

        void Detach(T entity);

        void Attach(T entity);

        void OnChanging(T entity, string propertyName);

        void OnChanged(T entity, string propertyName);

        Task<List<ActionResult<T, string>>> Save();

        Task<ActionResult<T, string>> Save(T entity);

        IAuthenticationService AuthService { get; set; }
    }
    public class BusinessHelper<T> : IBusinessHelper<T> where T : Entity
    {
        WeakList<T> attached = new WeakList<T>();
        private IIdGenerator _idgen;
        public BusinessHelper(IBusinessController<T> controller)
        {
            this.Controller = controller;

            var tmp = typeof(T).GetProperty("Id").GetCustomAttributes(false);
            var attr = tmp.Where(a => a is BsonIdAttribute).FirstOrDefault() as BsonIdAttribute;
            var gentype = attr.IdGenerator;
            _idgen = Activator.CreateInstance(gentype) as IIdGenerator;
        }

        public string GenerateId( T entity)
        {
            return _idgen.GenerateId(this.Controller.Repository.Collection, entity) as string;
        }
        

        [Inject]
        public IKernel Kernel { get; set; }

        [Inject]
        public IAuthenticationService AuthService { get; set; }

        public IBusinessController<T> Controller { get; private set; }

        public virtual T Create()
        {
            T result = Kernel.Get<T>();
            result.IsChanged = true;
            Attach(result);

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
            attached.Add(entity);
        }

        public void Detach(T entity)
        {
            attached.Remove(entity);
        }

        public virtual void OnChanging(T entity, string propertyName)
        {

        }

        public virtual void OnChanged(T entity, string propertyName)
        {
            entity.IsChanged = true;
        }

        public virtual async Task<ActionResult<T,string>>Save(T entity)
        {
            return await Controller.Post(entity);
           
        }

        public virtual async Task<List<ActionResult<T, string>>> Save()
        {
            List<ActionResult<T, string>> results = new List<ActionResult<T, string>>();
            foreach (var item in attached.Where(r => r.IsChanged))
            {
                var result = await Controller.Post(item);
                results.Add(result);
            }
            results.ThrowIfHasError(" when saving. See Internal errors");
            return results;
        }
        public bool IsAttached(T entity)
        {
            return attached.Contains(entity);
        }

    }

}
