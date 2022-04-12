using AutoMapper;
using Blog.Data.Abstract;
using Blog.Services.Abstract;
using Blog.Shared.Utilities.Results.Abstract;
using Blog.Shared.Utilities.Results.ComplexTypes;
using Blog.Shared.Utilities.Results.Concrete;
using System.Threading.Tasks;

namespace Blog.Services.Concrete
{
    public class CommentManager : ICommentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CommentManager(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IDataResult<int>> Count()
        {
            var comments = await _unitOfWork.Comments.CountAsync();
            if (comments > -1)
            {
                return new DataResult<int>(ResultStatus.Success, comments);
            }
            return new DataResult<int>(ResultStatus.Error, $"Beklenmeyen bir hata ile karşılaşıldı", -1);
        }

        public async Task<IDataResult<int>> CountByNonDeleted()
        {
            var comments = await _unitOfWork.Comments.CountAsync(c => !c.IsDeleted);
            if (comments > -1)
            {
                return new DataResult<int>(ResultStatus.Success, comments);
            }
            return new DataResult<int>(ResultStatus.Error, $"Beklenmeyen bir hata ile karşılaşıldı", -1);
        }
    }
}