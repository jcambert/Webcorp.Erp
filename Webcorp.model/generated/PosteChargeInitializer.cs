using System.ComponentModel;
using Webcorp.unite;
namespace Webcorp.Model.Quotation{
	public class PosteChargeInitializer : IEntityProviderInitializable<PosteCharge, int>{
		public void InitializeProvider(EntityProvider<PosteCharge, int> entityProvider)
        {
            
					 
			entityProvider.Register(new PosteCharge(){Section="Decoupe",Code=215,Designation="Laser bystronic", TauxHorrairePrep=70*TauxHorraire.EuroHeure,TauxHorraireOperation=110*TauxHorraire.EuroHeure,TempsPreparation=0.3*Time.Hour,Aide="Calcul", TempsBaseOp=0.0*Time.Hour,TypeDecoupe=TypeDecoupe.Laser});		
			entityProvider.Register(new PosteCharge(){Section="Decoupe",Code=205,Designation="T500R-2", TauxHorrairePrep=80*TauxHorraire.EuroHeure,TauxHorraireOperation=95*TauxHorraire.EuroHeure,TempsPreparation=0.5*Time.Hour,Aide="Calcul", TempsBaseOp=0.0*Time.Hour,TypeDecoupe=TypeDecoupe.Poinconnage});		
			entityProvider.Register(new PosteCharge(){Section="Decoupe",Code=100,Designation="Cisaillage", TauxHorrairePrep=70*TauxHorraire.EuroHeure,TauxHorraireOperation=70*TauxHorraire.EuroHeure,TempsPreparation=0.3*Time.Hour,Aide="Coupe/piece", TempsBaseOp=0.00300*Time.Hour,TypeDecoupe=TypeDecoupe.NonApplicable});		
			entityProvider.Register(new PosteCharge(){Section="Decoupe",Code=101,Designation="Cisaillage 2OPS", TauxHorrairePrep=70*TauxHorraire.EuroHeure,TauxHorraireOperation=125*TauxHorraire.EuroHeure,TempsPreparation=0.4*Time.Hour,Aide="Coupe/piece", TempsBaseOp=	0.00500*Time.Hour,TypeDecoupe=TypeDecoupe.NonApplicable});		
			entityProvider.Register(new PosteCharge(){Section="Pliage",Code=300,Designation="Pliage<200", TauxHorrairePrep=70*TauxHorraire.EuroHeure,TauxHorraireOperation=75*TauxHorraire.EuroHeure,TempsPreparation=0.3*Time.Hour,Aide="Nbr de Plis", TempsBaseOp=0.00400*Time.Hour,TypeDecoupe=TypeDecoupe.NonApplicable});		
			entityProvider.Register(new PosteCharge(){Section="Pliage",Code=301,Designation="Pliage>200", TauxHorrairePrep=70*TauxHorraire.EuroHeure,TauxHorraireOperation=80*TauxHorraire.EuroHeure,TempsPreparation=0.4*Time.Hour,Aide="Nbr de Plis", TempsBaseOp=0.00600*Time.Hour,TypeDecoupe=TypeDecoupe.NonApplicable});		
			entityProvider.Register(new PosteCharge(){Section="Pliage",Code=302,Designation="Pliage_2OPS", TauxHorrairePrep=90*TauxHorraire.EuroHeure,TauxHorraireOperation=125*TauxHorraire.EuroHeure,TempsPreparation=0.5*Time.Hour,Aide="Nbr de Plis", TempsBaseOp=0.0080*Time.Hour,TypeDecoupe=TypeDecoupe.NonApplicable});		
		}
		
	}
}
