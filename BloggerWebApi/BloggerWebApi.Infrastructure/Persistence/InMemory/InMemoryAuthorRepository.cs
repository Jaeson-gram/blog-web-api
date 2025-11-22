using System.Globalization;
using BloggerWebApi.BloggerWebApi.Application.Interfaces;
using BloggerWebApi.BloggerWebApi.Domain.Entities;

namespace BloggerWebApi.BloggerWebApi.Infrastructure.Persistence.InMemory;

public class InMemoryAuthorRepository : IAuthorRepository
{

    private readonly InMemoryDB _db;

    public InMemoryAuthorRepository(InMemoryDB db)
    {
        _db = db;
    }
    
    public async Task<IEnumerable<Author>> GetAllAsync()
    {
        return await Task.FromResult<IEnumerable<Author>>(_db.Authors);
    }

    public async Task<Author?> GetByIdAsync(string id)
    {
        var authors = _db.Authors.FirstOrDefault(a => a.Id == Guid.Parse(id));
        return await Task.FromResult(authors);
    }

    public async Task<IEnumerable<Author?>> GetByNameAsync(string name)
    {
        var authors = _db.Authors.FindAll(a => a.Name == name);
        return await Task.FromResult(authors);
    }

    public async Task<Author?> GetByPostIdAsync(string id)
    {
        var post = _db.Posts.FirstOrDefault(p => p.Id == Guid.Parse(id));
        return await Task.FromResult(post.Author);
    }

    public async Task<IEnumerable<Author?>> GetByPostTitleAsync(string postTitle)
    {
        var posts = _db.Posts.FindAll(p => p.Title == postTitle);
        IEnumerable<Author> authors = new List<Author>();

        foreach (var post in posts)
        {
            authors.Append(post.Author);
        }

        return authors;
    }

    /// todo ->
    /// <summary>create methods for number range too so admin can get range
    /// suitable for things like awards </summary>
    public async Task<IEnumerable<Author?>> GetByNoOfPostsAsync(int value )
    {
        var authors = _db.Authors.FindAll(a => a.Posts.Count == value);
        
        return await Task.FromResult(authors);
    }

    public async Task<Author?> GetByEmailAsync(string email)
    {
        var author = _db.Authors.FirstOrDefault(a => a.Email == email);

        return await Task.FromResult(author);
    }

    public async Task CreateAsync(Author author)
    {
        throw new NotImplementedException();
    }

    public async Task<Post> UpdateAsync(Author author)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> DeleteAsync(string id)
    {
        throw new NotImplementedException();
    }
}