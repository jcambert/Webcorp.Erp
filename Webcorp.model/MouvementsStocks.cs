using MongoDB.Bson.Serialization.Attributes;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Webcorp.Model
{
    [DataContract]
    [Serializable]
    public class MouvementsStocks : ReactiveList<MouvementStock>
    {
        public MouvementsStocks() : base()
        {

        }
        public MouvementsStocks(IEnumerable<MouvementStock> list) : base(list)
        {

        }

       
        public Article Article { get; set; }

        public override void Add(MouvementStock item)
        {
            var insertPoint = this.Where(i => i.Date > item.Date).OrderBy(i => i.Date).FirstOrDefault();
            var idx = insertPoint == null ? Count : this.IndexOf(insertPoint);

            //idx = idx + 1;
            if (Count == 0)
                item.LastStock = 0;
            else
            {
                for (int i = idx; i < Count; i++)
                {
                    var mvt = this[i];
                    var mvto = i == idx ? item : this[i - 1];
                    mvt.LastStock = mvt.Sens == MouvementSens.Inventaire ? 0 : (mvto.Sens == MouvementSens.Sortie ? -1 : 1) * mvto.Quantite + mvto.LastStock;

                }
                var mvto_ = this[idx == Count ? idx - 1 : idx];
                item.LastStock = item.Sens == MouvementSens.Inventaire ? 0 : (mvto_.Sens == MouvementSens.Sortie ? -1 : 1) * mvto_.Quantite + mvto_.LastStock;
            }
            this.Insert(idx, item);
        }

        public override bool Remove(MouvementStock item)
        {
            return base.Remove(item);
        }

        public override void RemoveAt(int index)
        {
            if (index < Count - 1)
            {
                this[index + 1].LastStock = this[index].LastStock;
                for (int i = index + 1; i < Count - 1; i++)
                {
                    var mvt = this[i];
                    var mvto = this[i + 1];
                    mvto.LastStock = mvto.Sens == MouvementSens.Inventaire ? 0 : (mvt.Sens == MouvementSens.Sortie ? -1 : 1) * mvt.Quantite + mvt.LastStock;

                }
            }
            base.RemoveAt(index);
        }

        public int StockPhysique
        {
            get
            {
                var mvt = this.Last();
                return GetStock(mvt);
            }
        }

        public int StockAtDate(DateTime d)
        {

            var mvt = this.Where(i => i.Date >= d.Date).OrderBy(i => i.Date).FirstOrDefault();
            return GetStock(mvt);
        }

        private int GetStock(MouvementStock mvt) => mvt == null ? 0 : mvt.LastStock + (mvt.Sens == MouvementSens.Sortie ? -1 : 1) * mvt.Quantite;
    }
}
