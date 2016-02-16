using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Webcorp.Model
{
    public interface IEntityProvider<T,TKey> where T : Entity
    {
        void Register(T entity);

        T Find(TKey key);

        T Find(params string[] keys);

        List<TKey> Keys { get; }

        List<T> Entities { get; }
    }
}
