using System.ComponentModel;
using Webcorp.unite;
namespace Webcorp.Model.Quotation{
	public class BeamInitializer : IEntityProviderInitializable<Beam, string>{
		public void InitializeProvider(EntityProvider<Beam, string> entityProvider)
        {
				 
			entityProvider.Register(new Beam(){Code="IPE 80",Libelle="IPE 80",MassLinear=MassLinear.Parse("6 kg/m"),AreaLinear= AreaLinear.Parse("0.328 m2/m"),AreaMass= AreaMass.Parse("54.64 m2/t")});		
			entityProvider.Register(new Beam(){Code="IPE 100",Libelle="IPE 100",MassLinear=MassLinear.Parse("8.1 kg/m"),AreaLinear= AreaLinear.Parse("0.4 m2/m"),AreaMass= AreaMass.Parse("49.33 m2/t")});		
			entityProvider.Register(new Beam(){Code="IPE 120",Libelle="IPE 120",MassLinear=MassLinear.Parse("10.4 kg/m"),AreaLinear= AreaLinear.Parse("0.475 m2/m"),AreaMass= AreaMass.Parse("45.82 m2/t")});		
			entityProvider.Register(new Beam(){Code="IPE 140",Libelle="IPE 140",MassLinear=MassLinear.Parse("12.9 kg/m"),AreaLinear= AreaLinear.Parse("0.551 m2/m"),AreaMass= AreaMass.Parse("42.70 m2/t")});		
			entityProvider.Register(new Beam(){Code="IPE 160",Libelle="IPE 160",MassLinear=MassLinear.Parse("15.8 kg/m"),AreaLinear= AreaLinear.Parse("0.623 m2/m"),AreaMass= AreaMass.Parse("39.47 m2/t")});		
			entityProvider.Register(new Beam(){Code="IPE 180",Libelle="IPE 180",MassLinear=MassLinear.Parse("18.8 kg/m"),AreaLinear= AreaLinear.Parse("0.698 m2/m"),AreaMass= AreaMass.Parse("37.13 m2/t")});		
			entityProvider.Register(new Beam(){Code="IPE 200",Libelle="IPE 200",MassLinear=MassLinear.Parse("22.4 kg/m"),AreaLinear= AreaLinear.Parse("0.768 m2/m"),AreaMass= AreaMass.Parse("34.36 m2/t")});		
			entityProvider.Register(new Beam(){Code="IPE 220",Libelle="IPE 220",MassLinear=MassLinear.Parse("26.2 kg/m"),AreaLinear= AreaLinear.Parse("0.848 m2/m"),AreaMass= AreaMass.Parse("32.36 m2/t")});		
		}
		
	}
}
