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
    public class BinItem: CustomReactiveObject
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

    public class ItemList : ReactiveCollection<BinItem>
    {
        public ItemList():base()
        {
            Initiliaze();
        }

        public ItemList(IList<BinItem> list):base(list)
        {
            Initiliaze();
            UpdateSum();
        }
        private void Initiliaze()
        {
            this.Changed.Subscribe(_ => { UpdateSum(); });
            
        }

        private void UpdateSum()
        {
            _sum = 0;
            foreach (var item in Items.Where(i=>i.Size>0))
                _sum += item.Sum;
        }
        int _sum;
        public int Sum => _sum;

        public List<BinItem> Available
        {

            get
            {
                var result = new List<BinItem>();
                foreach (BinItem i in Items)
                {
                    for (int j = 1; j <= i.Pieces; j++)
                    {
                        if (i.Size > 0) result.Add(i);
                    }
                }
                return result;
            }
        }
    }
}
