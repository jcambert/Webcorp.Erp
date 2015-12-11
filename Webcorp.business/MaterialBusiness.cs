using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Webcorp.Controller;
using Webcorp.Model;

namespace Webcorp.Business
{
    [Business]
    public class MaterialBusiness:AbstractBusiness<Material>
    {
        public override void OnChanged(Material entity, string propertyName)
        {
            base.OnChanged(entity, propertyName);
            if (propertyName == "Density")
                entity.Density /= 2;
        }
    }
}
