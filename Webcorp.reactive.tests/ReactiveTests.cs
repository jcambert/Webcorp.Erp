using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization;
using System.IO;
using MongoDB.Bson.IO;
using System.Text;
using System.Diagnostics;
using ReactiveUI;
using Webcorp.Model.Quotation;

namespace Webcorp.reactive.tests
{
    [TestClass]
    public class ReactiveTests
    {
        [TestMethod]
        public void MyTestMethod()
        {
            var a = AppDomain.CurrentDomain.GetAssemblies().Any(ass => ass.FullName.StartsWith("Microsoft.VisualStudio.TestPlatform"));
        }

        [TestMethod]
        public void TestSimplePocoReactiveSerialization()
        {
            var poco = new POCO() { Index = 10 };
            poco.Values.Add("un");
            poco.Values.Add("deux");
            poco.Values.Add("trois");

            string s = Serialize(poco);
            var poco_ = Deserialize<POCO>(s);
            Assert.AreEqual(poco, poco_);
        }

        [TestMethod]
        public void TestReactivePocoReactiveSerialization()
        {
            var poco = new POCO2() { Index = 10 };
            poco.Values.Add("un");
            poco.Values.Add("deux");
            poco.Values.Add("trois");


            string s = Serialize(poco);


            var poco_ = Deserialize<POCO2>(s);

            Assert.AreEqual(poco, poco_);
        }


        [TestMethod]
        public void TestSimplePocoNavigbleReactiveSerialization()
        {
            var poco = new POCO3() { Index = 10 };
            poco.Values.Add("un");
            poco.Values.Add("deux");
            poco.Values.Add("trois");

            string s = Serialize(poco);

            POCO3 poco_ = Deserialize<POCO3>(s);

            Assert.AreEqual(poco, poco_);
            Assert.IsTrue(poco_.Values.CanGoNext);
        }


        [TestMethod]
        public void TestQuotationSerialization()
        {
            var q = new Quotation();
            var s=Serialize(q);

            var q_ = Deserialize<Quotation>(s);

            Assert.AreEqual(q, q_);
        }

        private string Serialize(object o)
        {
            StringBuilder sb = new StringBuilder();
            TextWriter tw = new StringWriter(sb);
            using (BsonWriter writer = new JsonWriter(tw))
            {
                BsonSerializer.Serialize(writer, o);

            }
            Debugger.Log(0, "", sb.ToString());
            return sb.ToString();
        }

        private T Deserialize<T>(string s)
        {
            BsonReader reader = new JsonReader(s);
            return (T)BsonSerializer.Deserialize(reader, typeof(T));
        }
    }

    public class POCO
    {

        public int Index { get; set; }

        [BsonSerializer(typeof(ReactiveCollectionSerializer<string>))]
        public ReactiveCollection<string> Values { get; set; } = new ReactiveCollection<string>();

        public override bool Equals(object obj)
        {
            var cmp = (POCO)obj;

            return Index == cmp.Index && Values.Items.SequenceEqual(cmp.Values.Items);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }

    public class POCO2 : ReactiveObject
    {
        public int Index { get; set; }

        [BsonSerializer(typeof(ReactiveCollectionSerializer<string>))]
        public ReactiveCollection<string> Values { get; set; } = new ReactiveCollection<string>();

        public override bool Equals(object obj)
        {
            var cmp = (POCO)obj;

            return Index == cmp.Index && Values.Items.SequenceEqual(cmp.Values.Items);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }

    public class POCO3
    {

        public int Index { get; set; }

        [BsonSerializer(typeof(ReactiveNavigableCollectionSerializer<string>))]
        public ReactiveNavigableCollection<string> Values { get; set; } = new ReactiveNavigableCollection<string>();

        public override bool Equals(object obj)
        {
            var cmp = (POCO3)obj;

            return Index == cmp.Index && Values.Items.SequenceEqual(cmp.Values.Items);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
