using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Webcorp.OneDCut.Models
{
    public class CutModel
    {
        public List<Stock> Stocks { get; set; } = new List<Stock>();

        public List<Stock> ToCut { get; set; } = new List<Stock>();

    }

    public class Stock
    {
        public int Id { get; set; }

        [Display(Name = "Longueur")]
        public int Length { get; set; }

        [Display(Name = "Quantité")]
        public int Quantity { get; set; }
    }
}