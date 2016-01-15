using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Webcorp.Model;
using Webcorp.unite;

namespace Webcorp.lib.onedcut
{
    [DebuggerDisplay("StockIndex={StockIndex} ,StockLength={StockLength} ,CutLength={CutLength} , Waste={Waste}")]
    public class CutPlan:CustomReactiveObject
    {
        private List<CutBeam> _beams = new List<CutBeam>();
        private int _cutLength,_cuttingWidth, _miniLength;
        private Article _beam;
        public CutPlan(int stockIndex,int stockLength,int cuttingWidth,int miniLength, Article beam)
        {
            this.StockIndex = stockIndex;
            this.StockLength = stockLength;
            this._cuttingWidth = cuttingWidth;
            this._miniLength = miniLength;
            this._beam = beam;
            
        }
        public int CutLengthWithCuttingLength => _cutLength + CutCount * _cuttingWidth;
        public int CutCount
        {
            get
            {
                int result = 0;
                Beams.ForEach(x => result += x.Quantity);
                return result;
            }
        }
        public int StockIndex { get; private set; }
        public int StockLength { get; private set; }
        public int CutLength => _cutLength;
        public int Waste => StockLength - CutLengthWithCuttingLength;
        public bool IsWaste => _cutLength > 0;
        public bool IsRealWaste
        {
            get
            {
             //   if (Debugger.IsAttached && Waste == 2450) Debugger.Break();
                return Waste < _miniLength /*&& IsWaste*/;
            }
        }

        public List<CutBeam> Beams=> _beams;

        public MassLinear MassLinear => _beam.MassLinear;

        public Mass TotalCutMass => MassLinear * CutLengthWithCuttingLength;

        public Currency TotalCutCost => _beam.CostLinear* CutLengthWithCuttingLength;

        public Currency TotalWasteCost => (IsWaste && IsRealWaste )? _beam.CostLinear * Waste:new Currency(0);

       

        public bool IsUncut => !IsWaste;

        public bool AddCut(int index, int qty, int length)
        {
            if (qty == 0) return false;
            if (qty * length + _cutLength +_cuttingWidth*(CutCount+ qty) <= StockLength)
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
            sb.AppendLine("Stock Index:" + (StockIndex+1));
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
