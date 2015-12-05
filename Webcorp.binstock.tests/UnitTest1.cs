using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Webcorp.lin.binpack;
using System.Collections.Generic;

namespace Webcorp.binstock.tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestBinSolver()
        {
            List<BinItem> theItems = new List<BinItem>();
            List<BinStock> theStocks = new List<BinStock>();


            theItems.Add(new BinItem() {Size=258,Pieces=5 });
            theItems.Add(new BinItem() { Size = 1250, Pieces = 1 });

            theStocks.Add(new BinStock() { Size =700,MaxPieces=1,Cost=1,ConsiderMaxPieces=true});
            theStocks.Add(new BinStock() { Size =2000, MaxPieces = 2, Cost = 1, ConsiderMaxPieces = true });
            CuttingStock solver = new CuttingStock(theStocks, theItems);
            solver.Solve();

            var solutions = solver.Solutions;
        }
    }
}
