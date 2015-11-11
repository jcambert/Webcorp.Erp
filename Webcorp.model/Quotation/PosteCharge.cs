using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Webcorp.unite;

namespace Webcorp.Model.Quotation
{
    public  class PosteCharge:Entity,IOperation
    {

        public PosteCharge()
        {
            
        }

        public string Section { get; set; }

        [KeyProvider]
        public int Code { get; set; }

        public string Designation { get; set; }

        public TauxHorraire TauxHorrairePrep { get; set; }

        public TauxHorraire TauxHorraireOperation { get; set; }

        public Time TempsPreparation { get; set; }

        public Time TempsBaseOp { get; set; }

        public string Aide { get; set; }

        public TypeDecoupe TypeDecoupe { get; set; }

       
    }

    public enum TypeDecoupe
    {
        NonApplicable=0,
        Laser,
        Poinconnage
    }
}
