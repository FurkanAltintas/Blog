using Blog.Entities.Concrete;
using System.Collections.Generic;

namespace Blog.Entities.Dtos
{
    public class RoleListDto
    {
        public IList<Role> Roles { get; set; }
    }
}
