using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webcorp.Dal
{
    public class DbContext : IDbContext
    {
        private readonly IMongoDatabase database;

        /// <summary>
        /// Initializes a new instance of the MongoRepository class.
        /// Uses the Default App/Web.Config connectionstrings to fetch the connectionString and Database name.
        /// </summary>
        /// <remarks>Default constructor defaults to "MongoServerSettings" key for connectionstring.</remarks>
        public DbContext()
            : this(Util.GetDefaultConnectionString())
        {
        }

        /// <summary>
        /// Initializes a new instance of the MongoRepository class.
        /// </summary>
        /// <param name="connectionString">Connectionstring to use for connecting to MongoDB.</param>
        public DbContext(string connectionString, string databaseName = "")
        {
            connectionString.ThrowIfNullOrEmpty("You must specify a connection string in App.config ConnectionString section with key=MongoServer");
            var url = new MongoUrl(connectionString);
            if (databaseName.IsNullOrEmpty()) databaseName = url.DatabaseName;
            if (databaseName.IsNullOrEmpty()) databaseName = Util.GetDefaultDatabaseName();
            databaseName.ThrowIfNullOrEmpty("You must specify a database name in App.config AppSetting section with key=MongoDatabaseName");
            database = Util.GetDatabaseFromUrl(url,databaseName);
        }



        /// <summary>
        /// Initializes a new instance of the MongoRepository class.
        /// </summary>
        /// <param name="url">Url to use for connecting to MongoDB.</param>
        public DbContext(MongoUrl url)
        {
            database = Util.GetDatabaseFromUrl(url);
        }

        public IMongoDatabase Database => database;

        public bool IsAlive()
        {

            return Database.Client.Cluster.Description.State == MongoDB.Driver.Core.Clusters.ClusterState.Connected;
        }
    }
}
