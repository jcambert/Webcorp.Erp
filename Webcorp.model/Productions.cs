using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReactiveUI;
using System.Runtime.Serialization;

namespace Webcorp.Model
{
    [DataContract]
    [Serializable]
    public class Productions:ReactiveList<Production>
    {
    }
}
