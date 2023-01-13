using Domain.Models;

namespace Domain.IRepository
{
    public interface IProductRepository
    {
        Task<bool> Create(Product product);
        Task<bool> Update(Product product, string id);
        Task<bool> Delete(string id);
        Task<Product> Get(string id);
        Task<IEnumerable<Product>> GetAll();

    }
}
