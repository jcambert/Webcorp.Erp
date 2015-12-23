using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Webcorp.unite;

namespace Webcorp.Model
{
    public enum TypeCommande
    {
        [Description("Commande Client")]
        Client,

        [Description("Commande Fournisseur")]
        Fournisseur,

        [Description("Commande de Production")]
        Production,

        [Description("Bordereau de Livraison")]
        Livraison

    }
    

    public enum StatutCommande
    {
        Creer,
        Editer,
        EnCours,
        Partiel,
        Solde,
        Facture

    }
    public class Commande : ErpEntity
    {
        [BsonId(IdGenerator = typeof(CommandIdGenerator))]
        public override string Id { get; set; }

        public int Numero { get; set; }

        public int Avenant { get; set; }

        public TypeCommande TypeCommande { get; set; }

        public StatutCommande Statut { get; set; }

        public string TiersId { get; set; }

        public Societe Tiers { get; set; }

    }

    public class LignesCommandes : ReactiveUI.ReactiveList<LigneCommande>
    {

    }

    public class LigneCommande
    {
        public string ArticleId { get; set; }

        public Article Article { get; set; }

        public int QuantiteCommandee { get; set; }

        public int QuantiteLivree { get; set; }

        public Currency Prixunitaire { get; set; }

        public double Remise { get; set; }

        public DateTime DelaiDemande { get; set; }

        public DateTime DelaiFinProduction { get; set; }

        public DateTime DelaiExpedition { get; set; }



    }

    /// <summary>
    /// Societe-Numero-Type-Avenant
    /// Ex:999-12345-CL-0
    /// </summary>
    public class CommandIdGenerator : IIdGenerator
    {

        public object GenerateId(object container, object document)
        {
            var art = document as Commande;
            return string.Format("{0}-{1}-{2}-{3}", art.Societe, art.Numero,art.TypeCommande.ToString().Left(2).ToUpper(), art.Avenant);
            //return "" + Guid.NewGuid().ToString();
        }

        public bool IsEmpty(object id)
        {
            return id == null || String.IsNullOrEmpty(id.ToString());
        }
    }

}
