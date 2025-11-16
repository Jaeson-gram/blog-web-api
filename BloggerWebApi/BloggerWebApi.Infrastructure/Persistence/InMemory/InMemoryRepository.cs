using BloggerWebApi.BloggerWebApi.Application.Interfaces;
using BloggerWebApi.BloggerWebApi.Domain.Entities;

namespace BloggerWebApi.BloggerWebApi.Infrastructure.Persistence.InMemory;

public class InMemoryRepository : IPostRepository
{
    // private int idCounter = 0;
        public List<Author> Authors { get; set; }
        public List<Post> Posts { get; set; }

        public InMemoryRepository()
        {
            // seed Authors
            Authors = new List<Author>
            {
                new Author { Id = Guid.NewGuid(), Name = "Jey Electronica", Email = "jey@inspired.com" },
                new Author { Id = Guid.NewGuid(), Name = "uk lele", Email = "uk@inspired.com" },
                new Author { Id = Guid.NewGuid(), Name = "ore precious", Email = "pre.ore@inspired.com" }
            };

            // seed Posts
            Posts = new List<Post>
            {
                // new Post {Title = "complex systems", Content = "what are complex systems? well.. let's ask the right que...", AuthorId = 1},
                new Post {Title = "the electromagnetic spectrum", Content = "when you look around you, the things you see and the things you don't", 
                    Author = new Author
                    {
                        Name = "jey electronica", Email = "jey@inspired.com", Id = Guid.NewGuid(), NoOfPosts = 3
                    }},
                new Post
                {
                    Title = "complex systems made simple", Content = "every sophisticated tech is beautifully portrayed simplicity",
                    Author = new Author
                    { 
                        Name = "jey electronica", Email = "jey@inspired.com", Id = Guid.NewGuid(), NoOfPosts = 3
                    }
                },
                new Post
                {
                    Title = "combinatorics", Content = "it all began with a zero, and a one",
                    Author = new Author
                    { 
                        Name = "jey electronica", Email = "jey@inspired.com", Id = Guid.NewGuid(), NoOfPosts = 3
                    }
                },
                new Post
                {
                    Title = "how to make a dummy", Content = "genius and idiots dwell in the human mind, harnessing the right ...",
                    Author = new Author
                    { 
                        Name = "uke lele", Email = "uk@inspired.com", Id = Guid.NewGuid(), NoOfPosts = 3
                    }
                },
                new Post
                {
                    Title = "inside an engine", Content = "a machine is not unlike a hamburger",
                    Author = new Author
                    { 
                        Name = "precious ore", Email = "ore@inspired.com", Id = Guid.NewGuid(), NoOfPosts = 3
                    }
                },
            };
            
            // seed comments
            // Comments = new List<Comment>
            // {
            //     new Comment {Content = "First post!", AuthorId = 1},
            //     new Comment {Content = "APIs made easy.", AuthorId = 2 }
            // };
        }
        
        // public int GetNextId() => Interlocked.Increment(ref idCounter);
        
        public async Task<IEnumerable<Post>> GetAllAsync()
        {
            return await Task.FromResult<IEnumerable<Post>>(Posts);
        }

        public async Task<Post?> GetByIdAsync(string id)
        {
            var post = Posts.FirstOrDefault(p => p.Id == Guid.Parse(id));
            return await Task.FromResult(post);
        }

        public async Task<IEnumerable<Post?>> GetByAuthorName(string authorName)
        {
            var posts = Posts.Where(p => p.Author.Name.ToLower() == authorName.ToLower());
            return await Task.FromResult(posts);
        }

        public async Task<IEnumerable<Post?>> GetByAuthorIdAsync(string authorId)
        {
            var posts = Posts.Where(p => p.Author.Id == Guid.Parse(authorId));
            return await Task.FromResult(posts);
        }

        public Task CreateAsync(Post post)
        {
            post.GetType().GetProperty("Id")?.SetValue(post, Guid.NewGuid());
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

        public Task<bool> DeleteAsync(string id)
        {
            var postToDelete = Posts.FirstOrDefault(p => p.Id == Guid.Parse(id));

            if (postToDelete is null)
            {
                return null;
            }
            
            Posts.Remove(postToDelete);

            return Task.FromResult(true);
        }
}