using BloggerWebApi.BloggerWebApi.Domain.Entities;

namespace BloggerWebApi.BloggerWebApi.Application.Interfaces;

public interface IAuthorRepository
{
    Task<IEnumerable<Author>> GetAllAsync();
    Task<Author?> GetByIdAsync(string id);
    Task<IEnumerable<Author?>> GetByNameAsync(string name);
    Task<Author?> GetByPostIdAsync(string authorId);
    Task<IEnumerable<Author?>> GetByPostTitleAsync(string postTitle);
    Task<IEnumerable<Author?>> GetByNoOfPostsAsync(int value);
    Task<Author?> GetByEmailAsync(string email);
    Task CreateAsync(Author author);
    Task<Post> UpdateAsync(Author author);
    Task<bool> DeleteAsync(string id);
}