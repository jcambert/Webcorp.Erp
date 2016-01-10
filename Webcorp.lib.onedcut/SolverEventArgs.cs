using GAF;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Webcorp.unite;

namespace Webcorp.lib.onedcut
{
    public class SolverEventArgs:GaEventArgs
    {
        public SolverEventArgs(Population population,int generation,long evaluations,ReactiveList<BeamToCut> beams):base(population,generation,evaluations)
        {
            _totalCuttingMass = new Mass(0);
            _totalCuttingCost = new Currency(0);
            _totalWasteCost = new Currency(0);
            TotalToCut = 0;
            beams.ToList().ForEach(item => TotalToCut += item.TotalLength);
            foreach (var gene in Fittest.Genes)
            {
                var cutplan = (gene.ObjectValue as CutPlan);
                CutPlan.Add(cutplan);
                _totalWaste += cutplan.Waste == cutplan.StockLength ? 0 : cutplan.Waste;
                _totalStock += cutplan.Waste == cutplan.StockLength ? 0 : cutplan.StockLength;
                _totalCut += cutplan.CutLength;
                _totalUncut += cutplan.Waste == cutplan.StockLength ? cutplan.StockLength : 0;
                _totalCuttingMass += cutplan.TotalCutMass;
                _totalCuttingCost += cutplan.TotalCutCost;
                _totalWasteCost += cutplan.TotalWasteCost;
            }
            
        }

        public Chromosome Fittest => Population.GetTop(1)[0];

        int _totalWaste;
        public int TotalWaste => _totalWaste;

        int _totalStock;
        public int TotalStock => _totalStock;

        int _totalCut;
        public int TotalCut => _totalCut;

        int _totalUncut;
        public int TotalUncut => _totalUncut;

        public double WastePercentage => (_totalWaste * 1.0 / (_totalStock == 0 ? 1 : _totalStock));

        public int TotalToCut { get; private set; }

        public bool IsStockSuffisant => TotalToCut <= TotalCut;

        public int RestToCut => TotalToCut - TotalCut;

        public List<CutPlan> CutPlan { get; private set; } = new List<onedcut.CutPlan>();

        Mass _totalCuttingMass;
        public Mass TotalCuttingMass => _totalCuttingMass;

        Currency _totalCuttingCost;
        public Currency TotalCuttingCost => _totalCuttingCost;

        Currency _totalWasteCost;
        public Currency TotalWasteCost => _totalWasteCost;
    }
}
