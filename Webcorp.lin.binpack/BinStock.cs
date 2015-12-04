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
    public class BinStock : CustomReactiveObject
    {
        float _Size;
        public float Size { get { return _Size; } set { this.RaiseAndSetIfChanged(ref _Size, value); } }

        float _Cost;
        public float Cost { get { return _Cost; } set { this.RaiseAndSetIfChanged(ref _Cost, value); } }


        int _MaxPieces;
        public int MaxPieces { get { return _MaxPieces; } set { this.RaiseAndSetIfChanged(ref _MaxPieces, value); } }


        bool _StockLimited;
        public bool ConsiderMaxPieces { get { return _StockLimited; } set { this.RaiseAndSetIfChanged(ref _StockLimited, value); } }

    }
}
