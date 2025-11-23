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
    


        public Task<Post> CreateAsync(Post post, bool isPrivate) // DateTime publishTime)
        {
            // post.GetType().GetProperty("Id")?.SetValue(post, Guid.NewGuid());
            // var forExcerp = post.Content.Reverse();

            var newPost = new Post
            {
                Id = Guid.NewGuid(),
                Title = post.Title,
                Slug = string.Empty,
                Content = post.Content,
                Excerpt = (string)post.Content.Take<char>(20),
                Status = !isPrivate ? Status.Published : Status.Private,
                PublishedAt = DateTime.UtcNow, // for now
                AuthorId = post.AuthorId,
                Comments = [],
            };
            
            _db.Posts.Add(newPost);
            
            return (Task<Post>)Task.CompletedTask;
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