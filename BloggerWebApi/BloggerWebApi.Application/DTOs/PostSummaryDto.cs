using BloggerWebApi.BloggerWebApi.Domain.Entities;
using BloggerWebApi.BloggerWebApi.Domain.Enums;

namespace BloggerWebApi.BloggerWebApi.Application.DTOs;

public class PostSummaryDto
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Slug { get; set; }
    public string Excerpt { get; set; }
    public PostStatus Status { get; set; }
    public string AuthorName { get; set; }
    public DateTime PublishedAt { get; set; }
    public List<string> Tags { get; set; } = new();
    public int CommentCount { get; set; } = new();
    
}