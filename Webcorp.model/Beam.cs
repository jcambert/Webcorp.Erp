using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Webcorp.Model.Article;
using Webcorp.unite;

namespace Webcorp.Model
{
    public class Beam : Webcorp.Model.Article.Article
    {
        public MassLinear MassLinear { get; set; }

        public AreaLinear AreaLinear { get; set; }

        public AreaMass AreaMass { get; set; }

        public MassCurrency MassCurrency { get; set; }

        public Currency CostLinear => MassLinear * MassCurrency;
    }
}
