using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Webcorp.Model;
using ReactiveUI;
using System.Reactive.Linq;
using System.Collections;
using System.Reflection;
using System.Reactive.Subjects;

namespace Webcorp.Dal
{

    public class EntityEntry : ReactiveObject
    {
        private readonly ReactiveList<string> _propertiesChanged = new ReactiveList<string>();
        private readonly Dictionary<string, PropertyInfo> _propertiesCache = new Dictionary<string, PropertyInfo>();
        private Entity _original;
        internal EntityEntry(object entity)
        {
            this.Entity = entity as Entity;
            this._original = this.Entity.Clone();
            if (Entity.IsChanged)
                _state = EntityState.Modified;
            else if (Entity.Id.IsNullOrEmpty())
                _state = EntityState.Added;
            else
                _state = EntityState.Unchanged;

            _propertiesChanged.ItemsAdded.Subscribe(i =>
            {
                if (!_propertiesCache.ContainsKey(i))
                {
                    _propertiesCache[i] = this.Entity.GetType().GetProperty(i);
                }
            });

            this.WhenAnyValue(x => x.State).Where(p => p == EntityState.Unchanged).Throttle(TimeSpan.FromMilliseconds(100), RxApp.MainThreadScheduler).Subscribe(p => { RestoreOriginal(); });
        }

        public Entity Entity { get; private set; }

        public void RestoreOriginal()
        {
            Entity = _original;
            _propertiesCache.Clear();
            _propertiesChanged.Clear();
        }

        public ReactiveList<string> PropertiesChanged => _propertiesChanged;


        EntityState _state;
        public virtual EntityState State
        {
            get { return _state; }
            set
            {
                this.RaiseAndSetIfChanged(ref _state, value);
            }
        }
    }

    internal class Entries : IDictionary<string, EntityEntry>
    {
        private readonly Dictionary<string, EntityEntry> _inner = new Dictionary<string, EntityEntry>();

        public EntityEntry this[string key]
        {
            get
            {
                return _inner[key];
            }

            set
            {

                if (!ContainsKey(key))
                {
                    var e = value.Entity;
                    e.ShouldDispose(e.Changed.Subscribe(p =>
                    {
                        e.IsChanged = true;
                        value.PropertiesChanged.Add(p.PropertyName);

                    }));
                }
                _inner[key] = value;
            }
        }

        public int Count => _inner.Count;

        public bool IsReadOnly => false;

        public ICollection<string> Keys => _inner.Keys;

        public ICollection<EntityEntry> Values => _inner.Values;

        public void Add(KeyValuePair<string, EntityEntry> item)
        {

        }

        public void Add(string key, EntityEntry value)
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            _inner.Clear();
        }

        public bool Contains(KeyValuePair<string, EntityEntry> item) => _inner.Contains(item);

        public bool ContainsKey(string key) => _inner.ContainsKey(key);

        public void CopyTo(KeyValuePair<string, EntityEntry>[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<KeyValuePair<string, EntityEntry>> GetEnumerator() => _inner.GetEnumerator();

        public bool Remove(KeyValuePair<string, EntityEntry> item) => _inner.Remove(item.Key);

        public bool Remove(string key) => _inner.Remove(key);

        public bool TryGetValue(string key, out EntityEntry value) => _inner.TryGetValue(key, out value);

        IEnumerator IEnumerable.GetEnumerator() => _inner.GetEnumerator();

    }

    public class DbSet<TEntity> : IDbSet<TEntity> where TEntity : Entity
    {
        //private readonly Dictionary<string, EntityEntry> _entries = new Dictionary<string, EntityEntry>();
        private Entries _entries = new Entries();
        public TEntity Attach(TEntity entity)
        {
            _entries[entity.Id] = new EntityEntry(entity);



            return entity;
        }

        public void Dispose()
        {

        }

        public TEntity Remove(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public EntityEntry Upsert(TEntity entity)
        {
            var result = new EntityEntry(entity);
            _entries[entity.Id] = result;
            return result;
        }
    }
}
