using Blog.Entities.Concrete;
using Blog.Shared.Entities.Abstract;
using System.Collections.Generic;

namespace Blog.Entities.Dtos
{
    public class UserListDto : DtoGetBase
    {
        public IList<User> Users { get; set; }
    }
}