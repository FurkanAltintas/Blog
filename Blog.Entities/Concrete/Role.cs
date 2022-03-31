using Blog.Shared.Entities.Abstract;
using System.Collections.Generic;

namespace Blog.Entities.Concrete
{
    public class Role : EntityBase, IEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public ICollection<User> Users { get; set; }
    }
}
