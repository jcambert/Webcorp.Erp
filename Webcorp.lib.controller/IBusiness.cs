using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Webcorp.Model;

namespace Webcorp.Controller
{
    public interface IBusiness<T, TKey> where T : IEntity<TKey>
    {
        void OnChanging(T entity,string propertyName);
        void OnChanged(T entity,string propertyName);
    }

    public interface IBusiness<T> : IBusiness<T, string> where T : IEntity<string>
    {

    }

    public class Business<T, TKey> : IBusiness<T, TKey> where T : IEntity<TKey>
    {
        public virtual void OnChanged(T entity, string propertyName)
        {
            entity.IsChanged = true;
        }

        public virtual void OnChanging(T entity, string propertyName)
        {

        }
    }

    public class Business<T> : Business<T, string>, IBusiness<T, string> where T : IEntity<string>
    {

    }

    public interface IBusinessProvider<T, TKey> where T : IEntity<TKey>
    {
        IEnumerable<IBusiness<T, TKey>> Businesses { get; }
    }

    public interface IBusinessProvider<T> : IBusinessProvider<T, string> where T : IEntity<string>
    {
    }

    public interface IBusinessHelper<T> where T : Entity
    {
        T Create();

        void Attach(T entity);
    }
    public class BusinessHelper<T> : IBusinessHelper<T> where T : Entity
    {
        [Inject]
        public IKernel Kernel { get; set; }

        [Inject]
        public IBusinessProvider<T> BusinessProvider { get; set; }

        public T Create()
        {
            T result = Kernel.Get<T>();
            Attach(result);

            return result;
        }

        public void Attach(T entity)
        {
            entity.ShouldDispose(entity.Changing.Subscribe(_ => { BusinessProvider.Businesses.ForEach(b => b.OnChanging(entity,_.PropertyName));}));
            entity.ShouldDispose(entity.Changed.Subscribe(_ => { BusinessProvider.Businesses.ForEach(b => b.OnChanged(entity, _.PropertyName)); entity.IsChanged = true; }));
        }
    }
}
