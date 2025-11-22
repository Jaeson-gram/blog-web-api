using BloggerWebApi.BloggerWebApi.Domain.Entities;

namespace BloggerWebApi.BloggerWebApi.Application.Interfaces;

public interface IPostRepository
{
    Task<IEnumerable<Post>> GetAllAsync();
    Task<Post?> GetByIdAsync(string id);
    Task<IEnumerable<Post?>> GetByAuthorName(string authorName);
    Task<IEnumerable<Post?>> GetByAuthorIdAsync(string authorId);
    Task CreateAsync(Post post);
    Task<Post> UpdateAsync(Post post);
    Task<bool> DeleteAsync(string id);
    
    //
    // get by date
    // get by date range
    // get today posts
}