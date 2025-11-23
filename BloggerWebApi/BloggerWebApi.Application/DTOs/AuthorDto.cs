using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace BloggerWebApi.BloggerWebApi.Application.DTOs;

public class AuthorDto
{
    [Required]
    public string Name {get; set;}
    [EmailAddress]
    public string Email { get; set; }
    
    // public string Bio {get; set;}
}