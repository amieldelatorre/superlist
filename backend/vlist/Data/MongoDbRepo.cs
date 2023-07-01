using Microsoft.Extensions.Options;
using MongoDB.Driver;
using vlist.Models.VList;

namespace vlist.Data
{
    public class MongoDbRepo : IRepo
    {
        private readonly IMongoCollection<VList> _vlistCollection;

        public MongoDbRepo(IOptions<MongoDbDatabaseSettings> mongoDbDatabaseSettings)
        {
            var mongoClient = new MongoClient(
                mongoDbDatabaseSettings.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(
                mongoDbDatabaseSettings.Value.DatabaseName);
            _vlistCollection = mongoDatabase.GetCollection<VList>(
                mongoDbDatabaseSettings.Value.CollectionName);

            var indexOptions = new CreateIndexOptions { ExpireAfter = new TimeSpan(0, 0, 0) };
            var model = new CreateIndexModel<VList>(Builders<VList>.IndexKeys.Ascending("Expiry"), indexOptions);
            _vlistCollection.Indexes.CreateOne(model);

        }

        public async Task<VList> GetAsync(string id) =>
            await _vlistCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(VList vList) =>
            await _vlistCollection.InsertOneAsync(vList);

        public async Task UpdateAsync(string id, VList updatedVlist) =>
            await _vlistCollection.ReplaceOneAsync(x => x.Id == id, updatedVlist);

    }
}
