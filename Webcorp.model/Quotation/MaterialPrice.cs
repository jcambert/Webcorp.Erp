using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Webcorp.unite;

namespace Webcorp.Model.Quotation
{
    public class MaterialPrice : Entity
    {
       
        [KeyProvider]
        public string MaterialNumber { get; set; }

        public string Remark { get; set; }

        public MassCurrency Cout { get; set; }
    }
}
