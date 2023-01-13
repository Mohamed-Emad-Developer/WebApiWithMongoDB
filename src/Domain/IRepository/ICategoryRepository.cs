using Domain.Models;

namespace Domain.IRepository
{
    public interface ICategoryRepository
    {
        Task<bool> Create(Category category);
        Task<bool> Update(Category category, string id);
        Task<bool> Delete(string id);
        Task<Category> Get(string id);
        Task<IEnumerable<Category>> GetAll();

    }
}
