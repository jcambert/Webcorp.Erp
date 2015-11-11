using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Webcorp.unite;

namespace Webcorp.Model.Quotation
{
    public class OperationLaser : IOperationDecoupe
    {
        public OperationLaser()
        {

        }

        public OperationLaser(Operation op)
        {
            op.OperationDecoupe = this;
        }

        public double Epaisseur { get; set; }

        public GazDecoupe Gaz { get; set; }

        public Length Longueur { get; set; }

        public int NombrePetitDiametre { get; set; }

        public int NombreAmorcage { get; set; }

        public VitesseDecoupeLaser Laser { get; set; }

        public double CoeficientDecoupe { get; set; } = Configuration.DefaultCoeficientDecoupe;

        public Format FormatPiece { get; set; }

        public Format FormatTole { get; set; }

        [BsonIgnore]
        public Time TempsBaseOp
        {
            get
            {
                Time tpsDecoupe = Longueur / Laser.GrandeVitesse;
                //Debug.WriteLine("Tps Decoupe:" + tpsDecoupe.ToString("0.00000 [h]"));
                Time tpsPetitDiametre = (NombrePetitDiametre * 40) / Laser.PetiteVitesse.ConvertTo(Velocity.MillimetrePerMinute) * Time.Minute;
                //Debug.WriteLine("Tps  PetitDiametre:" + tpsPetitDiametre.ToString("0.00000 [h]"));
                Time tpsAmorcage = (0.00027 * NombreAmorcage * Epaisseur) * Time.Hour;
                //Debug.WriteLine("TpsAmorcage:" + tpsAmorcage.ToString("0.00000 [h]"));
                return (tpsDecoupe + tpsPetitDiametre + tpsAmorcage) * CoeficientDecoupe;
            }

        }

        public Length SqueletteX
        {
            get;
            set;
        }

        public Length SqueletteY
        {
            get;
            set;
        }

        public Length Pince
        {
            get;
            set;
        }


    }
}
