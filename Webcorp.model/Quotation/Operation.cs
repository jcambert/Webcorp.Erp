﻿using MongoDB.Bson.Serialization.Attributes;
using System.Text;
using Webcorp.unite;
using ReactiveUI;
namespace Webcorp.Model.Quotation
{
    public class Operation:CustomReactiveObject
    {

        public Operation()
        {

        }

        public Operation(EntityQuotation e)
        {
            e.Operations.Add(this);

        }

        PosteCharge _posteCharge;
        public PosteCharge Poste { get { return _posteCharge; } set { this.RaiseAndSetIfChanged(ref _posteCharge, value); } }

        double _nombre;
        public double Nombre { get { return _nombre; } set { this.RaiseAndSetIfChanged(ref _nombre, value); } }

        [BsonIgnore]
        public Currency CoutPreparation => Poste.TauxHorrairePrep * Poste.TempsPreparation;

        [BsonIgnore]
        public Currency CoutOperation => Poste.TauxHorraireOperation * (TempsBaseOp * Nombre);

        [BsonIgnore]
        public double Cadence => 1 / (TempsBaseOp.ConvertTo(Time.Hour) * Nombre);

        [BsonIgnore]
        public Time TempsOperation => 1 / Cadence * Time.Hour;

        IOperationDecoupe _decoupe;
        [BsonIgnoreIfNull]
        public IOperationDecoupe OperationDecoupe { get { return _decoupe; } set { this.RaiseAndSetIfChanged(ref _decoupe, value); } }

        [BsonIgnore]
        private Time TempsBaseOp
        {
            get
            {
                if (Poste.TypeDecoupe != TypeDecoupe.NonApplicable) return OperationDecoupe.TempsBaseOp;
                return Poste.TempsBaseOp;
            }
        }

        [BsonIgnore]
        public Currency CoutMatiere
        {
            get
            {
                if (Poste.TypeDecoupe == TypeDecoupe.NonApplicable) return 0 * Currency.Euro;
                return OperationDecoupe?.FormatTole?.CoutMatiere * Nombre;
            }
        }


        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
          
            sb.AppendLine("Operation:" + Poste.Designation);
            sb.AppendLine("Cout matière:" + CoutMatiere.ToString("0.00 [euro]"));
            sb.AppendLine("Aide:" + Poste.Aide);
            sb.AppendLine("Nbre Action:" + Nombre);
            sb.AppendLine("tx prep:" + Poste.TauxHorrairePrep);
            sb.AppendLine("tps prep:" + Poste.TempsPreparation.ToString("0.00000 [h]"));
            sb.AppendLine("cout prep:" + CoutPreparation);
            sb.AppendLine("th op:" + Poste.TauxHorraireOperation);
            sb.AppendLine("tps base op:" + TempsBaseOp.ToString("0.00000 [h]"));
            //sb.AppendLine("tps op:" + TempsOperation);
            sb.AppendLine("cout op:" + CoutOperation);
            sb.AppendLine("cadence:" + Cadence + "p/h");
            sb.AppendLine("tps/op:" + TempsOperation.ToString("0.00 [min]"));
            return sb.ToString();
        }
    }
}