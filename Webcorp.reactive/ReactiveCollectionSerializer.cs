using MongoDB.Bson.Serialization.Serializers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization;
using MongoDB.Bson;

namespace Webcorp.reactive
{
    public class ReactiveCollectionSerializer<T>: SerializerBase<ReactiveCollection<T>>
    {
        public override void Serialize(BsonSerializationContext context, BsonSerializationArgs args, ReactiveCollection<T> values)
        {

            BsonSerializer.Serialize(context.Writer, values.Items);

            
        }

        public override ReactiveCollection<T> Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
        {
            var type = context.Reader.CurrentBsonType;
            if (type == BsonType.Array)
            {
                object o = BsonSerializer.Deserialize(context.Reader, (typeof(List<T>)));
                return new ReactiveCollection<T>(o as List<T>);
            }
            else
                return  new ReactiveCollection<T>();
        }
    }

    public class ReactiveNavigableCollectionSerializer<T> : SerializerBase<ReactiveNavigableCollection<T>>
    {
        public override void Serialize(BsonSerializationContext context, BsonSerializationArgs args, ReactiveNavigableCollection<T> values)
        {

            BsonSerializer.Serialize(context.Writer, values.Items);


        }

        public override ReactiveNavigableCollection<T> Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
        {
            var type = context.Reader.CurrentBsonType;
            if (type == BsonType.Array)
            {
                object o = BsonSerializer.Deserialize(context.Reader, (typeof(List<T>)));
                var result= new ReactiveNavigableCollection<T>(o as List<T>);
                result.UpdateCapabilities();

                return result;
            }
            else
                return new ReactiveNavigableCollection<T>();
        }
    }

    public class ReactiveVMCollectionSerializer<T> : SerializerBase<ReactiveVMCollection<T>> where T :class
    {
        public override void Serialize(BsonSerializationContext context, BsonSerializationArgs args, ReactiveVMCollection<T> values)
        {

            BsonSerializer.Serialize(context.Writer, values.Items);


        }

        public override ReactiveVMCollection<T> Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
        {
            var type = context.Reader.CurrentBsonType;
            if (type == BsonType.Array)
            {
                object o = BsonSerializer.Deserialize(context.Reader, (typeof(List<T>)));
                var result = new ReactiveVMCollection<T>(o as List<T>);
                result.UpdateCapabilities();

                return result;
            }
            else
                return new ReactiveVMCollection<T>();
        }
    }
}