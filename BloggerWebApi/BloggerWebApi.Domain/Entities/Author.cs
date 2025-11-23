namespace BloggerWebApi.BloggerWebApi.Domain.Entities;

public class Author
{
    public Guid Id {get; set;}
    public string Name {get; set;}
    public string Email { get; set; }
    public string? Bio { get; set; }
    public List<Post> Posts { get; set; } = new();
    public int NumberOfPosts {get; set;}
}