using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GAF;
using GAF.Extensions;
using System.Collections.Generic;
using GAF.Operators;
using System.Diagnostics;
using System.Text;
using System.Linq;
namespace Webcorp.oned.tests
{
    [TestClass]
    public class UnitTest1
    {
        int[] stocks = new int[] { 10, 10, 10 };
        int[] itemsCount = new int[] { 3, 5, 1 };
        int[] itemsLength = new int[] { 3, 2, 5 };
        [TestMethod]
        public void TestGAF()
        {


            var population = new Population();

            int popSize = 100;
            for (int i = 0; i < popSize; i++)
            {
                var tmpStock = new int[stocks.Length];
                var tmpItemsCount = new int[itemsCount.Length];
                stocks.CopyTo(tmpStock, 0);
                itemsLength.CopyTo(tmpItemsCount, 0);

                Debug.WriteLine("************************************");
                Debug.WriteLine("Create new Chromosome");
                var chromosome = new Chromosome();
                for (int k = 0; k < stocks.Length; k++)
                {

                    Gene gene;
                    if (Rnd(k, ref tmpItemsCount, out gene))
                        chromosome.Genes.Add(gene);
                }
                chromosome.Genes.ShuffleFast();
                population.Solutions.Add(chromosome);


            }

            var elite = new Elite(10);
            var crossover = new Crossover(0.8) { CrossoverType = CrossoverType.SinglePoint };
            var mutate = new SwapMutate(0.02);
            var ga = new GeneticAlgorithm(population, CalculateFitness);
            ga.OnGenerationComplete += Ga_OnGenerationComplete;
            ga.OnRunComplete += Ga_OnRunComplete;
            ga.Operators.Add(elite);
            ga.Operators.Add(crossover);
            ga.Operators.Add(mutate);
            ga.Run(50);
        }

        private void Ga_OnRunComplete(object sender, GaEventArgs e)
        {
            var fittest = e.Population.GetTop(1)[0];
            for (int i = 0; i < fittest.Genes.Count; i++)
            {


                StringBuilder sb = new StringBuilder();
                int stock = stocks[i];
                int[] solitems = (int[])fittest.Genes[i].ObjectValue;
                int sum = solitems.Sum();

                Debug.WriteLine(string.Format("Stock {0} - Length {1} with {2}", i, stock, string.Join(",", solitems)));
            }
        }

        private void Ga_OnGenerationComplete(object sender, GaEventArgs e)
        {
            var fittest = e.Population.GetTop(1)[0];
            for (int i = 0; i < fittest.Genes.Count; i++)
            {


                StringBuilder sb = new StringBuilder();
                int stock = stocks[i];
                int[] solitems = (int[])fittest.Genes[i].ObjectValue;
                int sum = solitems.Sum();

                Debug.WriteLine(string.Format("Stock {0} - Length {1} with {2}", i, stock, string.Join(",", solitems)));
            }
        }

        private double CalculateFitness(Chromosome solution)
        {
            var result = 0.0;
            var wastedCount = 0;
            var waste = 0;
            var stockCount = 0;
            for (int i = 0; i < solution.Genes.Count; i++)
            {
                var itemLength = 0;
                var stock = stocks[i];
                stockCount += stock;
                int[] gene = (int[])solution.Genes[i].ObjectValue;
                for (int j = 0; j < gene.Length; j++)
                {
                    itemLength += (itemsLength[j] * gene[j]);
                   // waste += (stock - itemLength);
                    // if (Debugger.IsAttached && itemLength > stock) Debugger.Break();
                    //if (itemLength > stock) return 100;
                }
                waste = stockCount - itemLength;
            }
            if (stockCount == 0) return 100;
            return waste / stockCount;
        }
        public bool Terminate(Population population, int currentGeneration, long currentEvaluation)
        {
            Debug.WriteLine("Current Generartion:" + currentGeneration);
            return currentGeneration > 20;
        }
        private bool Rnd(int stockidx, ref int[] itemsCount, out Gene gene)
        {
            var r = new List<int>();
            int count = 0;
            Random random = new Random();
            for (int j = 0; j < itemsCount.Length; j++)
            {
                int result = 0;



                // var idx = random.Next(4);
                if (stockidx == stocks.Count()-1)
                {
                    result = itemsCount[j];
                }
                else
                    result = random.Next(itemsCount[j]);
                if (result * itemsLength[j] > stocks[stockidx])
                {
                    gene = new Gene();
                    return false;
                }

                count += result;
                if (count > stocks[stockidx])
                {
                    gene = new Gene();
                    return false;
                }

                
                itemsCount[j] -= result;

                r.Add(result);
            }
            Debug.WriteLine(string.Format("Create new Gene:{0}", string.Join(",", r)));
            gene = new Gene(r.ToArray());
            return true;
        }
        bool CheckHasItem(int[] itemsCount)
        {
            var r = 0;
            for (int i = 0; i < itemsCount.Length; i++)
            {
                r += itemsCount[i];
            }
            return r > 0;
        }
    }

    public static class ext
    {
        public static int Sum(this int[] i)
        {
            int result = 0;
            foreach (var item in i)
            {
                result += item;
            }
            return result;
        }
    }
}
