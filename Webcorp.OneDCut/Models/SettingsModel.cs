using Ninject;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Webcorp.lib.onedcut;

namespace Webcorp.OneDCut.Models
{
    public class SettingsModel : ISolverParameter, IInitializable
    {
        public SettingsModel()
        {
            LargeurLame = 0.0;
            MiniLength = 0;
        }

        [Inject]
        public ISolverParameter OrignalParameters { get; set; }

        [DisplayName("Probabilité d'échange")]
        [Required]
        [Range(0, 1, ErrorMessage = "Veuillez entrer un nombre entre 0 et 1")]
        public double CrossoverProbability
        {
            get; set;

        }

        [DisplayName("Pourcente d'élite")]
        [Required]
        [Range(0, 100, ErrorMessage = "Veuillez entrer un nombre entre 0 et 100")]
        public int ElitePercentage
        {
            get; set;
        }

        [DisplayName("Nombre de population initiale")]
        [Required]
        [Range(1, int.MaxValue)]
        public int InitialPopulationCount
        {
            get; set;
        }

        [DisplayName("Nombre d'évaluation Max")]
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Veuillez entrer un nombre >0")]
        public int MaxEvaluation
        {
            get; set;
        }


        [DisplayName("Probabilité de mutation")]
        [Required]
        [Range(0, 1, ErrorMessage = "Veuillez entrer un nombre entre 0 et 1")]
        public double MutationProbability
        {
            get; set;
        }

        public double LargeurLame
        {
            get; set;
        }


        [DisplayName("Largeur de lame")]
        [Range(0, 10, ErrorMessage = "Veuillez entrer un nombre entre 0 et 10")]
        public int CuttingWidth
        {
            get; set;
        }

        [DisplayName("Longueur Mini à mettre en stock")]
        [Range(0, 12000, ErrorMessage = "Veuillez entrer un nombre entre 0 et 12000")]
        public int MiniLength
        {
            get;set;
        }

        public void Save()
        {
            OrignalParameters?.Save();
        }

        public void Initialize()
        {
            this.CrossoverProbability = OrignalParameters.CrossoverProbability;
            this.ElitePercentage = OrignalParameters.ElitePercentage;
            this.InitialPopulationCount = OrignalParameters.InitialPopulationCount;
            this.MaxEvaluation = OrignalParameters.MaxEvaluation;
            this.MutationProbability = OrignalParameters.MutationProbability;
            this.CuttingWidth = OrignalParameters.CuttingWidth;
            this.MiniLength = OrignalParameters.MiniLength;
        }
    }
}