using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Disposables;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Webcorp.common;
using Webcorp.Model;

namespace Webcorp.Controller
{
    

    public interface IBusinessHelper<T> where T : Entity
    {
        T Create();

        void Detach(T entity);

        void Attach(T entity);

        void OnChanging(T entity, string propertyName);

        void OnChanged(T entity, string propertyName);

        Task Save();
    }
    public class BusinessHelper<T> : IBusinessHelper<T> where T : Entity
    {
        WeakList<T> attached = new WeakList<T>();

        [Inject]
        public IKernel Kernel { get; set; }
        
        [Inject]
        public IBusinessController<T> Controller { get; set; }

        public T Create()
        {
            T result = Kernel.Get<T>();
            Attach(result);

            return result;
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

        public virtual async Task Save()
        {

            foreach (var item in attached.Where(r => r.IsChanged))
            {
                await Controller.Post(item);
            }
        }
        public bool IsAttached(T entity)
        {
            return attached.Contains(entity);
        }

    }

}
