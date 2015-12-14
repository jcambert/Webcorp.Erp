using Webcorp.unite;
namespace Webcorp.Model{
	public class MaterialInitializer : IEntityProviderInitializable<Material, string>{
		public void InitializeProvider(EntityProvider<Material, string> entityProvider)
        {
			 
			entityProvider.Register(new Material(){Code="1.0035",Group=MaterialGroup.P,Density=7800*Density.KilogramPerCubicMetre, Symbol="S185",Correspondance=new string[]{"S185","A33"} });
			entityProvider.Register(new Material(){Code="1.0036",Group=MaterialGroup.P,Density=7800*Density.KilogramPerCubicMetre, Symbol="S235JRG1" });
			entityProvider.Register(new Material(){Code="1.0037",Group=MaterialGroup.P,Density=7800*Density.KilogramPerCubicMetre, Symbol="S235JR",Correspondance=new string[]{"S235JR","E24-2"} });
			entityProvider.Register(new Material(){Code="1.0038",Group=MaterialGroup.P,Density=7800*Density.KilogramPerCubicMetre, Symbol="S235JRG2" });
			entityProvider.Register(new Material(){Code="1.0044",Group=MaterialGroup.P,Density=7800*Density.KilogramPerCubicMetre, Symbol="S275JR" });
		}
	}
}
