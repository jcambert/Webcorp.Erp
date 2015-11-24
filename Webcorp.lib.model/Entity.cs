using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;
using PropertyChanged;
using PropertyChanging;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Webcorp.Model
{
    [Serializable]
    [DataContract]

    public class CustomReactiveObject : ReactiveObject
    {
        public new event PropertyChangedEventHandler PropertyChanged = delegate { };
        public new event System.ComponentModel.PropertyChangingEventHandler PropertyChanging = delegate{};
        public void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
#if DEBUG
            Debug.WriteLine("CustomReactiveObject PropertyChanged:" + propertyName);
#endif
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        public void OnPropertyChanging([CallerMemberName]string propertyName = "")
        {
#if DEBUG
            Debug.WriteLine("CustomReactiveObject PropertyChanging:" + propertyName);
#endif
            PropertyChanging(this, new System.ComponentModel.PropertyChangingEventArgs(propertyName));
        }
    }

    /// <summary>
    /// Abstract Entity for all the BusinessEntities.
    /// </summary>
    [DataContract]
    [Serializable]
    [BsonIgnoreExtraElements(Inherited = true)]
  
    public  class Entity : CustomReactiveObject, IEntity
    {
       

        /// <summary>
        /// Gets or sets the id for this object (the primary record for an entity).
        /// </summary>
        /// <value>The id for this object (the primary record for an entity).</value>
        [DataMember]
        //[BsonRepresentation(BsonType.ObjectId)]
        [BsonId(IdGenerator = typeof(ErpIdGenerator))]
        public virtual string Id { get; set; }
        [DataMember]
        [BsonDateTimeOptions(Representation = BsonType.DateTime)]
        [BsonIgnoreIfNull]
        public virtual DateTime? CreatedOn { get; set; } 
        [DataMember]
        [BsonIgnoreIfNull]
        public string CreatedBy { get; set; }
        [DataMember]
        [BsonDateTimeOptions(Representation = BsonType.DateTime, Kind = DateTimeKind.Utc)]
        [BsonIgnoreIfNull]
        public DateTime? ModifiedOn { get; set; } = null;
        [DataMember]
        [BsonIgnoreIfNull]
        public string ModifiedBy { get; set; }
        [BsonIgnore]
        [IgnoreDataMember]
        public bool IsSelected { get; set; }

     //   public void OnPropertyChanged([CallerMemberName] string propertyName = "") { }
        
    }

    public class ErpIdGenerator : IIdGenerator
    {

        public object GenerateId(object container, object document)
        {

            return "" + Guid.NewGuid().ToString();
        }

        public bool IsEmpty(object id)
        {
            return id == null || String.IsNullOrEmpty(id.ToString());
        }
    }
}
