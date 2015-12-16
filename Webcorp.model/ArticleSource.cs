using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webcorp.Model
{
    public enum ArticleSource
    {
        [Description("Source interne")]
        Interne,
        [Description("Source Externe")]
        Externe
    }
}
