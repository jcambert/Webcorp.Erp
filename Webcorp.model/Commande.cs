using AutoMapper;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Webcorp.common;
using Webcorp.unite;

namespace Webcorp.Model
{
    public enum CommandeType
    {
        [Description("Commande Client")]
        Client,

        [Description("Commande Fournisseur")]
        Fournisseur,

        [Description("Commande de Production")]
        Production,

        [Description("Bordereau de Livraison")]
        Livraison,

        [Description("Avoir")]
        Avoir

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

    [CollectionName("Commande")]
    public class Commande : ErpEntity
    {
        [BsonId(IdGenerator = typeof(CommandIdGenerator))]
        public override string Id { get; set; }
        [BsonElement("nume"), BsonRequired]
        public int Numero { get; set; }
        [BsonElement("ind"), BsonRequired]
        public int Avenant { get; set; }
        [BsonElement("typcde"), BsonRequired]
        public CommandeType CommandeType { get; set; }
        [BsonElement("status"), BsonRequired]
        public StatutCommande Statut { get; set; }
        [BsonElement("tiers"), BsonRequired]
        public Societe Tiers { get; set; }
        [BsonElement("lignes"), BsonRequired]
        public LignesCommandes Lignes { get; set; } = new LignesCommandes();

    }

    public class LignesCommandes : ReactiveUI.ReactiveList<LigneCommande>
    {
        public override void Add(LigneCommande item)
        {
            (item.Commande.CommandeType == CommandeType.Fournisseur && (item.Article.TypeArticle != ArticleType.SousTraitance && item.Article.TypeArticle != ArticleType.MatièrePremiere)).ThrowIf<InvalidOperationException>("INVALID");
            ((item.Commande.CommandeType == CommandeType.Client || item.Commande.CommandeType==CommandeType.Livraison || item.Commande.CommandeType == CommandeType.Production || item.Commande.CommandeType == CommandeType.Avoir)  && item.Article.TypeArticle != ArticleType.ProduitFini).ThrowIf<InvalidOperationException>("INVALID");
            
            base.Add(item);
        }
    }

    public class LigneCommande
    {
        Article _inner;

        [BsonConstructor]
        public LigneCommande()
        {

        }

        public LigneCommande(Commande cde)
        {
            this.Commande = cde;
            this.CommandeId = cde.Id;
        }

        [BsonIgnore]
        public Commande Commande { get; set; }

        [BsonElement("cdeid"),BsonRequired]
        public string CommandeId { get; set; }

        [BsonIgnore]
        public Article ArticleInner
        {
            get { return _inner; }
            set
            {
                if (_inner == value) return;
                Article = Mapper.Map<ArticleCommande>(value);
                _inner = value;
            }
        }

        [BsonElement("art"), BsonRequired]
        public ArticleCommande Article { get; set; }

        [BsonElement("qtecde")]
        public int QuantiteCommandee { get; set; }
        [BsonElement("qteliv")]
        public int QuantiteLivree { get; set; }
        [BsonElement("prixu")]
        public Currency Prixunitaire { get; set; }
        [BsonElement("remise")]
        public double Remise { get; set; }
        [BsonElement("datdem")]
        public DateTime DelaiDemande { get; set; }
        [BsonElement("datmad")]
        public DateTime DelaiMiseADisposition { get; set; }
        [BsonElement("datexp")]
        public DateTime DelaiExpedition { get; set; }



    }

    /// <summary>
    /// Not intended to be a bson document
    /// It must be include in LigneCommande only
    /// Using automapper From Article to -> ArticleCommande
    /// </summary>
    public class ArticleCommande : ErpEntity
    {
        //[BsonId(IdGenerator = typeof(CommandIdGenerator))]
        public override string Id { get; set; }

        string _code, _libelle;
        [BsonRequired]

        [BsonElement("artcod")]
        public string Code { get { return _code; } set { this.SetAndRaise(ref _code, value); } }
        [BsonIgnoreIfNull]
        [BsonElement("artlib")]
        public string Libelle { get { return _libelle; } set { this.SetAndRaise(ref _libelle, value); } }
        [BsonRequired]
        [BsonElement("typart")]
        public ArticleType TypeArticle { get; set; }
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
            return string.Format("{0}-{1}-{2}-{3}", art.Societe, art.Numero, art.CommandeType.ToString().Left(2).ToUpper(), art.Avenant);
            //return "" + Guid.NewGuid().ToString();
        }

        public bool IsEmpty(object id)
        {
            return id == null || String.IsNullOrEmpty(id.ToString());
        }
    }

}
