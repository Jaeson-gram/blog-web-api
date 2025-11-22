using BloggerWebApi.BloggerWebApi.Application.Interfaces;
using BloggerWebApi.BloggerWebApi.Domain.Entities;

namespace BloggerWebApi.BloggerWebApi.Infrastructure.Persistence.InMemory;

public class InMemoryPostRepository : IPostRepository
{
    // private int idCounter = 0;
    private readonly InMemoryDB _db;

        public InMemoryPostRepository(InMemoryDB db)
        {
            _db = db;
        }
        
        // public int GetNextId() => Interlocked.Increment(ref idCounter);
        
        public async Task<IEnumerable<Post>> GetAllAsync()
        {
            return await Task.FromResult<IEnumerable<Post>>(_db.Posts);
        }

        public async Task<Post?> GetByIdAsync(string id)
        {
            var post = _db.Posts.FirstOrDefault(p => p.Id == Guid.Parse(id));
            return await Task.FromResult(post);
        }

        public async Task<IEnumerable<Post?>> GetByAuthorName(string authorName)
        {
            var posts = _db.Posts.Where(p => p.Author.Name.ToLower() == authorName.ToLower());
            return await Task.FromResult(posts);
        }

        public async Task<IEnumerable<Post?>> GetByAuthorIdAsync(string authorId)
        {
            var posts = _db.Posts.Where(p => p.Author.Id == Guid.Parse(authorId));
            return await Task.FromResult(posts);
        }

        public Task CreateAsync(Post post)
        {
            post.GetType().GetProperty("Id")?.SetValue(post, Guid.NewGuid());
            _db.Posts.Add(post);
            
            return Task.CompletedTask;
        }

        public Task<Post> UpdateAsync(Post post)
        {
            var existingPost = _db.Posts.FirstOrDefault(p => p.Id == post.Id);
            if (existingPost is null)
            {
                return null;
            }
            existingPost.Update(post.Title, post.Content);
            return Task.FromResult(post);
        }

        public Task<bool> DeleteAsync(string id)
        {
            var postToDelete = _db.Posts.FirstOrDefault(p => p.Id == Guid.Parse(id));

            if (postToDelete is null)
            {
                return null;
            }
            
            _db.Posts.Remove(postToDelete);

            return Task.FromResult(true);
        }
}