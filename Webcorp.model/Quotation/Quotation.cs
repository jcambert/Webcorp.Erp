﻿using MongoDB.Bson.Serialization.Attributes;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Webcorp.reactive;
using Webcorp.unite;

namespace Webcorp.Model.Quotation
{

    [Serializable]
    public class Quotation:Entity
    {
        public int Numero { get; set; }

        public string Client { get; set; }

        #region  Default Properties for EntityQuotation
        public string Commentaire { get; set; } = "";

        public TauxHorraire TauxHorraireMethodes { get; set; } = Configuration.DefaultTauxHorraireMethodes;

        public Time TempsMethodes { get; set; } = Configuration.DefaultTempsMethodes;

        public Currency FAD { get; set; } = Configuration.DefaultFAD;

        public Currency Outillage { get; set; } = Configuration.DefaultOutillage;

        public Difficulte Difficulte { get; set; } = Difficulte.Moyen;

        public Delai Delai { get; set; } = Delai.AConvenir;

        public TraitementSurface Ts { get; set; } = TraitementSurface.Aucun;

        [BsonSerializer(typeof(ReactiveCollectionSerializer<string>))]
        public ReactiveCollection<EntityQuotation> Entities { get; set; } = new ReactiveCollection<EntityQuotation>();

        public int Version { get; set; }

        #endregion

         

    }


}
