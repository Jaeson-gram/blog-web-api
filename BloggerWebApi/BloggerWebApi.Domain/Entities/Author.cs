namespace BloggerWebApi.BloggerWebApi.Domain.Entities;

public class Author
{
    public Guid Id {get; set;}
    public string Name {get; set;}
    public string Email { get; set; }
    
    public int? NoOfPosts {get; set;}
    
}