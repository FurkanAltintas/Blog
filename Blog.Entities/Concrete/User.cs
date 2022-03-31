using Blog.Shared.Entities.Abstract;
using System.Collections.Generic;

namespace Blog.Entities.Concrete
{
    public class User : EntityBase, IEntity
    {
        public int RoleId { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public byte[] PasswordHash { get; set; }
        public string UserName { get; set; }

        public Role Role { get; set; }
        public ICollection<Article> Articles { get; set; }
    }
}
