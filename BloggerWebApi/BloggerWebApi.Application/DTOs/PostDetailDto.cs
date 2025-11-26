using BloggerWebApi.BloggerWebApi.Domain.Entities;
using BloggerWebApi.BloggerWebApi.Domain.Enums;

namespace BloggerWebApi.BloggerWebApi.Application.DTOs;

public class PostDetailDto : PostSummaryDto
{
    public string Content { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public PostStatus Status { get; set; }
    public List<Comment?> Comments { get; set; } = new();
}