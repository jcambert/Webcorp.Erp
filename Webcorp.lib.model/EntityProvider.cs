using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Webcorp.Model
{
    public sealed class EntityProvider<T,TKey> : IEntityProvider<T,TKey>, IInitializable where T : Entity
    {

        private readonly Dictionary<TKey, T> cache = new Dictionary<TKey, T>();
        
        private List<PropertyInfo> PropertyKeys { get; set; }


        public EntityProvider()
        {

        }

        [Inject]
        public EntityProvider(IEntityProviderInitializable<T,TKey> init)
        {
            this.Initializer = init;
        }
        public void Register(T entity)
        {
            entity.ThrowIfNull<ArgumentNullException>("entity cannot be null");

            if ( typeof(TKey) == typeof(string))
            {
                List<string> klvalue = new List<string>();
                foreach (var key in PropertyKeys)
                {
                    var tmp = key.GetValue(entity);
                    tmp.ThrowIfNull<ArgumentNullException>("Key value for provided entity cannot be null Property:"+key.Name);
                    klvalue.Add(tmp.ToString());
                }
                TKey kklvalue =(TKey) Convert.ChangeType(string.Join("_", klvalue),typeof(TKey)) ;
                cache[kklvalue] = entity;
            }
            else
            {
                foreach (var key in PropertyKeys)
                {
                    if (key.PropertyType == typeof(TKey)) {
                        var kvalue = (TKey)key.GetValue(entity);
                        kvalue.ThrowIfNull<ArgumentNullException>("Key value for provided entity cannot be null property:"+key.Name);
                        cache[kvalue] = entity;
                        break;
                    }
                }
            }

        }

        public List<T> Entities => cache.Values.ToList();


        public T this[TKey key] => cache[key];

        public T Find(params string[] keys)
        {
            (typeof(TKey) != typeof(string)).ThrowIf<ArgumentException>("Find method works only with key as string");            string kvalue = string.Join("_", keys.ToList());
            return this[ (TKey) Convert.ChangeType( kvalue,typeof(TKey)) ] ;
        }

        public void Initialize()
        {
            PropertyKeys = typeof(T).GetPropertiesSortedByFieldOrder<KeyProviderAttribute>();
            PropertyKeys.ThrowIfNull<ArgumentException>("a provided entity must have one KeyProviderAttribute");
            if(Initializer!=null) Initializer.InitializeProvider(this);
        }

        public T Find(TKey key)
        {
            return this[key];
        }

        public List<TKey> Keys => cache.Keys.ToList();

        public IEntityProviderInitializable<T, TKey> Initializer { get; private set; }
    }
}
