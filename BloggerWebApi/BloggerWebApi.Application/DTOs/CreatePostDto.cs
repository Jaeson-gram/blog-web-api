using System.ComponentModel.DataAnnotations;
using BloggerWebApi.BloggerWebApi.Domain.Entities;
using BloggerWebApi.BloggerWebApi.Domain.Enums;

namespace BloggerWebApi.BloggerWebApi.Application.DTOs;

public class CreatePostDto
{
    [Required]
    [MaxLength(120)]
    public string Title { get; init; } = null!;
    [Required]
    [MinLength(200)]
    public string Content { get; init; } = null!;
    public string? Excerpt { get; init; }
    public PostStatus Status { get; init; } = PostStatus.Draft; 
    public Guid AuthorId { get; init; }
    public List<String> Tags { get; init; } = null!;
    // public string? AuthorName { get; init; }
    // public Author Author { get; init; } 
}