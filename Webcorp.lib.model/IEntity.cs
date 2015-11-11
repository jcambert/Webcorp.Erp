using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webcorp.Model
{
    /// <summary>
    /// Generic Entity interface.
    /// </summary>
    /// <typeparam name="TKey">The type used for the entity's Id.</typeparam>
    public interface IEntity<TKey>
    {
        /// <summary>
        /// Gets or sets the Id of the Entity.
        /// </summary>
        /// <value>Id of the Entity.</value>
       // [BsonId]
        TKey Id { get; set; }

        DateTime? CreatedOn { get; set; }

        string CreatedBy { get; set; }

        DateTime? ModifiedOn { get; set; }

        string ModifiedBy { get; set; }

        
    }

    /// <summary>
    /// "Default" Entity interface.
    /// </summary>
    /// <remarks>Entities are assumed to use strings for Id's.</remarks>
    public interface IEntity : IEntity<string>
    {
       
    }
}
