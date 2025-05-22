using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace PotatisFunctionLab3.Services
{
    public class PotatisServices
    {
        private IMongoDatabase _database;

        public PotatisServices(string connectionString, string databaseName)
        {
            var client = new MongoClient(connectionString);
            _database = client.GetDatabase(databaseName);
        }

        public async Task<T?> AddAsync<T>(string collectionName, T item) //ADD
        {
            var collection = _database.GetCollection<T>(collectionName);
            await collection.InsertOneAsync(item);
            return item;
        }

        public async Task<List<T>> GetAllAsync<T>(string collectionName) //GET ALL
        {
            var collection = _database.GetCollection<T>(collectionName);
            return await collection.Find(_ => true).ToListAsync();
        }

        public async Task<T?> GetByIdAsync<T>(string collectionName, string id)
        {
            var collection = _database.GetCollection<T>(collectionName);
            var filter = Builders<T>.Filter.Eq("_id", ObjectId.Parse(id));
            return await collection.Find(filter).FirstOrDefaultAsync();
        }

        public async Task<T> UpdateAsync<T>(string collectionName, string id, T item)
        {
            var collection = _database.GetCollection<T>(collectionName);
            var filter = Builders<T>.Filter.Eq("_id", ObjectId.Parse(id));
            var result = await collection.ReplaceOneAsync(
                filter,
                item,
                new ReplaceOptions { IsUpsert = false });
            return result.ModifiedCount > 0 ? item : default;
        }

        public async Task<bool> DeleteAsync<T>(string collectionName, string id)
        {
            var collection = _database.GetCollection<T>(collectionName);
            var filter = Builders<T>.Filter.Eq("_id", ObjectId.Parse(id));
            var result = await collection.DeleteOneAsync(filter);
            return result.DeletedCount > 0;
        }
    }
}
