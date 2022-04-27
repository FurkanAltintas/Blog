using AutoMapper;
using Blog.Entities.Concrete;
using Blog.Entities.Dtos;
using System;

namespace Blog.Services.AutoMapper.Profiles
{
    public class ArticleProfile : Profile
    {
        public ArticleProfile()
        {
            CreateMap<ArticleAddDto, Article>().ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(x => DateTime.Now));
            CreateMap<ArticleUpdateDto, Article>().ForMember(dest => dest.ModifiedDate, opt => opt.MapFrom(x => DateTime.Now));
            CreateMap<Article, ArticleUpdateDto>(); // Article değerini ArticleUpdateDto'ya dönüştürüldü.
        }
    }
}