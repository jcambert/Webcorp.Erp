using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Webcorp.Model;
using ReactiveUI;
namespace Webcorp.lin.binpack
{
    [Serializable]
    public class Item: CustomReactiveObject
    {
        // This class build the Item to manage

        private float _size;
        public float Size
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

        
    }
}
