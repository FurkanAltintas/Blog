using Blog.Shared.Entities.Abstract;

namespace Blog.Entities.Concrete
{
    public class Comment : EntityBase, IEntity
    {
        public int ArticleId { get; set; }

        public string Text { get; set; }


        public Article Article { get; set; }
    }
}
