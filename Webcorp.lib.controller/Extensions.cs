using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Webcorp.Model;

namespace Webcorp.Controller
{
   public static  class Extensions
    {
        public static bool HasError<T>(this List<ActionResult<T, string>> results) where T : Entity
        {
            foreach (var item in results)
            {
                if (!item.Result) return true;
            }
            return false;
        }
        public static void ThrowIfHasError<T>(this List<ActionResult<T, string>> results,string message="")where T : Entity 
        {
            if (results.HasError())
                throw new ControllerException<T>(results,message);
        }
    }

    public class ControllerException<T> : Exception where T :Entity
    {
        private List<ActionResult<T, string>> results;

        public ControllerException(List<ActionResult<T, string>> results,string message=""):base("An error Occur "+message)
        {
            this.results = results;
        }

        public List<ActionResult<T, string>> Results { get; set; }
    }
}
