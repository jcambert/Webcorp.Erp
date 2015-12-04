using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Webcorp.Model;
using ReactiveUI;
using Webcorp.reactive;
using System.Reactive.Linq;
using System.ComponentModel;

namespace Webcorp.lin.binpack
{
    [Serializable]
    public class Item: CustomReactiveObject
    {
        // This class build the Item to manage

        private int _size;
        public int Size
        {
            get { return _size; }
            set { this.RaiseAndSetIfChanged(ref _size, value); }
        }

        private int _pieces;
        public int Pieces
        {
            get { return _pieces; }
            set { this.RaiseAndSetIfChanged(ref _pieces, value); }
        }

        public int Sum => _pieces * _size;
    }

    public class ItemList : ReactiveCollection<Item>
    {
        public ItemList():base()
        {

        }

        public ItemList(IList<Item> list):base(list)
        {

        }
        private void Initiliaze()
        {
            var p=Observable.FromEventPattern<CollectionChangeEventArgs>(this, "CollectionChanged").ObserveOnDispatcher(System.Windows.Threading.DispatcherPriority.Normal);
            var t = p.Subscribe(_ => { UpdateSum(); });
            ShouldDispose(t);
        }

        private void UpdateSum()
        {
            _sum = 0;
            foreach (var item in Available)
                _sum += item.Sum;
        }
        int _sum;
        public int Sum => _sum;

        public List<Item> Available
        {
            get
            {
                return Items.Where(i => i.Size > 0).ToList();
            }
        }
    }
}
