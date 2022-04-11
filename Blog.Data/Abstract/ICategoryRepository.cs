using Blog.Entities.Concrete;
using Blog.Shared.Data.Abstract;
using System.Threading.Tasks;

namespace Blog.Data.Abstract
{
    public interface ICategoryRepository : IEntityRepository<Category>
    {
        Task<Category> GetByIdAsync(int categoryId);
    }
}