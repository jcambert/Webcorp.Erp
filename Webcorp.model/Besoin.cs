using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reactive.Linq;
using ReactiveUI;
namespace Webcorp.Model
{
    public class Besoin : CustomReactiveObject
    {
        DateTime _date;
        int _qteb,_qter;
        string _ref;
        TypeBesoin _tb;

        public DateTime Date { get { return _date; } set { this.RaiseAndSetIfChanged(ref _date, value); } }
        public int QuantiteBesoin { get { return _qteb; } set { this.RaiseAndSetIfChanged(ref _qteb, value); } }
        public int QuantiteRecue { get { return _qter; } set { this.RaiseAndSetIfChanged(ref _qter, value); } }
        public string Reference { get { return _ref; } set { this.RaiseAndSetIfChanged(ref _ref, value); } }
        public TypeBesoin TypeBesoin { get { return _tb; } set { this.RaiseAndSetIfChanged(ref _tb, value); } }
    }

    public enum TypeBesoin
    {
        Null=0,
        Fournisseur,
        Production
    }
}
