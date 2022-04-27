using Blog.Entities.Dtos;

namespace Blog.Mvc.Areas.Admin.Models
{
    public class CategoryAddAjaxViewModel
    {
        public CategoryAddDto CategoryAddDto { get; set; }
        public CategoryDto CategoryDto { get; set; }
        public string CategoryAddPartial { get; set; }
    }
}
