using BloggerWebApi.BloggerWebApi.Application.DTOs;
using BloggerWebApi.BloggerWebApi.Domain.Entities;

namespace BloggerWebApi.BloggerWebApi.Application.Interfaces;

public interface IPostRepository
{
    Task<IEnumerable<Post>> GetAllAsync();
    Task<Post?> GetByIdAsync(string id);
    Task<Post?> GetBySlugAsync(string slug);
    Task<IEnumerable<Post>?> GetPublishedAsync(int? page, int? pageSize = 10);
    Task<IEnumerable<Post>?> GetDraftsByAuthorIdAsync (string authorId);
    Task AddAsync(Post post);
    Task<Post> UpdateAsync(Post post);
    Task<bool> DeleteAsync(string id);
    
    Task<IEnumerable<Post?>> GetByAuthorName(string authorName);
    Task<IEnumerable<Post?>> GetByAuthorIdAsync(string authorId);
    // Task<Post> CreateAsync(Post post, bool isPrivate = false);

    
    //
    // get by date
    // get by date range
    // get today posts
}