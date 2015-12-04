using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Webcorp.Model;

namespace Webcorp.lin.binpack
{
    public class Bin:CustomReactiveObject
    {
        // This class stores the solution, that is a collection of Bins/Stocks in which each element contains a list of Items

        // This bool is used only to exclude the already processed Bin during the QualifySolution() method
        private bool theBinIsLINQable;
        public bool TheBinIsLINQable
        {
            get { return theBinIsLINQable; }
            set { theBinIsLINQable = value; }
        }

        private float stock;
        public float Stock
        {
            get { return stock; }
            set { stock = value; }
        }

        private float cost;
        public float Cost
        {
            get { return cost; }
            set { cost = value; }
        }

        private float employ;
        public float Employ
        {
            get { return employ; }
            set { employ = value; }
        }

        private float reject;
        public float Reject
        {
            get { return reject; }
            set { reject = value; }
        }

        private List<Item> itemsAssigned;
        public List<Item> ItemsAssigned
        {
            get { return itemsAssigned; }
            set { itemsAssigned = value; }
        }

        public Bin(float size, float cost)
        {
            this.TheBinIsLINQable = true;
            this.Stock = size;
            this.Cost = cost;
            this.Employ = 0;
            this.Reject = size;
            this.ItemsAssigned = new List<Item>();
        }
    }


}
