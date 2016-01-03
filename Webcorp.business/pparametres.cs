using System.ComponentModel;
using System.Collections.Generic;
using Webcorp.Model;
namespace Webcorp.Business{
	public class PParametres{
		public const string SOCIETÉPRINCIPALE = "001";
		public const string RAISONSOCIALE = "002";
		public const string FORMEETCAPITAL = "003";
		public const string NUMERORC = "004";
		public const string VILLERC = "005";
		public const string COMPTEURCOMMANDE = "006";
	
	}

	public class PParametresInitializer {
		public List<PParametre> StandardPParametres()
        {
			var result=new List<PParametre>();
			result.Add(new PParametre(){Id="001",Description="Societé principale",TypeParametre=(TypeParametre)3});		
			result.Add(new PParametre(){Id="002",Description="Raison Sociale",TypeParametre=(TypeParametre)3,Aide="Nom de la société"});		
			result.Add(new PParametre(){Id="003",Description="Forme et Capital",TypeParametre=(TypeParametre)3});		
			result.Add(new PParametre(){Id="004",Description="Numero RC",TypeParametre=(TypeParametre)3,Aide="Numéro registre du commerce"});		
			result.Add(new PParametre(){Id="005",Description="Ville RC",TypeParametre=(TypeParametre)3,Aide="Ville registre du commerce"});		
			result.Add(new PParametre(){Id="006",Description="Compteur Commande",TypeParametre=(TypeParametre)0,Aide="Compteur de commande"});		
			return result;
		}
		
	}
}
