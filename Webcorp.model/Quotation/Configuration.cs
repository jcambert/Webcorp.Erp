using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Webcorp.unite;

namespace Webcorp.Model.Quotation
{
    internal class Configuration
    {
        public const string TAUX_HORRAIRE_METHODES = "QuotationTauxHorraireMethodes";
        public const int DEFAULT_TAUX_HORRAIRE_METHODES = 60;

        public const string TEMPS_METHODES = "QuotationTempsMethodes";
        public const int DEFAULT_TEMPS_METHODES = 9;

        public const string OUTILLAGE = "QuotationOutillage";
        public const int DEFAULT_OUTILLAGE = 0;

        public const string FAD = "QuotationFad";
        public const int DEFAULT_FAD = 45;

        public const string COEF_PRESTATION = "QuotationCoeficientPrestation";
        public const double DEFAULT_COEF_PRESTATION = 0.25;

        public const string COEF_MATIERE = "QuotationCoeficientMatiere";
        public const double DEFAULT_COEF_MATIERE = 0.25;

        public const string COEF_DECOUPE = "QuotationCoeficientPrestation";
        public const double DEFAULT_COEF_DECOUPE = 0.3;



        public static TauxHorraire DefaultTauxHorraireMethodes=> Helpers.GetThFromAppConfig(TAUX_HORRAIRE_METHODES, DEFAULT_TAUX_HORRAIRE_METHODES);

        public static Currency DefaultOutillage=> Helpers.GetCurrencyFromAppConfig(OUTILLAGE, DEFAULT_OUTILLAGE);

        public static Currency DefaultFAD => Helpers.GetCurrencyFromAppConfig(FAD, DEFAULT_FAD);

        public static double DefaultCoeficientPrestation => Helpers.GetDoubleFromAppConfig(COEF_PRESTATION, DEFAULT_COEF_PRESTATION) +1;

        public static double DefaultCoeficientDecoupe => Helpers.GetDoubleFromAppConfig(COEF_DECOUPE, DEFAULT_COEF_DECOUPE) + 1;

        public static Time DefaultTempsMethodes => Helpers.GetTimeFromAppConfig(TEMPS_METHODES, DEFAULT_TEMPS_METHODES);

        public static double DefaultCoeficientMatiere => Helpers.GetDoubleFromAppConfig(COEF_MATIERE, DEFAULT_COEF_MATIERE) + 1;
    }
}
