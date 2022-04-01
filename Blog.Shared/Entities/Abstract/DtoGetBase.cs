using Blog.Shared.Utilities.Results.ComplexTypes;

namespace Blog.Shared.Entities.Abstract
{
    public abstract class DtoGetBase
    {
        public virtual ResultStatus ResultStatus { get; set; }
    }
}
