﻿using MongoDB.Bson.Serialization.Attributes;
using ReactiveUI;

namespace Webcorp.Model
{
    public class Nomenclatures:ReactiveList<Nomenclature>
    {
        [BsonIgnore]
        public Article Parent { get; set; }
    }
}
