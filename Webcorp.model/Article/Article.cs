using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webcorp.Model.Article
{
    [DebuggerDisplay("Article Code={Code},Libelle={Libelle}")]
    public class Article:Entity
    {
        [BsonRequired]
        [KeyProvider]
        public string Code { get; set; }

        public string Libelle { get; set; }
    }
}
