using Blog.Entities.Concrete;
using System.Collections.Generic;

namespace Blog.Entities.Dtos
{
    public class CommentListDto
    {
        public IList<Comment> Comments { get; set; }
    }
}
