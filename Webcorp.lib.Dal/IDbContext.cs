using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webcorp.Dal
{
    public interface IDbContext
    {
        IMongoDatabase Database { get;  }

        bool IsAlive();
    }
}
