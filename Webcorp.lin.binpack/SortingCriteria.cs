using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webcorp.lin.binpack
{

    // Method for Non Increasing Sort of Stocks
    public class NonIncreasingSortOnStockSize : IComparer<BinStock>
    {
        public int Compare(BinStock x, BinStock y)
        {
            if (x.Size < y.Size) return 1;
            else if (x.Size > y.Size) return -1;
            else return 0;
        }
    }


    // Method for Non Decreasing Sort of Items
    public class NonDecreasingSortOnItemSize : IComparer<Item>
    {
        public int Compare(Item x, Item y)
        {
            if (x.Size > y.Size) return 1;
            else if (x.Size < y.Size) return -1;
            else return 0;
        }
    }


    // Method for Non Increasing Sort of Items
    public class NonIncreasingSortOnItemSize : IComparer<Item>
    {
        public int Compare(Item x, Item y)
        {
            if (x.Size < y.Size) return 1;
            else if (x.Size > y.Size) return -1;
            else return 0;
        }
    }


    // Method for Non Decreasing Sort on Size of the Branch & Bound List
    public class NonDecreasingSortOnBranchAndBoundSize : IComparer<BranchAndBound.BranchBound>
    {
        public int Compare(BranchAndBound.BranchBound x, BranchAndBound.BranchBound y)
        {
            if (x.Size > y.Size) return 1;
            else if (x.Size < y.Size) return -1;
            else return 0;
        }
    }


    // Method for Non Decreasing Sort on Cost of the Branch & Bound List
    public class NonDecreasingSortOnBranchAndBoundCost : IComparer<BranchAndBound.BranchBound>
    {
        public int Compare(BranchAndBound.BranchBound x, BranchAndBound.BranchBound y)
        {
            if (x.Cost > y.Cost) return 1;
            else if (x.Cost < y.Cost) return -1;
            else return 0;
        }
    }
}
