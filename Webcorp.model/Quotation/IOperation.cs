using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Webcorp.unite;

namespace Webcorp.Model.Quotation
{
    public interface IOperation
    {
        Time TempsBaseOp { get; }
    }

    public interface IOperationDecoupe : IOperation
    {
        Format FormatPiece { get; set; }
        Format FormatTole { get; set; }
        Length SqueletteX { get; set; }
        Length SqueletteY { get; set; }
        Length Pince { get; set; }
    }
}
