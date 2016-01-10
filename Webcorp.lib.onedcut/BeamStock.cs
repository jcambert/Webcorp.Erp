using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webcorp.lib.onedcut
{
    public class BeamStock
    {
        public BeamStock()
        {

        }

        public BeamStock(int length,int cost)
        {
            this.Length = length;
            this.Cost = cost;
        }

        public int Length { get; set; }
        public int Cost { get; set; }
    }
}
