using AutoMapper;
using BloggerWebApi.BloggerWebApi.Application.DTOs;
using BloggerWebApi.BloggerWebApi.Domain.Entities;

namespace BloggerWebApi.BloggerWebApi.Application.Mappings;

public class MapProfile : Profile
{
    public MapProfile()
    {
        CreateMap<Post, PostDto>().ForMember(dest => dest.AuthorName, opt => opt.MapFrom(src => src.Author != null ? src.Author.Name : "Unknown"));

        CreateMap<CreatePostDto, Post>();
        CreateMap<UpdatePostDto, Post>();
        
    }
}