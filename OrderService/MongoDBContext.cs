using CommonServicesLib.Models;
using MongoDB.Driver;

namespace OrderService
{
    
    public class MongoDBContext
    {
        private readonly IMongoDatabase _databse;
        public MongoDBContext()
        {
            var client = new MongoClient("mongodb://localhost:27017/");
            _databse = client.GetDatabase("MyDatabase");
        }

        public IMongoCollection<Order> Orders => _databse.GetCollection<Order>("Orders");
    }
}
