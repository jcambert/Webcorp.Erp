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
    public class SettingsModel : ISolverParameter,IInitializable
    {
        public SettingsModel()
        {

        }
        
        [Inject]
        public ISolverParameter OrignalParameters { get;  set; }

        [DisplayName("Probabilité d'échange")]
        [Required]
        [Range(0,1,ErrorMessage ="Veuillez entrer un nombre entre 0 et 1")]
        public double CrossoverProbability
        {
            get;set;

        }

        [DisplayName("Pourcente d'élite")]
        [Required]
        [Range(0,100,ErrorMessage ="Veuillez entrer un nombre entre 0 et 100")]
        public int ElitePercentage
        {
            get;set;
        }

        [DisplayName("Nombre de population initiale")]
        [Required]
        [Range(1,int.MaxValue)]
        public int InitialPopulationCount
        {
            get;set;
        }

        [DisplayName("Nombre d'évaluation Max")]
        [Required]
        [Range(1,int.MaxValue,ErrorMessage ="Veuillez entrer un nombre >0")]
        public int MaxEvaluation
        {
            get;set;
        }


        [DisplayName("Probabilité de mutation")]
        [Required]
        [Range(0,1,ErrorMessage ="Veuillez entrer un nombre entre 0 et1")]
        public double MutationProbability
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
        }
    }
}