using Blog.Shared.Entities.Concrete;
using Blog.Shared.Utilities.Results.ComplexTypes;
using System;
using System.Collections.Generic;

namespace Blog.Shared.Utilities.Results.Abstract
{
    public interface IResult
    {
        public ResultStatus ResultStatus { get; }
        public string Message { get; }
        public Exception Exception { get; }
        public IEnumerable<ValidationError> ValidationErrors { get; }
    }
}