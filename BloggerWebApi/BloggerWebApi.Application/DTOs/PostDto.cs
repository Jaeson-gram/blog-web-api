using BloggerWebApi.BloggerWebApi.Domain.Entities;

namespace BloggerWebApi.BloggerWebApi.Application.DTOs;

public class PostDto
{
        public int Id { get; init; }
        public string Title { get; init; } = null!;
        public string Content { get; init; } = null!;
        public DateTime CreatedAt { get; init; }
        public DateTime? UpdatedAt { get; init; }
        public Author Author { get; init; } 
        public string? AuthorName { get; init; } 
        
         // Create
            // public PostDto(string title, string content)
            // {
            //     Title = title;
            //     Content = content;
            //     CreatedAt = DateTime.UtcNow;
            // }
            //
            // Read or Return
            // public PostDto(int id, string title, string content, DateTime createdAt, string authorName)
            // {
            //     Id = id;
            //     Title = title;
            //     Content = content;
            //     CreatedAt = createdAt;
            // }
}