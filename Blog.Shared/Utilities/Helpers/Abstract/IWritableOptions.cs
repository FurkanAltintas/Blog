using Microsoft.Extensions.Options;
using System;

namespace Blog.Shared.Utilities.Helpers.Abstract
{
    public interface IWritableOptions<out T> : IOptionsSnapshot<T> where T : class, new()
    {
        void Update(Action<T> applyChanges);
    }
}