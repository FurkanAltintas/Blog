using Blog.Shared.Entities.Abstract;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace Blog.Entities.Concrete
{
    public class User : IdentityUser<int>
    {
        public string Picture { get; set; }
        public ICollection<Article> Articles { get; set; }
    }
}
