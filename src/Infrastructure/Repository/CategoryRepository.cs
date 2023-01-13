using Domain.IRepository;
using Domain.Models;
using Domain.ViewModels;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Infrastructure.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        readonly IMongoCollection<Category> _categoryCollection;

        public CategoryRepository(IOptions<WebApiDatabaseSettings> WebApiDatabaseSettings)
        {
            var mongoClient = new MongoClient(
                WebApiDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(WebApiDatabaseSettings.Value.DatabaseName);

            _categoryCollection = mongoDatabase.GetCollection<Category>("Categories");
        }

        public async Task<bool> Create(Category category)
        {
            try
            {
                await _categoryCollection.InsertOneAsync(category);
                return true;
            }
            catch (Exception ex)
            {

                return false;
            }
        }
        public async Task<bool> Update(Category category, string id)
        {
            try
            {
                await _categoryCollection.ReplaceOneAsync(x => x.Id == id, category);
                return true;
            }
            catch (Exception ex)
            {

                return false;
            }
        }
        public async Task<bool> Delete(string id)
        {
            try
            {
                await _categoryCollection.DeleteOneAsync(x => x.Id == id);
                return true;
            }
            catch (Exception ex)
            {

                return false;
            }
        }
        public async Task<Category> Get(string id)
        {
            return await _categoryCollection.Find(x => x.Id == id).FirstOrDefaultAsync();
        }
        public async Task<IEnumerable<Category>> GetAll()
        {
            return await _categoryCollection.Find(_ => true).ToListAsync();
        }
    }
}
