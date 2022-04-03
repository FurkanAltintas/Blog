using Blog.Entities.Dtos;

namespace Blog.Mvc.Areas.Admin.Models
{
    public class CategoryUpdateAjaxViewModel
    {
        public CategoryUpdateDto CategoryUpdateDto { get; set; }
        public CategoryDto CategoryDto { get; set; }
        public string CategoryUpdatePartial { get; set; }
    }
}
