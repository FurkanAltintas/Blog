using Blog.Entities.Dtos;

namespace Blog.Mvc.Areas.Admin.Models
{
    public class UserUpdateAjaxViewModel
    {
        public UserUpdateDto UserUpdateDto { get; set; }
        public UserDto UserDto { get; set; }
        public string UserUpdatePartial { get; set; }
    }
}
