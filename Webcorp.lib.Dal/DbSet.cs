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
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Attributes;
using Ninject;
using System.Linq.Expressions;
using System.Threading;

namespace Webcorp.Dal
{

    public class EntityEntry : ReactiveObject
    {
        private readonly ReactiveList<KeyValue> _propertiesChanged = new ReactiveList<KeyValue>();
        private readonly Dictionary<string, PropertyInfo> _propertiesCache = new Dictionary<string, PropertyInfo>();
        private Entity _original;
        internal EntityEntry(object entity)
        {
            this.Entity = entity as Entity;
            //this._original = this.Entity.Clone();

            if (Entity.Id.IsNullOrEmpty())
                _state = EntityState.Added;
            else if (Entity.IsChanged)
                _state = EntityState.Modified;
            else
                _state = EntityState.Unchanged;

            _propertiesChanged.ItemsAdded.Subscribe(i =>
            {
                if (!_propertiesCache.ContainsKey(i.Key))
                {
                    _propertiesCache[i.Key] = this.Entity.GetType().GetProperty(i.Key);
                }
                var v = _propertiesCache[i.Key].GetValue(entity);
                i.Value = v;
            });

            this.WhenAnyValue(x => x.State).Where(p => p == EntityState.Unchanged).Throttle(TimeSpan.FromMilliseconds(100), RxApp.MainThreadScheduler).Subscribe(p => { Entity.IsChanged = false; _propertiesChanged.Clear(); });
        }

        public Entity Entity { get; private set; }

        public void RestoreOriginal()
        {
            Entity = _original;
            _propertiesCache.Clear();
            _propertiesChanged.Clear();
        }

        public ReactiveList<KeyValue> PropertiesChanged => _propertiesChanged;


        EntityState _state;
        public virtual EntityState State
        {
            get { return _state; }
            set
            {
                this.RaiseAndSetIfChanged(ref _state, value);
            }
        }

        internal bool MarkDeleted { get; set; }



    }
    public class KeyValue
    {
        public KeyValue(string key)
        {
            this.Key = key;
        }

        public string Key { get; private set; }
        public object Value { get; set; }
    }
    public class Entries : IDictionary<string, EntityEntry>
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
                        if (value.State != EntityState.Added)
                        {
                            value.State = EntityState.Modified;
                            value.PropertiesChanged.Add(new KeyValue(p.PropertyName));
                        }

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

        public void RemoveEntryMarkedAsDeleted()
        {
            foreach (var item in _inner.Where(v => v.Value.MarkDeleted).ToList())
            {
                _inner.Remove(item.Key);
            }
        }

    }

    public abstract class DbSet : IDbSet
    {

        public Entries Entries { get; private set; } = new Entries();


        public virtual void Dispose()
        {
        }

#pragma warning disable CS1998 // Cette méthode async n'a pas d'opérateur 'await' et elle s'exécutera de façon synchrone
        public virtual async Task<int> SaveChanges(CancellationToken token = default(CancellationToken))
#pragma warning restore CS1998 // Cette méthode async n'a pas d'opérateur 'await' et elle s'exécutera de façon synchrone
        {
            return 0;
        }
    }

    public class DbSet<TEntity> : DbSet, IDbSet<TEntity> where TEntity : Entity
    {
        //private readonly Dictionary<string, EntityEntry> _entries = new Dictionary<string, EntityEntry>();
        //private Entries _entries = new Entries();
        private readonly IIdGenerator _idgen;
        public DbSet()
        {
            var tmp = typeof(TEntity).GetProperty("Id").GetCustomAttributes(false);
            var attr = tmp.Where(a => a is BsonIdAttribute).FirstOrDefault() as BsonIdAttribute;
            var gentype = attr.IdGenerator;
            _idgen = Activator.CreateInstance(gentype) as IIdGenerator;
        }

        [Inject]
        public IRepository<TEntity> Repository { get; set; }

        public virtual async Task<int> SaveChanges(TEntity entity, CancellationToken token = default(CancellationToken))
        {
            if (token.IsCancellationRequested) return 0;
            var entry = Entry(entity);
            if (entry == null) return 0;
            bool res = false;
            switch (entry.State)
            {
                case EntityState.Detached:
                    break;
                case EntityState.Unchanged:
                    break;
                case EntityState.Deleted:
                    res = await Repository.Delete(entry.Entity.Id);
                    if (res) entry.MarkDeleted = true;
                    Entries.Remove(entity.Id);
                    break;
                case EntityState.Modified:
                    res = await Repository.Update((TEntity)entry.Entity, entry.PropertiesChanged);
                    if (res) entry.State = EntityState.Unchanged;
                    break;
                case EntityState.Added:
                    res = await Repository.Add((TEntity)entry.Entity);
                    if (res) entry.State = EntityState.Unchanged;
                    break;
                default:
                    break;
            }
            return res ? 1 : 0;
        }

        public override async Task<int> SaveChanges(CancellationToken token = default(CancellationToken))
        {
            int result = 0;
            foreach (var entry in Entries.Values.Where(v => v.State == EntityState.Added))
            {
                if (token.IsCancellationRequested) goto cancelled;
                var res = await Repository.Add((TEntity)entry.Entity);
                if (res)
                    entry.State = EntityState.Unchanged;
                //result += await entry.SaveChanges();
            }

            foreach (var entry in Entries.Values.Where(v => v.State == EntityState.Modified))
            {
                if (token.IsCancellationRequested) goto cancelled;
                var res = await Repository.Update((TEntity)entry.Entity, entry.PropertiesChanged);
                if (res)
                    entry.State = EntityState.Unchanged;
                //result += await entry.SaveChanges();
            }


            foreach (var entry in Entries.Values.Where(v => v.State == EntityState.Deleted))
            {
                if (token.IsCancellationRequested) goto cancelled;
                var res = await Repository.Delete(entry.Entity.Id);
                if (res)
                    entry.MarkDeleted = true;
                //result += await entry.SaveChanges();
            }
            cancelled:
            Entries.RemoveEntryMarkedAsDeleted();

            return result;
        }

        public TEntity Attach(TEntity entity)
        {
            Upsert(entity);

            return entity;
        }


        public TEntity Remove(TEntity entity)
        {
            var entry = Entries[entity.Id];
            if (entry.State == EntityState.Added)
                entry.MarkDeleted = true;
            else
                entry.State = EntityState.Deleted;
            return entity;
        }



        public EntityEntry Upsert(TEntity entity)
        {

            var result = new EntityEntry(entity);
            Entries[entity.Id ?? GenerateId(entity)] = result;
            return result;
        }

        public string GenerateId(TEntity entity)
        {
            return _idgen.GenerateId(null, entity) as string;
        }

        public EntityEntry Entry(TEntity entity)
        {
            EntityEntry result = null;
            Entries.TryGetValue(entity.Id ?? GenerateId(entity), out result);
            return result;
        }
    }
}
