using BloggerWebApi.BloggerWebApi.Application.DTOs;
using BloggerWebApi.BloggerWebApi.Application.Interfaces;
using BloggerWebApi.BloggerWebApi.Domain.Entities;
using BloggerWebApi.BloggerWebApi.Domain.Enums;

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
    
        public async Task<Post?> GetBySlugAsync(string slug)
        {
            var post = _db.Posts.FirstOrDefault(p => p.Slug.Equals(slug, StringComparison.OrdinalIgnoreCase));

            return post;
        }

        public async Task<IEnumerable<Post>?> GetPublishedAsync(int? page, int? pageSize)
        {
            // var query = _db.Posts.AsQueryable();
            var query = _db.Posts.Where(p => p.PostStatus == PostStatus.Published)
                .OrderByDescending(p => p.PublishedAt ?? p.CreatedAt);

            if (page.HasValue)
            {
                query = (IOrderedEnumerable<Post>)query.Skip((page.Value - 1) * pageSize.Value).Take(pageSize.Value);
            }

            return await Task.FromResult(query.AsEnumerable());
        }

        public async Task<IEnumerable<Post>?> GetDraftsByAuthorIdAsync(string authorId)
        {
            var posts = _db.Posts.Where(p =>
                p.AuthorId == Guid.Parse(authorId) && p.PostStatus == PostStatus.Draft).OrderByDescending(p => p.CreatedAt);

            return posts.AsEnumerable();
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
    
        public Task AddAsync(Post post)
        {
            _db.Posts.Add(post);
            return Task.CompletedTask;
        }

        public Task<Post> UpdateAsync(Post post)
        {
            var existingPost = _db.Posts.FirstOrDefault(p => p.Id == post.Id);
            if (existingPost is not null)
            {
                _db.Posts.Remove(existingPost);
            }

            _db.Posts.Add(post);
            return Task.FromResult(post);
            // existingPost.Update(post.Title, post.Content);
        }

        public Task<bool> DeleteAsync(string id)
        {
            var postToDelete = _db.Posts.FirstOrDefault(p => p.Id == Guid.Parse(id));

            if (postToDelete is null)
            {
                return Task.FromResult(false);
            }
            
            _db.Posts.Remove(postToDelete);

            return Task.FromResult(true);
        }
}