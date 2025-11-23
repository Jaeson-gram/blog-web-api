namespace BloggerWebApi.BloggerWebApi.Domain.Entities;

public class Comment
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Body { get; set; } = string.Empty;
    public string AuthorName { get; set; } = "anonymous";
    public DateTime CommentedAt { get; set; } = DateTime.UtcNow;
    public Guid PostId { get; set; }
    public Post Post { get; set; }
}