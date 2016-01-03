using System.ComponentModel;
using System.Collections.Generic;
using Webcorp.Model;
namespace Webcorp.Business{

	public class Pair{
		public string id,value;
	}
	public class ParametresInitializer {
		public List<Pair> StandardParametres()
        {
			var result=new List<Pair>();
			result.Add(new Pair(){id="001",value="999"});		
			result.Add(new Pair(){id="002",value="ATF Industrie"});		
			result.Add(new Pair(){id="003",value="SARL 30000€"});		
			result.Add(new Pair(){id="004",value="12345678"});		
			result.Add(new Pair(){id="005",value="Belfort"});		
			result.Add(new Pair(){id="006",value="0"});		
			return result;
		}
		
	}
}
