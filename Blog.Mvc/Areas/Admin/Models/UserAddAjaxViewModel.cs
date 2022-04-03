using Blog.Entities.Dtos;

namespace Blog.Mvc.Areas.Admin.Models
{
    public class UserAddAjaxViewModel
    {
        public UserAddDto UserAddDto { get; set; }
        public UserDto UserDto { get; set; }
        public string UserAddPartial { get; set; }
    }
}
