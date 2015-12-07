using ReactiveUI.Fody.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Webcorp.Model;
using ReactiveUI;
using Webcorp.reactive;

namespace Webcorp.lib.onedcut
{
    public class Item : CustomReactiveObject
    {
        [Reactive]
        public int Size { get; set; }
        [Reactive]
        public int Quantity { get; set; }

        public int Sum => Size * Quantity;
    }

    public class Stock : Item
    {
        [Reactive]
        public int Cost { get; set; }
    }


    public class OneDCollection<T>:ReactiveCollection<T>  where T : Item
    {
        public ReactiveList<T> Available => new ReactiveList<T>(this.Items.Where(p => p.Size > 0 && p.Quantity > 0));
    }


    public class ItemList : OneDCollection<Item>
    {

        

    }

    public class StockList:OneDCollection<Stock>
    {

    }


   /* public class Solver
    {
        private readonly ReactiveList<Stock> _stocks;
        private readonly ReactiveList<Item> _items;

        public Solver(StockList stocks, ItemList items)
        {
            this._stocks = stocks.Available;
            this._items = items.Available;
        }

        public void Solve()
        {
            if (_stocks.Count == 0 || _items.Count == 0) return;
            if (!CheckIfSolutionCanExist()) return;
        }

        private bool CheckIfSolutionCanExist()
        {
            _stocks.Sort(new SortBySizeDescendant<Stock>());

            _items.Sort(new SortBySizeDescendant<Item>());

            return _items[0].Size > _stocks[0].Size;
            
        }

        private void FirstSolution()
        {
            Solution solution = new Solution();
            solution.Stock =0;
           
        }

    }
*/
    public struct Solution
    {
        public int Item { get; set; }
        public int Stock { get; set; }

    }
}