using BloggerWebApi.BloggerWebApi.Application.Interfaces;
using BloggerWebApi.BloggerWebApi.Domain.Entities;

namespace BloggerWebApi.BloggerWebApi.Infrastructure.Persistence.InMemory;

public class InMemoryRepository : IPostRepository
{
    private int idCounter = 0;
        public List<Author> Authors { get; set; }
        public List<Post> Posts { get; set; }

        public InMemoryRepository()
        {
            // seed Authors
            Authors = new List<Author>
            {
                new Author { Id = 1, Name = "Jey Electronica", Email = "jey@inspired.com" },
                new Author { Id = 2, Name = "uk lele", Email = "uk@inspired.com" },
                new Author { Id = 3, Name = "ore precious", Email = "pre.ore@inspired.com" }
            };

            // seed Posts
            Posts = new List<Post>
            {
                // new Post {Title = "complex systems", Content = "what are complex systems? well.. let's ask the right que...", AuthorId = 1},
                new Post {Title = "the electromagnetic spectrum", Content = "when you look around you...", AuthorName = "Jey Electronica" },
                new Post {Title = "complex systems made simple", Content = "every sophisticated tech is basically simplicity in ...", AuthorName = "Jey Electronica" },
                new Post {Title = "combinatorics", Content = "it all began with a zero, and a one", AuthorName = "Jey Electronica" },
                new Post {Title = "how to make a dummy", Content = "", AuthorName = "uke lele" },

            };
            
            // seed comments
            // Comments = new List<Comment>
            // {
            //     new Comment {Content = "First post!", AuthorId = 1},
            //     new Comment {Content = "APIs made easy.", AuthorId = 2 }
            // };
        }
        
        public int GetNextId() => Interlocked.Increment(ref idCounter);
        
        public async Task<IEnumerable<Post>> GetAllAsync()
        {
            return await Task.FromResult<IEnumerable<Post>>(Posts);
        }

        public async Task<Post?> GetByIdAsync(int id)
        {
            var post = Posts.FirstOrDefault(p => p.Id == id);
            return await Task.FromResult(post);
        }

        public async Task<IEnumerable<Post?>> GetByAuthorName(string authorName)
        {
            var posts = Posts.Where(p => p.AuthorName.ToLower() == authorName.ToLower());
            return await Task.FromResult(posts);
        }

        public async Task<IEnumerable<Post?>> GetByAuthorIdAsync(int authorId)
        {
            var posts = Posts.Where(p => p.Author.Id == authorId);
            return await Task.FromResult(posts);
        }

        public Task CreateAsync(Post post)
        {
            post.GetType().GetProperty("Id")?.SetValue(post, Posts.Count + 1);
            Posts.Add(post);
            
            return Task.CompletedTask;
        }

        public Task<Post> UpdateAsync(Post post)
        {
            var existingPost = Posts.FirstOrDefault(p => p.Id == post.Id);
            if (existingPost is null)
            {
                return null;
            }
            existingPost.Update(post.Title, post.Content);
            return Task.FromResult(post);
        }

        public Task<bool> DeleteAsync(int id)
        {
            var postToDelete = Posts.FirstOrDefault(p => p.Id == id);

            if (postToDelete is null)
            {
                return null;
            }
            
            Posts.Remove(postToDelete);

            return Task.FromResult(true);
        }
}