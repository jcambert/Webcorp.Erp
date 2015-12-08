using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Webcorp.Model;

namespace Webcorp.lib.onedcut
{
    [DebuggerDisplay("StockIndex={StockIndex} ,StockLength={StockLength} ,CutLength={CutLength} , Waste={Waste}")]
    public class CutPlan:CustomReactiveObject
    {
        private List<CutBeam> _beams = new List<CutBeam>();
        private int _cutLength;

        public CutPlan(int stockIndex,int stockLength)
        {
            this.StockIndex = stockIndex;
            this.StockLength = stockLength;
        }

        public int StockIndex { get; private set; }
        public int StockLength { get; private set; }
        public int CutLength => _cutLength;
        public int Waste => StockLength - _cutLength;
        public List<CutBeam> Beams=> _beams;

        public bool AddCut(int index, int qty, int length)
        {
            if (qty == 0) return false;
            if (qty * length + _cutLength <= StockLength)
            {
                _cutLength += (qty * length);
                _beams.Add(new CutBeam() { Quantity = qty, Length = length });
                return true;
            }
            return false;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Stock Index:" + StockIndex);
            sb.AppendLine("Stock Length:" + StockLength);
            sb.AppendLine("Cutting Length:" + _cutLength);
            sb.AppendLine("Cutting details");
            sb.Append("\t");
            sb.AppendLine(string.Join("\n\t", _beams));
            sb.AppendLine("Waste:" + Waste);
            return sb.ToString();
        }
    }
}
