using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webcorp.lin.binpack
{
    public class BranchAndBound
    {
        /*
         * This class generates the solution's space of research: the Branch & Bound data structure.
         * A status label shows information during the process.         
         */

        // StatusLabel on the Form
        private ToolStripLabel Label;

        // Stores the maximum number of branhces
        private float MaxBranch;

        // Stores the branches already evaluated
        private float EvaluatedBranchCounter;

        // The TotalItemsSum is used to fix the minimum dimension of the space of research
        private float TotalItemsSum;

        // The Bound value limits the space of research
        private float BoundValue;

        // This array is used to create a set of potential solution. It contains the Max Quantity needed for each Stocks Size  
        private int[] MaxStocksQuantity;

        // Collection of Stocks
        private List<BinStock> BranchListOfStocks;

        // Collection of Items
        private List<Item> BranchListOfItems;

        // The single "branch" data structure
        public struct BranchBound
        {
            public float Size;
            public float Cost;
            public float[] SetOfStocks;

            // Array parallel to SetOfStocks
            public float[] SetOfStocksCost;
        }

        // The important Branch & Bound data structure bilt up by this class
        private List<BranchBound> BranchBoundList;


        public BranchAndBound(List<BinStock> theStocks, List<Item> theItems, float theTotalItemsSum, float theBound,
            ToolStripLabel theToolStripLabel)
        {
            this.Label = theToolStripLabel;
            this.TotalItemsSum = theTotalItemsSum;
            this.BoundValue = theBound;

            BranchListOfStocks = new List<BinStock>();
            this.BranchListOfStocks = theStocks;

            BranchListOfItems = new List<Item>();
            this.BranchListOfItems = theItems;

            this.MaxStocksQuantity = new int[theStocks.Count];
            this.BranchBoundList = new List<BranchBound>();
        }


        public List<BranchBound> GetBranchAndBound()
        {
            // This is the driver method for all the operations in this class.
            // It returns a Branch & Bound set of potential solutions.
            GetMaxStocksQuantity();
            GetBranchBoundList();
            return BranchBoundList;
        }


        private void GetMaxStocksQuantity()
        {
            // Calculate the maximum number of each Stocks needed to reach the Bound and the maximum number of branches
            MaxBranch = 1;
            for (int i = 0; i < MaxStocksQuantity.Length; ++i)
            {
                int qmax = (int)Math.Ceiling(BoundValue / BranchListOfStocks[i].Size);
                if (BranchListOfStocks[i].ConsiderMaxPieces && qmax > BranchListOfStocks[i].MaxPieces)
                {
                    qmax = (int)BranchListOfStocks[i].MaxPieces;
                }
                MaxStocksQuantity[i] = qmax;
                MaxBranch *= qmax;
            }
        }


        private void GetBranchBoundList()
        {
            // A maximum number of Stock sizes is managed by this algorithm. This is a design choice based on the practice.

            float measure;

            try
            {
                // 1 Stock
                if (MaxStocksQuantity.Length == 1)
                {
                    for (int c1 = 0; c1 <= MaxStocksQuantity[0]; ++c1)
                    {
                        ++EvaluatedBranchCounter;
                        if (EvaluatedBranchCounter % 500 == 0)
                        {
                            UpdateStatus();
                        }

                        measure = c1 * BranchListOfStocks[0].Size;
                        if (measure >= TotalItemsSum && measure < BoundValue)
                        {
                            BranchBound bb = new BranchBound();
                            bb.Size = measure;
                            bb.Cost = c1 * BranchListOfStocks[0].Cost;
                            Array.Resize(ref bb.SetOfStocks, c1);
                            Array.Resize(ref bb.SetOfStocksCost, c1);
                            int[] coeff = new int[] { c1 };
                            AddItemToBranchBoundList(bb, coeff);
                        }
                    }
                }

                // 2 Stocks
                if (MaxStocksQuantity.Length == 2)
                {
                    for (int c1 = 0; c1 <= MaxStocksQuantity[0]; ++c1)
                    {
                        for (int c2 = 0; c2 <= MaxStocksQuantity[1]; ++c2)
                        {
                            ++EvaluatedBranchCounter;
                            if (EvaluatedBranchCounter % 500 == 0)
                            {
                                UpdateStatus();
                            }

                            measure = c1 * BranchListOfStocks[0].Size + c2 * BranchListOfStocks[1].Size;
                            if (measure >= TotalItemsSum && measure < BoundValue)
                            {
                                BranchBound bb = new BranchBound();
                                bb.Size = measure;
                                bb.Cost = c1 * BranchListOfStocks[0].Cost + c2 * BranchListOfStocks[1].Cost;
                                Array.Resize(ref bb.SetOfStocks, c1 + c2);
                                Array.Resize(ref bb.SetOfStocksCost, c1 + c2);
                                int[] coeff = new int[] { c1, c2 };
                                AddItemToBranchBoundList(bb, coeff);
                            }
                        }
                    }
                }

                // 3 Stocks
                if (MaxStocksQuantity.Length == 3)
                {
                    for (int c1 = 0; c1 <= MaxStocksQuantity[0]; ++c1)
                    {
                        for (int c2 = 0; c2 <= MaxStocksQuantity[1]; ++c2)
                        {
                            for (int c3 = 0; c3 <= MaxStocksQuantity[2]; ++c3)
                            {
                                ++EvaluatedBranchCounter;
                                if (EvaluatedBranchCounter % 500 == 0)
                                {
                                    UpdateStatus();
                                }

                                measure = c1 * BranchListOfStocks[0].Size + c2 * BranchListOfStocks[1].Size + c3 * BranchListOfStocks[2].Size;
                                if (measure >= TotalItemsSum && measure < BoundValue)
                                {
                                    BranchBound bb = new BranchBound();
                                    bb.Size = measure;
                                    bb.Cost = c1 * BranchListOfStocks[0].Cost + c2 * BranchListOfStocks[1].Cost + c3 * BranchListOfStocks[2].Cost;
                                    Array.Resize(ref bb.SetOfStocks, c1 + c2 + c3);
                                    Array.Resize(ref bb.SetOfStocksCost, c1 + c2 + c3);
                                    int[] coeff = new int[] { c1, c2, c3 };
                                    AddItemToBranchBoundList(bb, coeff);
                                }
                            }
                        }
                    }
                }

                // 4 Stocks
                if (MaxStocksQuantity.Length == 4)
                {
                    for (int c1 = 0; c1 <= MaxStocksQuantity[0]; ++c1)
                    {
                        for (int c2 = 0; c2 <= MaxStocksQuantity[1]; ++c2)
                        {
                            for (int c3 = 0; c3 <= MaxStocksQuantity[2]; ++c3)
                            {
                                for (int c4 = 0; c4 <= MaxStocksQuantity[3]; ++c4)
                                {
                                    ++EvaluatedBranchCounter;
                                    if (EvaluatedBranchCounter % 5000 == 0)
                                    {
                                        UpdateStatus();
                                    }

                                    measure = c1 * BranchListOfStocks[0].Size + c2 * BranchListOfStocks[1].Size + c3 * BranchListOfStocks[2].Size
                                   + c4 * BranchListOfStocks[3].Size;

                                    if (measure >= TotalItemsSum && measure < BoundValue)
                                    {
                                        BranchBound bb = new BranchBound();
                                        bb.Size = measure;
                                        bb.Cost = c1 * BranchListOfStocks[0].Cost + c2 * BranchListOfStocks[1].Cost + c3 * BranchListOfStocks[2].Cost
                                            + c4 * BranchListOfStocks[3].Cost;
                                        Array.Resize(ref bb.SetOfStocks, c1 + c2 + c3 + c4);
                                        Array.Resize(ref bb.SetOfStocksCost, c1 + c2 + c3 + c4);
                                        int[] coeff = new int[] { c1, c2, c3, c4 };
                                        AddItemToBranchBoundList(bb, coeff);
                                    }
                                }
                            }
                        }
                    }
                }

                // 5 Stocks
                if (MaxStocksQuantity.Length == 5)
                {
                    for (int c1 = 0; c1 <= MaxStocksQuantity[0]; ++c1)
                    {
                        for (int c2 = 0; c2 <= MaxStocksQuantity[1]; ++c2)
                        {
                            for (int c3 = 0; c3 <= MaxStocksQuantity[2]; ++c3)
                            {
                                for (int c4 = 0; c4 <= MaxStocksQuantity[3]; ++c4)
                                {
                                    for (int c5 = 0; c5 <= MaxStocksQuantity[4]; ++c5)
                                    {
                                        ++EvaluatedBranchCounter;
                                        if (BranchBoundList.Count % 5000 == 0)
                                        {
                                            UpdateStatus();
                                        }
                                        measure = c1 * BranchListOfStocks[0].Size + c2 * BranchListOfStocks[1].Size + c3 * BranchListOfStocks[2].Size
                                       + c4 * BranchListOfStocks[3].Size + c5 * BranchListOfStocks[4].Size;

                                        if (measure >= TotalItemsSum && measure < BoundValue)
                                        {
                                            BranchBound bb = new BranchBound();
                                            bb.Size = measure;
                                            bb.Cost = c1 * BranchListOfStocks[0].Cost + c2 * BranchListOfStocks[1].Cost +
                                                c3 * BranchListOfStocks[2].Cost + c4 * BranchListOfStocks[3].Cost +
                                                c5 * BranchListOfStocks[4].Cost;
                                            Array.Resize(ref bb.SetOfStocks, c1 + c2 + c3 + c4 + c5);
                                            Array.Resize(ref bb.SetOfStocksCost, c1 + c2 + c3 + c4 + c5);
                                            int[] coeff = new int[] { c1, c2, c3, c4, c5 };
                                            AddItemToBranchBoundList(bb, coeff);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                // 6 Stocks
                if (MaxStocksQuantity.Length == 6)
                {
                    for (int c1 = 0; c1 <= MaxStocksQuantity[0]; ++c1)
                    {
                        for (int c2 = 0; c2 <= MaxStocksQuantity[1]; ++c2)
                        {
                            for (int c3 = 0; c3 <= MaxStocksQuantity[2]; ++c3)
                            {
                                for (int c4 = 0; c4 <= MaxStocksQuantity[3]; ++c4)
                                {
                                    for (int c5 = 0; c5 <= MaxStocksQuantity[4]; ++c5)
                                    {
                                        for (int c6 = 0; c6 <= MaxStocksQuantity[5]; ++c6)
                                        {
                                            ++EvaluatedBranchCounter;
                                            if (BranchBoundList.Count % 10000 == 0)
                                            {
                                                UpdateStatus();
                                            }
                                            measure = c1 * BranchListOfStocks[0].Size + c2 * BranchListOfStocks[1].Size
                                                + c3 * BranchListOfStocks[2].Size + c4 * BranchListOfStocks[3].Size
                                                + c5 * BranchListOfStocks[4].Size + c6 * BranchListOfStocks[5].Size;

                                            if (measure >= TotalItemsSum && measure < BoundValue)
                                            {
                                                BranchBound bb = new BranchBound();
                                                bb.Size = measure;
                                                bb.Cost = c1 * BranchListOfStocks[0].Cost + c2 * BranchListOfStocks[1].Cost +
                                                    c3 * BranchListOfStocks[2].Cost + c4 * BranchListOfStocks[3].Cost +
                                                    c5 * BranchListOfStocks[4].Cost + c6 * BranchListOfStocks[5].Cost;
                                                Array.Resize(ref bb.SetOfStocks, c1 + c2 + c3 + c4 + c5 + c6);
                                                Array.Resize(ref bb.SetOfStocksCost, c1 + c2 + c3 + c4 + c5 + c6);
                                                int[] coeff = new int[] { c1, c2, c3, c4, c5, c6 };
                                                AddItemToBranchBoundList(bb, coeff);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            catch (OutOfMemoryException)
            {
                MessageBox.Show("Memory Full!\nThe application will be quitted.\nRetry with less Items or remove some Stocks.", BPCSform.HeaderMsgBox, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                Application.Exit();
                return;
            }
        }

        private void AddItemToBranchBoundList(BranchBound myB, params int[] myCoeff)
        {
            // Adds a new branch to the Branch & Bound List
            int j = 0;
            for (int k = 0; k < BranchListOfStocks.Count; ++k)
            {
                while (myCoeff[k] > 0)
                {
                    myB.SetOfStocks[j] = BranchListOfStocks[k].Size;
                    myB.SetOfStocksCost[j] = BranchListOfStocks[k].Cost;
                    --myCoeff[k];
                    ++j;
                }
            }
            BranchBoundList.Add(myB);
        }

        private void UpdateStatus()
        {
            // Shows information on the process
            Label.Text = "Branch and Bound. Evaluating branch " + EvaluatedBranchCounter.ToString("#,###") + " of " + MaxBranch.ToString("#,###");
            Application.DoEvents();
        }
    }

}
