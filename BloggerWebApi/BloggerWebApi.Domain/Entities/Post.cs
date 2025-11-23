using BloggerWebApi.BloggerWebApi.Domain.Enums;

namespace BloggerWebApi.BloggerWebApi.Domain.Entities;

public class Post
{
    public Guid Id {get; set;}
    public string Title {get; set;}
    public string Slug {get; set;}
    public string Content { get; set;}
    public string Excerpt {get; set;}
    public Status Status {get; set;}
    public DateTime CreatedAt {get; set;} = DateTime.UtcNow;
    public DateTime PublishedAt {get; set;}
    public DateTime UpdatedAt {get; set;}
    public Guid AuthorId { get; set; }
    public Author Author {get; set;}
    public List<Comment> Comments { get; set; } = new();
    
    // todo -> will add tags later
    // public List<string> Tags { get; set; } = new();

    public Post()
    {
        
    }

    public Post(string title, string content, string authorName)
    {
        if (string.IsNullOrWhiteSpace(title))
        {
            throw new ArgumentException("title is required.");
        }
        if (title.Length > 120) 
        {
            throw new ArgumentException("title is too long.");
        }
        
        if (string.IsNullOrWhiteSpace(content))
        {
            throw new ArgumentException("content is required.");
        }
        if (content.Length < 60) 
        {
            throw new ArgumentException("content must be above 60 characters.");
        }

        title = Title;
        content = Content;
        CreatedAt = DateTime.UtcNow;
        Author.Name = authorName;
        // Author = author;
    }
    
    public void Update(string title, string content)
    {
        Title = title;
        Content = content;
        UpdatedAt = DateTime.UtcNow;
    }
}