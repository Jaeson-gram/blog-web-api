using BloggerWebApi.BloggerWebApi.Domain.Entities;

namespace BloggerWebApi.BloggerWebApi.Infrastructure.Persistence.InMemory;

public class InMemoryDB
{
    public List<Author> Authors { get; set; } = new();
    public List<Post> Posts { get; set; } = new();
    public List<Comment> Comments { get; set; } = new();

    public InMemoryDB()
    {
        var jey = new Author { Id = Guid.NewGuid(), Name = "Jey Electronica", Email = "jey@inspired.com" };
        var uk = new Author { Id = Guid.NewGuid(), Name = "uk lele", Email = "uk@inspired.com" };
        var precious = new Author { Id = Guid.NewGuid(), Name = "ore precious", Email = "pre.ore@inspired.com" };
        
        Authors.Add(jey);
        Authors.Add(uk);
        Authors.Add(precious);
        
        Posts.AddRange(new[]
        {
            new Post {Title = "the electromagnetic spectrum", Content = "when you look around you, the things you see and the things you don't", 
                Author = new Author
                {
                    Name = "jey electronica", Email = "jey@inspired.com", Id = Guid.NewGuid(), // Posts = null
                }},
            new Post
            {
                Title = "complex systems made simple", Content = "every sophisticated tech is beautifully portrayed simplicity",
                Author = new Author
                { 
                    Name = "jey electronica", Email = "jey@inspired.com", Id = Guid.NewGuid(), // Posts = null,
                }
            },
            new Post
            {
                Title = "combinatorics", Content = "it all began with a zero, and a one",
                Author = new Author
                { 
                    Name = "jey electronica", Email = "jey@inspired.com", Id = Guid.NewGuid(), // Posts = null
                }
            },
            new Post
            {
                Title = "how to make a dummy", Content = "genius and idiots dwell in the human mind, harnessing the right ...",
                Author = new Author
                { 
                    Name = "uke lele", Email = "uk@inspired.com", Id = Guid.NewGuid(), // Posts = null
                }
            },
            new Post
            {
                Title = "inside an engine", Content = "a machine is not unlike a hamburger",
                Author = new Author
                { 
                    Name = "precious ore", Email = "ore@inspired.com", Id = Guid.NewGuid(), // NoOfPosts = null
                }
            },
        });
    }
}