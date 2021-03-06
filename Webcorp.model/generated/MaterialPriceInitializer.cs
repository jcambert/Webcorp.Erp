﻿using System.ComponentModel;
using Webcorp.unite;
namespace Webcorp.Model.Quotation{
	public class MaterialPriceInitializer : IEntityProviderInitializable<MaterialPrice, string>{
		public void InitializeProvider(EntityProvider<MaterialPrice, string> entityProvider)
        {
				 
			entityProvider.Register(new MaterialPrice(){MaterialNumber="1.0035",Cout=600*MassCurrency.EuroKilogramme});		
			entityProvider.Register(new MaterialPrice(){MaterialNumber="1.0036",Cout=MassCurrency.Parse("600 euro/tonne")});		
		}
		
	}
}
