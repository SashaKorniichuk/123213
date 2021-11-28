using Data.Entities;
using MongoDB.Driver;

namespace Data.DBContext
{
    public class DbContext
    {
        private readonly IMongoDatabase _dbContext;

        public DbContext(IMongoClient client, string dbName)
        {
            _dbContext = client.GetDatabase(dbName);
        }
        public IMongoCollection<T> GetCollection<T>(string name)
        {
            return _dbContext.GetCollection<T>(name);
        }
        public IMongoCollection<UserTask> UserTasks => _dbContext.GetCollection<UserTask>("UserTask");
    }
}
