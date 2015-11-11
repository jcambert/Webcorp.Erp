using System.ComponentModel;
using Webcorp.unite;
namespace Webcorp.Model.Quotation{
	public class VitesseDecoupeLaserInitializer : IEntityProviderInitializable<VitesseDecoupeLaser, string>{
		public void InitializeProvider(EntityProvider<VitesseDecoupeLaser, string> entityProvider)
        {
            
        
					 
			entityProvider.Register(new VitesseDecoupeLaser(){Code=215,GroupeMatiere=MaterialGroup.P,Epaisseur=1,Gaz=GazDecoupe.Oxygene,GrandeVitesse=8650*Velocity.MillimetrePerMinute,PetiteVitesse=7100*Velocity.MillimetrePerMinute});		
			entityProvider.Register(new VitesseDecoupeLaser(){Code=215,GroupeMatiere=MaterialGroup.P,Epaisseur=1.5,Gaz=GazDecoupe.Oxygene,GrandeVitesse=7400*Velocity.MillimetrePerMinute,PetiteVitesse=6100*Velocity.MillimetrePerMinute});		
		}
		
	}
}
