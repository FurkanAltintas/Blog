using Blog.Entities.Concrete;
using Blog.Shared.Data.Abstract;

namespace Blog.Data.Abstract
{
    public interface IArticleRepository : IEntityRepository<Article>
    {
    }
}
