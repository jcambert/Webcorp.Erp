using MongoDB.Bson.Serialization.Attributes;
using ReactiveUI;
using System;
using System.Reactive.Subjects;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace Webcorp.Model
{
    [DataContract]
    [Serializable]
    public class Nomenclatures:ReactiveList<Nomenclature>
    {
        Lazy<Subject<Nomenclature>> _itemRequested;

        public Nomenclatures()
        {
            ChangeTrackingEnabled = true;
            _itemRequested = new Lazy<Subject<Nomenclature>>(() => new Subject<Nomenclature>());
        }

        

        public IObservable<Nomenclature> ItemRequested { get { return _itemRequested.Value; } }
        
        public override  Nomenclature this[int index]
        {
            get
            {

                Task<Nomenclature> t = Task.Factory.StartNew(()=> {
                    var result = base[index];
                    if (result.Article.IsNull())
                    {
                        if (_itemRequested.IsValueCreated) _itemRequested.Value.OnNext(result);
                    }
                    return result;
                } );
                t.Wait();
                return t.Result;

            }

            set
            {
                base[index] = value;
            }
        }
    }
}
