using Domain.IRepository;
using Domain.Models;
using Domain.ViewModels;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Domain.Extension;
namespace WebApiWithMongoDB.Infrastructure.Repository
{
    public class ProductRepository : IProductRepository
    {
        readonly IMongoCollection<Product> _productCollection;
        readonly IMongoDatabase _mongoDatabase;

        public ProductRepository(IOptions<WebApiDatabaseSettings> WebApiDatabaseSettings)
        {
            var mongoClient = new MongoClient(WebApiDatabaseSettings.Value.ConnectionString);

            _mongoDatabase = mongoClient.GetDatabase(WebApiDatabaseSettings.Value.DatabaseName);

            _productCollection = _mongoDatabase.GetCollection<Product>("Products");
        }

        public async Task<bool> Create(Product product)
        {
            try
            {
                await _productCollection.InsertOneAsync(product);
                return true;
            }
            catch (Exception ex)
            {

                return false;
            }
        }
        public async Task<bool> Update(Product product, string id)
        {
            try
            {
                await _productCollection.ReplaceOneAsync(x => x.Id == id, product);
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
                await _productCollection.DeleteOneAsync(x => x.Id == id);
                return true;
            }
            catch (Exception ex)
            {

                return false;
            }
        }
        public async Task<Product> Get(string id)
        {
            var product = await _productCollection.Find(x => x.Id == id).FirstOrDefaultAsync();
            return await product.IncludeProperty(_mongoDatabase);
        }
        public async Task<IEnumerable<Product>> GetAll()
        {
            return await _productCollection.Find(_ => true).ToListAsync();
        }
    }
}
