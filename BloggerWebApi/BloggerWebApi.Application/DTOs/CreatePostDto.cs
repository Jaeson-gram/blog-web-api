using System.ComponentModel.DataAnnotations;
using BloggerWebApi.BloggerWebApi.Domain.Entities;

namespace BloggerWebApi.BloggerWebApi.Application.DTOs;

public class CreatePostDto
{
    [Required]
    [MaxLength(120)]
    public string Title { get; init; } = null!;
    [Required]
    [MinLength(200)]
    public string Content { get; init; } = null!;
    public int? AuthorId { get; init; }
    public string? AuthorName { get; init; }
    // public Author Author { get; init; } 
}