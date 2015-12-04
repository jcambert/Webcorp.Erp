using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Collections.Specialized;
using MongoDB.Bson.Serialization.Attributes;
using System.Reactive.Disposables;

namespace Webcorp.reactive
{

    [Serializable]
    public class ReactiveCollection<T> : ReactiveObject, IList<T>, ICollection<T>, IEnumerable<T>, IEnumerable, IList, ICollection, IReadOnlyList<T>, IReadOnlyCollection<T>, INotifyCollectionChanged,IDisposable
    {

        private readonly CompositeDisposable _disposables = new CompositeDisposable();

        public virtual event NotifyCollectionChangedEventHandler CollectionChanged = delegate { };
        private SimpleMonitor _monitor = new SimpleMonitor();
        private const string IndexerName = "Item[]";
        private const string CountString = "Count";
        IList<T> items;
        [NonSerialized]
        private Object _syncRoot;

        #region ctor
        public ReactiveCollection()
        {
            items = new List<T>();
        }

        public ReactiveCollection(IList<T> list)
        {
            list.ThrowIfNull<ArgumentNullException>("source list cannot be null");
            this.items = list;
        }
        #endregion

        #region protected Methods
        public IList<T> Items => items;

        protected virtual void SetItem(int index, T item)
        {
            CheckReentrancy();
            T originalItem = this[index];
            items.Insert(index, item);
            this.RaisePropertyChanged(IndexerName);
            this.RaiseCollectionChanged(NotifyCollectionChangedAction.Replace, originalItem, item, index);
        }


        protected virtual void InsertItem(int index, T item)
        {
            CheckReentrancy();
            items.Insert(index, item);
            this.RaisePropertyChanged(CountString);
            this.RaisePropertyChanged(IndexerName);

            this.RaiseCollectionChanged(NotifyCollectionChangedAction.Add, item, index);
        }

        protected virtual void ClearItems()
        {
            CheckReentrancy();
            items.Clear();
            this.RaisePropertyChanged(CountString);
            this.RaisePropertyChanged(IndexerName);
            this.RaiseCollectionReset();

        }

        protected virtual void RemoveItem(int index)
        {
            CheckReentrancy();
            T removedItem = this[index];

            items.RemoveAt(index);

            this.RaisePropertyChanged(CountString);
            this.RaisePropertyChanged(IndexerName);
            this.RaiseCollectionChanged(NotifyCollectionChangedAction.Remove, removedItem, index);

        }

        protected virtual void RaiseCollectionChanged(NotifyCollectionChangedEventArgs e)
        {
            if (CollectionChanged != null)
            {
                using (BlockReentrancy())
                {
                    CollectionChanged(this, e);
                }
            }
        }

        protected IDisposable BlockReentrancy()
        {
            _monitor.Enter();
            return _monitor;
        }

        protected void CheckReentrancy()
        {
            if (_monitor.Busy)
            {
                // we can allow changes if there's only one listener - the problem
                // only arises if reentrant changes make the original event args 
                // invalid for later listeners.  This keeps existing code working 
                // (e.g. Selector.SelectedItems).
                if ((CollectionChanged != null) && (CollectionChanged.GetInvocationList().Length > 1))
                    throw new InvalidOperationException("Cannot reentrant");
            }
        }

        protected void ShouldDispose(IDisposable disposable)
        {
            _disposables.Add(disposable);
        }
        #endregion

        #region private Methods
        private void RaiseCollectionChanged(NotifyCollectionChangedAction action, T item, int index)
        {
            RaiseCollectionChanged(new NotifyCollectionChangedEventArgs(action, item, index));
        }
        private void RaiseCollectionChanged(NotifyCollectionChangedAction action, T item, int index, int oldIndex)
        {
            RaiseCollectionChanged(new NotifyCollectionChangedEventArgs(action, item, index, oldIndex));
        }

