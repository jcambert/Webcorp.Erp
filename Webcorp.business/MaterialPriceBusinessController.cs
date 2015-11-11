using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Webcorp.Controller;
using Webcorp.Model.Quotation;
using Webcorp.unite;

namespace Webcorp.Business
{
   
    public class MaterialPriceBusinessController : AbstractBusinessController<MaterialPrice>
    {
        public override async Task<ActionResult<MaterialPrice,string>> OnBeforeUpsert(MaterialPrice entity)
        {
           // entity.PriceMass = Mass.Gram;
           return  await base.OnBeforeUpsert(entity);
        }
    }
}
