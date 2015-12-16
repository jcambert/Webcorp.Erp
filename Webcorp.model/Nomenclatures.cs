using MongoDB.Bson.Serialization.Attributes;
using ReactiveUI;

namespace Webcorp.Model
{
    public class Nomenclatures:ReactiveList<Nomenclature>
    {

        public Nomenclatures()
        {
            ChangeTrackingEnabled = true;
        }
        
    }
}
