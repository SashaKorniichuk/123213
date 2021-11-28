using Data.Interfaces;
using MongoDB.Driver;
using Data.DBContext;
using MongoDbGenericRepository;

namespace Data
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        private readonly DbContext _dbContext;
        private readonly IMongoCollection<TEntity> _dbCollection;

        public GenericRepository(DbContext dbContext)
        {
            _dbContext = dbContext;
            string name = typeof(TEntity).Name.ToString();
            _dbCollection = _dbContext.GetCollection<TEntity>(name);
        }

        public async Task<IEnumerable<TEntity>> GetAllTaskAsync()
        {
            return await _dbCollection.Find(Builders<TEntity>.Filter.Empty).ToListAsync();

        }
    }
}
