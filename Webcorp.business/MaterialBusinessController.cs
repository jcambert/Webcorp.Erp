using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Webcorp.Controller;
using Webcorp.Model;
using Webcorp.unite;

namespace Webcorp.Business
{
    
    public class MaterialBusinessController :AbstractBusinessController<Material>
    {
        public override async Task<ActionResult<Material,string>> OnBeforeUpsert(Material entity)
        {
            entity.Density = 10*Density.KilogramPerCubicMetre;
            await base.OnBeforeUpsert(entity);
            //return new ActionResult<Material, string>(false, "Throw error for test", entity);
            return ActionResult<Material,string>.Ok(entity) ;
        }
        
    }
}
