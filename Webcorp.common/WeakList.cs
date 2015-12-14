using System;
using System.Collections.Generic;
using System.Linq;

namespace Webcorp.common
{

    public class WeakList<T> : IList<T> where T :class
    {
        private List<WeakReference> _innerList = new List<WeakReference>();

        #region IList<T> Members

        public int IndexOf(T item)
        {
            return _innerList.Select(wr => wr.Target).ToList().IndexOf(item);
        }

        public void Insert(int index, T item)
        {
            _innerList.Insert(index, new WeakReference(item));
        }

        public void RemoveAt(int index)
        {
            _innerList.RemoveAt(index);
        }

        public T this[int index]
        {
            get
            {
                return _innerList[index].Target as T; 
            }
            set
            {
                _innerList[index] = new WeakReference(value);
            }
        }

        #endregion

        #region ICollection<T> Members

        public void Add(T item)
        {
            _innerList.Add(new WeakReference(item));
        }

        public void Clear()
        {
            _innerList.Clear();
        }

        public bool Contains(T item)
        {
            Purge();
            return _innerList.Any(wr => object.Equals(wr.Target, item));
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            Purge();
            _innerList.Select(wr => wr.Target).ToArray().CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get { return _innerList.Count; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public bool Remove(T item)
        {
            int index = IndexOf(item);
            if (index > -1)
            {
                RemoveAt(index);
                return true;
            }
            return false;
        }

        #endregion

        #region IEnumerable<T> Members

        public IEnumerator<T> GetEnumerator()
        {
            Purge();
            return _innerList.Select(x => x.Target).Cast<T>().GetEnumerator();
        }

        #endregion

        #region IEnumerable Members

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        #endregion

        public void Purge()
        {
            _innerList.RemoveAll(wr => !wr.IsAlive);
        }
    }
}
