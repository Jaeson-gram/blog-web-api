// using BloggerWebApi.BloggerWebApi.Application.DTOs;
// using BloggerWebApi.BloggerWebApi.Domain.Entities;
// using BloggerWebApi.BloggerWebApi.Infrastructure.Persistence.InMemory;
//
// namespace BloggerWebApi.BloggerWebApi.Application.Services;
//
// public class AuthorService
// {
//      /// <summary>
//      ///  what's the difference between this and a primary constructor?
//      /// </summary>
//     private readonly InMemoryRepository _storage; 
//
//     public AuthorService(InMemoryRepository storage)
//     {
//         _storage = storage;
//     }
//
//     public Task<IEnumerable<AuthorDto>> GetAll()
//     {
//         var authors = _storage.Authors.Select(author => MapToDto(author));
//         return Task.FromResult(authors);
//     }
//     
//     public Task<AuthorDto?> GetById(int id)
//     {
//         var author = _storage.Authors.FirstOrDefault(author => author.Id == id);
//         return Task.FromResult(author is null ? null : MapToDto(author));
//     }
//     
//     public Task<AuthorDto?> GetByName(string name)
//     {
//         var author = _storage.Authors.FirstOrDefault(author => author.Name == name);
//         return Task.FromResult(author is null ? null : MapToDto(author));
//     }
//
//     // perhaps can be called upon wherever it is needed i guess.
//     // just leave it here.
//     public int? GetNumberOfPosts(int authorId)
//     {
//         var author = _storage.Authors.FirstOrDefault(author => author.Id == authorId);
//
//         return author?.NoOfPosts;
//     }
//     
//     
//
//     // method to map data to dto instead of automapper - not ready for all those setup rn
//     private static AuthorDto MapToDto(Author author)
//     {
//         return new AuthorDto
//         {
//             Id = author.Id,
//             Name = author.Name,
//             Email = author.Email,
//             NoOfPosts = author.NoOfPosts,
//         };
//     }
// }