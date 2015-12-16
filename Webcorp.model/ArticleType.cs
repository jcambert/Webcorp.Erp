using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webcorp.Model
{
    public enum ArticleType
    {
        [Description("Produit Fini")]
        ProduitFini,
        [Description("Produit Semi-Fini")]
        ProduitSemiFini,
        [Description("Matière Premiere")]
        MatièrePremiere,
        [Description("Libellé")]
        Libelle,
        [Description("Frais Generaux")]
        FraisGeneraux,
        [Description("Sous-Traitance")]
        SousTraitance

    }
}
