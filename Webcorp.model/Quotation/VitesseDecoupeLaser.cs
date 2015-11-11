using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Webcorp.unite;

namespace Webcorp.Model.Quotation
{
    public class VitesseDecoupeLaser:Entity
    {
        [KeyProvider(0)]
        public int Code { get; set; }

        [KeyProvider(1)]
        public MaterialGroup GroupeMatiere { get; set; }

        [KeyProvider(2)]
        public double Epaisseur { get; set; }

        [KeyProvider(3)]
        public GazDecoupe Gaz { get; set; }

        public Velocity GrandeVitesse{ get; set; }

        public Velocity PetiteVitesse{ get; set; }


    }

    public enum GazDecoupe
    {
        Oxygene=0,
        Azote
    }
}
