using AutoMapper;
using Blog.Entities.Concrete;
using Blog.Entities.Dtos;

namespace Blog.Mvc.AutoMapper.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            // Kaynak => Varış Noktası
            CreateMap<UserAddDto, User>(); // UserAddDto'da ki verileri User sınıfına map edeceğiz.
            CreateMap<User, UserUpdateDto>(); // User içerisindeki verileri UserUpdateDto'ya veriyoruz
            CreateMap<UserUpdateDto, User>();
        }
    }
}