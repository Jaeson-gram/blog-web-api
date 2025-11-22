using System.ComponentModel.DataAnnotations;

namespace BloggerWebApi.BloggerWebApi.Application.DTOs;

public class AuthorDto
{
    public string Id {get; set;}
    [Required]
    public string Name {get; set;}
    [EmailAddress]
    public string Email { get; set; }
    
    public int? NoOfPosts {get; set;}
}