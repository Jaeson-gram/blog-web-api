using BloggerWebApi.BloggerWebApi.Application.DTOs;
using BloggerWebApi.BloggerWebApi.Domain.Entities;

namespace BloggerWebApi.BloggerWebApi.Application.Interfaces;

public interface IPostService
{
    Task<IEnumerable<PostSummaryDto>> GetPublishedPostsAsync(int page = 1, int pageSize = 10);
    Task<PostDetailDto> GetBySlugAsync(string slug);
    Task<PostDetailDto> GetByIdAsync(string id);
    Task<PostDto> CreateAsync(CreatePostDto post, string authorId);
    Task<PostDto?> UpdateAsync(UpdatePostDto post, string Id, string authorId);
    Task DeleteAsync(string postId, string authorId);
    
}