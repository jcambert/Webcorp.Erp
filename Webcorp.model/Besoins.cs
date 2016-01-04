using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Webcorp.Model
{
    [DataContract]
    [Serializable]
    public class Besoins : ReactiveList<Besoin>
    {
    }
}
