using Blog.Entities.Concrete;
using Blog.Shared.Entities.Abstract;

namespace Blog.Entities.Dtos
{
    public class UserDto : DtoGetBase
    {
        public User User { get; set; }
    }
}