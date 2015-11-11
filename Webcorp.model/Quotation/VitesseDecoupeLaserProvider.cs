using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webcorp.Model.Quotation
{
    public static class VitesseDecoupeLaserExtensions
    {
        public static VitesseDecoupeLaser Find(this IEntityProvider<VitesseDecoupeLaser, string> entityProvider,int code,MaterialGroup matiere,double epaisseur,GazDecoupe gaz)
        {
            return entityProvider.Find(code.ToString(), matiere.ToString(), epaisseur.ToString(), gaz.ToString());
        }
    }
}
