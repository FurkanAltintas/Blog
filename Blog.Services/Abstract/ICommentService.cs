using Blog.Shared.Utilities.Results.Abstract;
using System.Threading.Tasks;

namespace Blog.Services.Abstract
{
    public interface ICommentService
    {
        Task<IDataResult<int>> Count();
        Task<IDataResult<int>> CountByNonDeleted();
    }
}