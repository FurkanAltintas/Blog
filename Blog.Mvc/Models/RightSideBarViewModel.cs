using Blog.Entities.Concrete;
using System.Collections.Generic;

namespace Blog.Mvc.Models
{
    public class RightSideBarViewModel
    {
        public IList<Category> Categories { get; set; }
        public IList<Article> Articles { get; set; }
    }
}