        private void RaiseCollectionChanged(NotifyCollectionChangedAction action, T oldItem, T newItem, int index)
        {
            RaiseCollectionChanged(new NotifyCollectionChangedEventArgs(action, newItem, oldItem, index));
        }
        private void RaiseCollectionReset()
        {
            RaiseCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        }

        [Serializable()]
        private class SimpleMonitor : IDisposable
        {
            public void Enter()
            {
                ++_busyCount;
            }

            public void Dispose()
            {
                --_busyCount;
            }

            public bool Busy { get { return _busyCount > 0; } }

            int _busyCount;
        }
        #endregion

        #region IList<T>
        public T this[int index]
        {
            get
            {
                try
                {
                    return items[index];
                }
                catch { return default(T); }
            }

            set
            {
                IsReadOnly.ThrowIf<NotSupportedException>("Collection is readdonly");
                (index.IsNotBound(0, items.Count - 1)).ThrowIf<ArgumentOutOfRangeException>("index is not in collection");
                SetItem(index, value);
            }
        }

        object IList.this[int index]
        {
            get { return this[index]; }
            set { this[index] = (T)value; }
        }

        public int Count => items.Count;

        public bool IsFixedSize
        {
            get
            {
                IList list = items as IList;
                if (list != null)
                {
                    return list.IsFixedSize;
                }
                return items.IsReadOnly;
            }
        }

        public bool IsReadOnly => items.IsReadOnly;

        public bool IsSynchronized => false;
        [BsonIgnore]
        public object SyncRoot
        {
            get
            {
                if (_syncRoot == null)
                {
                    ICollection c = items as ICollection;
                    if (c != null)
                    {
                        _syncRoot = c.SyncRoot;
                    }
                    else
                    {
                        System.Threading.Interlocked.CompareExchange<object>(ref _syncRoot, new Object(), null);
                    }
                }
                return _syncRoot;

            }
        }

        public int Add(object value)
        {
            Add((T)value);
            return items.IndexOf((T)value);
        }

        public void Add(T item)
        {
            IsReadOnly.ThrowIf<MulticastNotSupportedException>("Collection is readonly");
            InsertItem(items.Count, item);
        }

        public void Clear()
        {
            IsReadOnly.ThrowIf<MulticastNotSupportedException>("Collection is readonly");
            ClearItems();
        }

        public bool Contains(object value) => items.Contains((T)value);

        public bool Contains(T item) => items.Contains(item);

        public void CopyTo(Array array, int index)
        {
            Array.Copy(items.ToArray(), array, index);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            items.CopyTo(array, arrayIndex);
        }

        public IEnumerator<T> GetEnumerator() => items.GetEnumerator();

        public int IndexOf(object value) => items.IndexOf((T)value);

        public int IndexOf(T item) => items.IndexOf(item);

        public void Insert(int index, object value)
        {
            Insert(index, (T)value);
        }

        public void Insert(int index, T item)
        {
            IsReadOnly.ThrowIf<NotSupportedException>("Collection is readonly");
            index.IsNotBound(0, Count - 1).ThrowIf<ArgumentOutOfRangeException>("index is not in collection");
            InsertItem(index, item);
        }

        public void Remove(object value)
        {
            Remove((T)value);
        }

        public bool Remove(T item)
        {
            IsReadOnly.ThrowIf<NotSupportedException>("Collection is readonly");

            int index = items.IndexOf(item);
            if (index < 0) return false;
            RemoveItem(index);
            return true;
        }

        public void RemoveAt(int index)
        {
            IsReadOnly.ThrowIf<NotSupportedException>("Collection is readonly");
            index.IsNotBound(0, Count - 1).ThrowIf<ArgumentOutOfRangeException>("index is not in collection");

            RemoveItem(index);
        }

        IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable)items).GetEnumerator();


        #endregion

        #region Disposable
        public void Dispose()
        {
            _disposables.Dispose();
        }
        #endregion
    }

}
