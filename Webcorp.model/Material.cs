using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Webcorp.unite;
using ReactiveUI;
namespace Webcorp.Model
{
    public class Material : Entity
    {
        [KeyProvider]
        public string Code { get; set; }

        public MaterialGroup Group { get; set; }

        public string Symbol { get; set; }

        public Density Density { get; set; }

        public string[] Correspondance { get; set; }
    }

    /// <summary>
    /// @see CMC Classification
    /// </summary>
    public enum MaterialGroup
    {
        P=0, //Acier=>0
        M, //Inox=>1
        K, //Fontes=>2
        N, //Non Ferreux Aluminium=>3
        S, //Superalliage, Réfractaire=>4
        H  //Aciers Trempés=>5
    }
}
