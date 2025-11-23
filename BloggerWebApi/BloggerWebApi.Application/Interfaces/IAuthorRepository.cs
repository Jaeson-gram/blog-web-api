using BloggerWebApi.BloggerWebApi.Application.DTOs;
using BloggerWebApi.BloggerWebApi.Domain.Entities;

namespace BloggerWebApi.BloggerWebApi.Application.Interfaces;

public interface IAuthorRepository
{
    Task<IEnumerable<Author>> GetAllAsync();
    Task<Author?> GetByIdAsync(string id);
    Task<IEnumerable<Author?>> GetByNameAsync(string name);
    Task<Author?> GetByPostIdAsync(string postId);
    Task<IEnumerable<Author?>> GetByPostTitleAsync(string postTitle);
    Task<IEnumerable<Author?>> GetByNoOfPostsAsync(int value);
    Task<Author?> GetByEmailAsync(string email);
    Task<Author> CreateAsync(AuthorDto author);
    Task<Author> UpdateAsync(Author author);
    Task<bool> DeleteAsync(string id);
}