using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Attributes;
using ReactiveUI;
using System;
using System.Reactive.Disposables;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
namespace Webcorp.Model
{

    public interface IPropertyChanged
    {
        void OnPropertyChanged([CallerMemberName]  string propertyName = "");
    }

    public interface IShouldDispose
    {
        void ShouldDispose(IDisposable disposable);
    }


    [Serializable]
    [DataContract]
    [BsonSerializer]
    public class CustomReactiveObject : ReactiveObject, IDisposable, IShouldDispose
    {

        private readonly CompositeDisposable _disposables = new CompositeDisposable();

        public virtual void Dispose()
        { 
            _disposables.Dispose();
        }

        public void ShouldDispose(IDisposable disposable)
        {
            _disposables.Add(disposable);
        }
    }

    /// <summary>
    /// Abstract Entity for all the BusinessEntities.
    /// </summary>
    [DataContract]
    [Serializable]
    [BsonIgnoreExtraElements(Inherited = true)]
   
    public abstract class Entity : CustomReactiveObject, IEntity
    {


        /// <summary>
        /// Gets or sets the id for this object (the primary record for an entity).
        /// </summary>
        /// <value>The id for this object (the primary record for an entity).</value>
        //[DataMember]
        //[BsonId(IdGenerator = typeof(EntityIdGenerator))]
        [BsonIgnore]
        public abstract string Id { get; set; }
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
        public bool IsSelected { get; set; } = false;
        /// <summary>
        ///  Determine si l'article a été modifié ou non
        ///DO NOT SEND PropertyChanged event on this proprety 
        /// </summary>
        [BsonIgnore]
        [IgnoreDataMember]
        public bool IsChanged { get; set; } = false;
        /// <summary>
        /// Determine si l'article est attacher au BusinessHelper
        /// Pour declancher automatiquement le traitement métier
        /// </summary>
       // [BsonIgnore]
       // [IgnoreDataMember]
       // public bool IsAttached { get; set; }
        /// <summary>
        /// Enable or disable Events
        /// </summary>
        [BsonIgnore]
        [IgnoreDataMember]
        public bool EnableEvents { get; set; } = true;


        
    }

    public class EntityIdGenerator : IIdGenerator
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
