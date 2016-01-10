using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Webcorp.lib.onedcut;

namespace Webcorp.OneDCut.Models
{
    public class CutModel
    {
        public SolverEventArgs Solve { get; internal set; }

        public List<Stock> Stocks { get; set; } = new List<Stock>();

        public List<Stock> ToCut { get; set; } = new List<Stock>();

        public bool CanCalculate => Stocks.Count > 0 && ToCut.Count > 0;
    }

    public class Stock
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [Required]
        [Range(1,12000,ErrorMessage = "Veuillez entrer un nombre entre 1 et 12000")]
        [Display(Name = "Longueur")]
        public int Length { get; set; }

        [Required]
        [Range(1, Int32.MaxValue, ErrorMessage = "Veuillez entrer un nombre supérieur à 0")]
        [Display(Name = "Quantité")]
        public int Quantity { get; set; }
    }
}