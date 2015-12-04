using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webcorp.lin.binpack
{
    public class CuttingStock
    {
        /*
         * This is the main class, wich provides the solver algorithms for the Bin Packing - Cutting Stock (BPCS) problem.      
         */

        // Status Label on the Form
        //private ToolStripLabel Label;

        // Status Progress Bar on the Form
        //private ToolStripProgressBar ProgressBar;

        #region Events
        public event EventHandler OnSolvingEnd = delegate { };
        #endregion

        #region GreedyNextFit fields

        // Check Box ExitGreedyNextFit on the Form
        //private CheckBox ExitGreedyNextFit;

        private int NumberOfPermutations = 1;
        private int myPermutationCounter;
        private bool HasFoundAbsoluteBestSolution = false;

        // Control the Call of GreedyNextFit procedure
        private bool searchAbsoluteBestSolution;

        // The shadow array that will be permuted 
        private int[] myLexicographicOrder;

        // The Absolute Best Solution
        private List<Bin> GreedyNextFitSolution = new List<Bin>();

        # endregion


        // Total Sum of the Items
        private float TotalItemsSum;

        // Collection of Stocks to employ
        private List<BinStock> ListOfStocks;

        // Collection of Items to obtain
        private List<Item> ListOfItems;

        // Collection of Solutions
        private Dictionary<string, List<Bin>> SetOfSolutions = new Dictionary<string, List<Bin>>();

        public CuttingStock(List<BinStock> theStocks, ItemList theItems/*, float theTotalItemsSum/*, ToolStripLabel theTextBoxStatus, ToolStripProgressBar theToolStripProgressBar, CheckBox theExitGreedyNextFit*/)
        {
            this.TotalItemsSum =theItems.Sum;

            //this.Label = theTextBoxStatus;
            //this.ProgressBar = theToolStripProgressBar;
           // this.ExitGreedyNextFit = theExitGreedyNextFit;

            // Get a List of Stocks available
            this.ListOfStocks = new List<BinStock>();
            foreach (BinStock s in theStocks)
            {
                if (s.Size > 0) this.ListOfStocks.Add(s);
            }

            // Get a List of Items available
            this.ListOfItems = new List<Item>();
            foreach (Item i in theItems)
            {
                for (int j = 1; j <= i.Pieces; j++)
                {
                    if (i.Size > 0) this.ListOfItems.Add(i);
                }
            }
        }

        public async Task SolveAsync()
        {
            await new TaskFactory().StartNew(
                Solve
                );
            return ;
        }

        public void Solve()
        {
            // This method drives the solver algorithms

            // The Bound Value for the Branch & Bound
            float BoundValue = 0;


            // Step 1: an "Improved Greedy First Fit" algorithm is executed and a solution will be found.
            // This solution will be an Upper Bound value for any further research.           
            if (CheckIfSolutionCanExist())
            {
                // Status Label
               // Label.Text = "Getting Bound solution";
                //Application.DoEvents();

                // Collection which is the "Greedy First Fit" solution
                List<Bin> BoundSolution = new List<Bin>();
                BoundSolution = GreedyFirstFit();

                // Store the solution in the Dictionary
                SetOfSolutions.Add("Bound Solution", BoundSolution);

                // Get the Bound Value for the next step
                BoundValue = GetSize("Bound Solution");
            }
            else
            {
                return;
            }



            /* Step 2: try to find a better solution exploring the Branch & Bound with a "Greedy Best Fit" algorithm.
             * This process can be repeated twice for searching the Best Cost and the Best Size solutions.
             */
            // Get a Branch & Bound List of improving solutions 
            BranchAndBound Branch = new BranchAndBound(ListOfStocks, ListOfItems, TotalItemsSum, BoundValue/*, Label*/);
            List<BranchAndBound.BranchBound> BranchList = new List<BranchAndBound.BranchBound>();
            BranchList = Branch.GetBranchAndBound();

            if (BranchList.Count == 0)
            {
                // The Branch & Bound is empty. The algorithm stops here and displays only the Bound Solution
                //PrintSolution(0, 0, SetOfSolutions);
                return;
            }

            // Sort the BranchList Non Decreasing on Cost and pass it to the "Greedy Best Fit" algorithm 
            // only if at least one cost has been inserted
            NonDecreasingSortOnBranchAndBoundCost soc = new NonDecreasingSortOnBranchAndBoundCost();
            BranchList.Sort(soc);
            float BestCost = BranchList[0].Cost;
            if (BestCost > 0)
            {
                // Status Label
                //Label.Text = "Searching the Best solution on Cost";
                //Application.DoEvents();
                SetOfSolutions.Add("Best Cost Solution", GreedyBestFit(BranchList));
            }

            // Sort the BranchList Non Decreasing on Size and pass it to the "Greedy Best Fit" algorithm
            NonDecreasingSortOnBranchAndBoundSize sos = new NonDecreasingSortOnBranchAndBoundSize();
            BranchList.Sort(sos);

            // Status Label
            //Label.Text = "Searching the Best solution on Size";
           // Application.DoEvents();

            // Store the Best Size solution in the Dictionary, even if it's null
            SetOfSolutions.Add("Best Size Solution", GreedyBestFit(BranchList));
            float AbsoluteBestSize = BranchList[0].Size;



            /* Step 3: now the search is made only to achieve the Absolute Best Size with a "Greedy Next Fit" algorithm 
             * applied to the branches with size equal to AbsoluteBestSize.
             * If "Best Size Solution" is also "Absolute Best", or if there are too much items to process (12), than this step
             * is not executed and the results are displayed.      
             */
            // Notice that GetSize("Best Size Solution") return 0 if the Best Size Solution doesn't exist
            float CandidateBestSize = GetSize("Best Size Solution");

            if (CandidateBestSize > 0 && AbsoluteBestSize >= CandidateBestSize || ListOfItems.Count > 12)
            {
                //PrintSolution(AbsoluteBestSize, BestCost, SetOfSolutions);
                return;
            }

            // Cut all the now useless branches in the BranchList
            BranchList.RemoveAll(branch => branch.Size > AbsoluteBestSize);

            for (int i = 1; i <= ListOfItems.Count; ++i)
            {
                NumberOfPermutations *= i;
            }

            //ExitGreedyNextFit.Visible = true;
            //ExitGreedyNextFit.Focus();
            //ProgressBar.Value = 0;
            int branchCounter = 0;

            // Execute the Greedy Next Fit with the hope to find the Absolute Best Size Solution
            foreach (BranchAndBound.BranchBound bb in BranchList)
            {
                ++branchCounter;
                //ProgressBar.Value = 100 * branchCounter / BranchList.Count;
                //Application.DoEvents();

                GreedyNextFit(bb, branchCounter, BranchList.Count);
                if (HasFoundAbsoluteBestSolution)
                {
                    // Store the solution (only if it exists) in the Dictionary                  
                    SetOfSolutions.Add("Absolute Best Size Solution", GreedyNextFitSolution);
                    break;
                }
            }


            // Display results
            //PrintSolution(AbsoluteBestSize, BestCost, SetOfSolutions);
        }


        private bool CheckIfSolutionCanExist()
        {
            // Sort Non Increasing the ListOfStocks
            NonIncreasingSortOnStockSize nisStocks = new NonIncreasingSortOnStockSize();
            ListOfStocks.Sort(nisStocks);

            // Sort Non Increasing the ListOfItems
            NonIncreasingSortOnItemSize nisItems = new NonIncreasingSortOnItemSize();
            ListOfItems.Sort(nisItems);

            // Check if ItemSize max <= StockSize max          
            if (ListOfItems[0].Size > ListOfStocks[0].Size)
            {
               // MessageBox.Show("Stocks too small.\n\n The biggest Stock is " + ListOfStocks[0].Size + ", the biggest Item is " +
                //    ListOfItems[0].Size + ".\n\nA solution can't exist.", BPCSform.HeaderMsgBox, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }
            else
            {
                return true;
            }
        }


        #region GreedyFirstFit

        private List<Bin> GreedyFirstFit()
        {
            // Build the "Greedy First Fit" solution list 
            List<Bin> GreedyFirstFitSolution = new List<Bin>();

            // Add the first element of the solution list
            GreedyFirstFitSolution.Add(new Bin(ListOfStocks[0].Size, ListOfStocks[0].Cost));

            // The helper list which will store the 'FirstFit' Linq query
            List<Bin> queryList = new List<Bin>();

            // Build the solution
            foreach (Item myItem in ListOfItems)
            {
                // Check if the current Stock can accept the Item
                if (myItem.Size <= GreedyFirstFitSolution[GreedyFirstFitSolution.Count - 1].Reject)
                {
                    AssignItem(GreedyFirstFitSolution[GreedyFirstFitSolution.Count - 1], myItem);
                    continue;
                }

                // Check if one of the other Stocks can accept the Item
                var queryStock = from el in GreedyFirstFitSolution
                                 where el.Reject >= myItem.Size
                                 select el;

                queryList.Clear();
                queryList = queryStock.ToList<Bin>();
                if (queryList.Count != 0)
                {
                    // Assign the Item with "First Fit" criteria
                    AssignItem(queryList[0], myItem);
                }
                else
                {
                    // A new Stock is necessary
                    GreedyFirstFitSolution.Add(new Bin(ListOfStocks[0].Size, ListOfStocks[0].Cost));
                    AssignItem(GreedyFirstFitSolution[GreedyFirstFitSolution.Count - 1], myItem);
                    continue;
                }
            }

            // The GreedyFirstFitSolution has been built, now it will be checked the possibility of improvement
            DownSize(GreedyFirstFitSolution);
            QualifySolution(GreedyFirstFitSolution);
            DownSize(GreedyFirstFitSolution);

            return GreedyFirstFitSolution;
        }

        #endregion



        #region GreedyBestFit

        private List<Bin> GreedyBestFit(List<BranchAndBound.BranchBound> myBranchList)
        {
            // Build the "Greedy Best Fit" solution list 
            List<Bin> GreedyBestFitSolution = new List<Bin>();

            // The helper list which will store the 'BestFit' Linq query
            List<Bin> queryList = new List<Bin>();

            // Loop through the Branch & Bound List
            bool SolutionHasFound = false;

            foreach (BranchAndBound.BranchBound branch in myBranchList)
            {
                int ItemAssigned = 0;

                if (SolutionHasFound)
                {
                    break;
                }

                GreedyBestFitSolution.Clear();

                // k indexes the elements in the "branch.SetOfStocks" array
                int k = 0;

                // Add the first element of this solution list
                GreedyBestFitSolution.Add(new Bin(branch.SetOfStocks[k], branch.SetOfStocksCost[k]));

                // Try to build the solution
                foreach (Item myItem in ListOfItems)
                {
                    // Check if the current Stock can accept the Item
                    if (myItem.Size <= GreedyBestFitSolution[GreedyBestFitSolution.Count - 1].Reject)
                    {
                        AssignItem(GreedyBestFitSolution[GreedyBestFitSolution.Count - 1], myItem);
                        ++ItemAssigned;

                        // Check if the last Item has been assigned
                        if (ItemAssigned == ListOfItems.Count)
                        {
                            SolutionHasFound = true;
                        }
                        continue;
                    }

                    // Check if one of the other Stocks can accept the Item and find whose minimize its reject
                    var queryStock = from el in GreedyBestFitSolution
                                     where el.Reject >= myItem.Size
                                     orderby el.Reject
                                     select el;

                    queryList.Clear();
                    queryList = queryStock.ToList<Bin>();
                    if (queryList.Count != 0)
                    {
                        // Assign the Item with "Best Fit" criteria
                        AssignItem(queryList[0], myItem);
                        ++ItemAssigned;

                        // Check if the last Item has been assigned
                        if (ItemAssigned == ListOfItems.Count)
                        {
                            SolutionHasFound = true;
                        }

                    }
                    else
                    {
                        // A new Stocks is necessary   
                        ++k;

                        // Verify that a next stock exists and if it can accept the Item
                        if (k == branch.SetOfStocks.Length || myItem.Size > branch.SetOfStocks[k])
                        {
                            break;
                        }
                        else
                        {
                            GreedyBestFitSolution.Add(new Bin(branch.SetOfStocks[k], branch.SetOfStocksCost[k]));
                            AssignItem(GreedyBestFitSolution[GreedyBestFitSolution.Count - 1], myItem);
                            ++ItemAssigned;

                            // Check if the last Item has been assigned
                            if (ItemAssigned == ListOfItems.Count)
                            {
                                SolutionHasFound = true;
                            }
                            continue;
                        }
                    }
                }
            }

            if (SolutionHasFound)
            {
                QualifySolution(GreedyBestFitSolution);
                return GreedyBestFitSolution;
            }
            else
            {
                return null;
            }
        }

        #endregion



        #region GreedyNextFit

        private void GreedyNextFit(BranchAndBound.BranchBound branch, int mybranchCounter, int myBranchListCount)
        {
            // Sort Non Decreasing the Set Of Stocks
            Array.Sort(branch.SetOfStocks);

            // Sort Non Decreasing the ListOfItems
            NonDecreasingSortOnItemSize ndsItems = new NonDecreasingSortOnItemSize();
            ListOfItems.Sort(ndsItems);

            // If with this branch a solution could exist it must be ItemSize max <= StockSize max
            if (ListOfItems[ListOfItems.Count - 1].Size > branch.SetOfStocks[branch.SetOfStocks.Length - 1])
            {
                return;
            }

            InitializeLexicographicArray();

            InitializeSolution(branch);

            // Reset the counter of permutations tested
            myPermutationCounter = 1;

            searchAbsoluteBestSolution = true;
            while (searchAbsoluteBestSolution)
            {
                GreedyNextFitProcedure(branch, mybranchCounter, myBranchListCount);

                if (GreedyNextFitSolution == null)
                {
                    searchAbsoluteBestSolution = false;
                }
            }
            return;
        }


        private void InitializeSolution(BranchAndBound.BranchBound branch)
        {
            // Clear the GreedyNextFit solution list
            GreedyNextFitSolution.Clear();

            // Create a new GreedyNextFit solution list
            for (int i = 0; i < branch.SetOfStocks.Length; ++i)
            {
                GreedyNextFitSolution.Add(new Bin(branch.SetOfStocks[i], branch.SetOfStocksCost[i]));
            }
        }


        private void GreedyNextFitProcedure(BranchAndBound.BranchBound branch, int thebranchCounter, int theBranchListCount)
        {
            // k indexes the elements in GreedyNextFit solution list
            int k = 0;
            foreach (int j in myLexicographicOrder)
            {
                //Application.DoEvents();
                /*if (ExitGreedyNextFit.Checked)
                {
                    searchAbsoluteBestSolution = false;
                    return;
                }*/

                if (GreedyNextFitSolution[k].Reject >= ListOfItems[j].Size)
                {
                    // Assign the current Item to the current solution list element
                    AssignItem(GreedyNextFitSolution[k], ListOfItems[j]);
                    continue;
                }

                else if (k <= GreedyNextFitSolution.Count - 2 && GreedyNextFitSolution[k + 1].Reject >= ListOfItems[j].Size)
                {
                    // Select, if it exists, one more suitable Stock (e.g. it exists and still has enough reject)                                
                    ++k;
                    // Assign the current Item to the current (the NEXT) solution list element
                    AssignItem(GreedyNextFitSolution[k], ListOfItems[j]);
                    continue;
                }

                else if (NextPermutation())
                {
                    // The current permutation doesn't fit, so a new one must be processed
                    InitializeSolution(branch);
                    ++myPermutationCounter;
                    if (myPermutationCounter % 100000 == 0)
                    {
                        //Label.Text = "Searching the Absolute Best Solution: Branch " + thebranchCounter.ToString() + " of " +
                       //     theBranchListCount.ToString() +
                       //     ((double)myPermutationCounter / (double)NumberOfPermutations).ToString("  -  #0% ");
                    }
                    return;
                }

                else
                {
                    // All the permutations have been tested and no solution has been found: quit the GreedyNextFitProcedure
                    searchAbsoluteBestSolution = false;
                    return;
                }
            }

            // At this point a solution has been found
            QualifySolution(GreedyNextFitSolution);
            HasFoundAbsoluteBestSolution = true;
            searchAbsoluteBestSolution = false;
        }


        /*
         * These following methods manage the Dijkstra's Lexycographic permutation according to the algorithm
         * 
         * showed in  http://www.cut-the-knot.org/do_you_know/AllPerm.shtml. They were taken from 
         * 
         * http://www.codeproject.com/Articles/26050/Permutations-Combinations-and-Variations-using-C-G
         *                 
         */

        private void InitializeLexicographicArray()
        {
            // Construct the new shadow array of the ListOfItems
            myLexicographicOrder = new int[ListOfItems.Count];
            for (int i = 0; i < myLexicographicOrder.Length; ++i)
            {
                myLexicographicOrder[i] = i;
            }
        }

        private bool NextPermutation()
        {
            int i = myLexicographicOrder.Length - 1;
            while (myLexicographicOrder[i - 1] >= myLexicographicOrder[i])
            {
                --i;
                if (i == 0)
                {
                    return false;
                }
            }
            int j = myLexicographicOrder.Length;
            while (myLexicographicOrder[j - 1] <= myLexicographicOrder[i - 1])
            {
                --j;
            }
            Swap(i - 1, j - 1);
            ++i;
            j = myLexicographicOrder.Length;
            while (i < j)
            {
                Swap(i - 1, j - 1);
                ++i;
                --j;
            }
            return true;
        }

        private void Swap(int i, int j)
        {
            int tmp = myLexicographicOrder[i];
            myLexicographicOrder[i] = myLexicographicOrder[j];
            myLexicographicOrder[j] = tmp;
        }
        #endregion



        # region CommonMethods

        private void AssignItem(Bin myElement, Item myItemToAssign)
        {
            // Add a new item of the ListOfItems at the current element of the Solution List passed in
            // This method is also invoked by QualifySolution() to move an item from one Bin to another
            myElement.ItemsAssigned.Add(myItemToAssign);
            myElement.Employ += myItemToAssign.Size;
            myElement.Reject -= myItemToAssign.Size;
        }

        private void RemoveItem(Bin myElement, Item myItemToRemove)
        {
            // Remove an item in a source Bin of the Solution list. This method is invoked only by QualifySolution()
            myElement.Employ -= myItemToRemove.Size;
            myElement.Reject += myItemToRemove.Size;
            myElement.ItemsAssigned.Remove(myItemToRemove);
        }



        private void DownSize(List<Bin> mySolution)
        {
            // Makes an attempt to reduce the size of some Stock using a smaller one.
            // At this point the List Of Stocks is sorted Non Increasing, so that the first
            // Stock in the list is the biggest, and the last one is the smallest.
            foreach (Bin myElement in mySolution)
            {
                if (myElement.Reject > 0)
                {
                    for (int i = ListOfStocks.Count - 1; i >= 0; --i)
                    {
                        if (myElement.Employ <= ListOfStocks[i].Size)
                        {
                            myElement.Stock = ListOfStocks[i].Size;
                            myElement.Cost = ListOfStocks[i].Cost;
                            myElement.Reject = myElement.Stock - myElement.Employ;
                            break;
                        }
                    }
                }
            }
        }



        private void QualifySolution(List<Bin> mySolution)
        {
            /* 
             Makes an attempt to improve the quality of the solution, trying to free the Bins as much as possible in order to get a more 
             reusability of them in a next job process.
             This is what I mean with "more qualified" solution: given two solution with the same Sum of Stocks, the "more qualified" is
             the one which gives back Stocks still reusable because of the good reject. In other words, it's better to have less Stocks with 
             great reject than more Stocks with small reject, which will be less reusable.            
             The methods loops backward the Solution, from the last to the first bin, thus considering first the bins
             which probably are already less used than others.
             The algorithm executes an assignement with "Best Fit" criteria of some items in the solution while moving them.   
             */

            // The helper list which will store the 'BestFit' Linq query
            List<Bin> queryList = new List<Bin>();

            // The first Bin must not be processed (i > 0) because there is nothing before itself       
            for (int i = mySolution.Count - 1; i > 0; --i)
            {
                // Every current Bin is excluded from the Linq query
                mySolution[i].TheBinIsLINQable = false;

                for (int j = 0; j < mySolution[i].ItemsAssigned.Count; ++j)
                {
                    // Check if one of the others Bin (with lower index "i") can accept the Item and find whose minimize its reject
                    var queryStock = from el in mySolution
                                     where el.TheBinIsLINQable && el.Reject >= mySolution[i].ItemsAssigned[j].Size
                                     orderby el.Reject
                                     select el;

                    queryList.Clear();
                    queryList = queryStock.ToList<Bin>();

                    if (queryList.Count != 0)
                    {
                        // Move the Item and update the data of the bins source and destination and the j for-loop counter

                        // Destination Bin
                        AssignItem(queryList[0], mySolution[i].ItemsAssigned[j]);

                        // Source Bin
                        RemoveItem(mySolution[i], mySolution[i].ItemsAssigned[j]);

                        // j must be decreased because mySolution[i].ItemsAssigned list has been reduced
                        // by 1 element. If not so, j will index a no more existing element.
                        --j;
                    }
                }
            }

            // Now Check if some Bin has became empty (not used) and remove it from the list           
            mySolution.RemoveAll(bin => bin.ItemsAssigned.Count == 0);
        }





        private float GetSize(string theKey)
        {

            // Calculates the size of the solution associated with "thekey" Key
            // If the solution is null, than 0 is returned
            float size = 0;
            if (SetOfSolutions[theKey] != null)
            {
                foreach (Bin myBin in SetOfSolutions[theKey])
                {
                    size += myBin.Stock;
                }
            }
            return size;
        }


     /*   private void PrintSolution(float absoluteBestSize, float bestCost, Dictionary<string, List<Bin>> mySetOfSolutions)
        {
            // Status Label
            Label.Text = "Print solution";

            // Creates a Word applicatifor the print out
            Word.Application myWordApp = new Word.Application();

            StringBuilder sb = new StringBuilder();

            foreach (KeyValuePair<string, List<Bin>> kvp in mySetOfSolutions)
            {
                // Checks if the current "Value" in the dictionary contains a solution
                if (kvp.Value == null)
                {
                    continue;
                }

                sb.Clear();

                // Get solution type
                sb.AppendLine("SOLUTION:  " + kvp.Key);

                // Get the solution data
                float stockSum = 0;
                float employ = 0;
                float cost = 0;
                foreach (Bin bin in kvp.Value)
                {
                    stockSum += bin.Stock;
                    employ += bin.Employ;
                    cost += bin.Cost;
                }
                float reject = (float)Math.Round((stockSum - employ) / stockSum, 2);

                sb.AppendLine("Absolute Best Size:  " + absoluteBestSize.ToString("#,###.###") + "\t\tBest Cost:  " +
                    bestCost.ToString("#,##0.###") + "\nSum of Stocks used:  " + stockSum.ToString("#,###.###") +
                    " \t\tSum of Items:  " + TotalItemsSum.ToString("#,###.###") + " \tReject:  " + reject.ToString("#0.0##%")
                    + "\nCost of this solution:  " + cost.ToString("#,##0.###"));


                // Get the solution
                foreach (Bin bin in kvp.Value)
                {
                    sb.AppendLine();
                    sb.AppendLine("_______________________________________________________________________________");
                    sb.AppendLine("Stock  " + bin.Stock.ToString() + "  \tcost  " + bin.Cost.ToString("#,###.##") + "  \tused  "
                        + bin.Employ.ToString("#,###.##") + "  \treject  " + Math.Round(bin.Reject, 1).ToString());

                    foreach (Item item in bin.ItemsAssigned)
                    {
                        sb.Append("\t" + item.Size.ToString());
                    }
                    sb.AppendLine();
                }

                // Print out Word doc           
                myWordApp.Documents.Add();
                myWordApp.ActiveDocument.Words.First.InsertBefore(sb.ToString());
                myWordApp.Visible = true;
            }
        }*/

        # endregion
    }


}
