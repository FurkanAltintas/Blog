using Blog.Entities.Concrete;
using Blog.Entities.Dtos;

namespace Blog.Mvc.Models
{
    public class ArticleDetailRightSideBarViewModel
    {
        public string Header { get; set; } // Listelenen makaleler ile ilgili başlık
        public ArticleListDto ArticleListDto { get; set; }
        public User User { get; set; }
    }
}