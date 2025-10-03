using BloggerWebApi.BloggerWebApi.Domain.Entities;

namespace BloggerWebApi.BloggerWebApi.Application.Interfaces;

public interface IPostRepository
{
    Task<IEnumerable<Post>> GetAllAsync();
    Task<Post?> GetByIdAsync(int id);
    Task<IEnumerable<Post?>> GetByAuthorName(string authorName);
    Task<IEnumerable<Post?>> GetByAuthorIdAsync(int authorId);
    Task CreateAsync(Post post);
    Task<Post> UpdateAsync(Post post);
    Task<bool> DeleteAsync(int id);
}