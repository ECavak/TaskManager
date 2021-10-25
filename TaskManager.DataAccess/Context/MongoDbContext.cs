using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using TaskManager.Core.Settings;

namespace TaskManager.DataAccess.Context
{
   public class MongoDbContext
    {
        private readonly IMongoDatabase _mongoDatabase;
        public MongoDbContext(IOptions<MongoSettings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            _mongoDatabase = client.GetDatabase(settings.Value.Database);
        }
        public IMongoCollection<T> GetCollection<T>()
        {
            return _mongoDatabase.GetCollection<T>(typeof(T).Name.Trim());

        }
        public IMongoDatabase GetDatabase()
        {
            return _mongoDatabase;
        }
    }
}
