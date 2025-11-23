using AutoMapper;
using BloggerWebApi.BloggerWebApi.Application.DTOs;
using BloggerWebApi.BloggerWebApi.Application.Interfaces;
using BloggerWebApi.BloggerWebApi.Domain.Entities;
using BloggerWebApi.BloggerWebApi.Infrastructure.Persistence.InMemory;

namespace BloggerWebApi.BloggerWebApi.Application.Services;

public class AuthorService 
{
    private readonly IMapper _mapper;
    private readonly IAuthorRepository _authorRepository;

     /// <summary>
     ///  what's the difference between this and a primary constructor?
     /// </summary>

    
    

    // method to map data to dto instead of automapper - not ready for all those setup rn
    // private static AuthorDto MapToDto(Author author)
    // {
    //     return new AuthorDto
    //     {
    //         Id = author.Id.ToString(),
    //         Name = author.Name,
    //         Email = author.Email,
    //         NoOfPosts = author.Posts.Count,
    //     };
    // }

    public AuthorService(IAuthorRepository authorRepository, IMapper mapper)
    {
        _mapper = mapper;
        _authorRepository = authorRepository;
    }
     
    public async Task<IEnumerable<Author>> GetAllAsync()
    {
        return await _authorRepository.GetAllAsync();
    }

    public async Task<Author?> GetByIdAsync(string id)
    {
        return await _authorRepository.GetByIdAsync(id);
    }

    public async Task<IEnumerable<Author?>> GetByNameAsync(string name)
    {
        return await _authorRepository.GetByNameAsync(name);
    }

    public async Task<Author?> GetByPostIdAsync(string postId)
    {
        return await _authorRepository.GetByPostIdAsync(postId);
    }

    public async Task<IEnumerable<Author?>> GetByPostTitleAsync(string postTitle)
    {
        return await _authorRepository.GetByPostTitleAsync(postTitle);
    }

    public async Task<IEnumerable<Author?>> GetByNoOfPostsAsync(int value)
    {
        return await _authorRepository.GetByNoOfPostsAsync(value);
    }

    public async Task<Author?> GetByEmailAsync(string email)
    {
        return await _authorRepository.GetByEmailAsync(email);
    }

    public async Task<Author> CreateAsync(AuthorDto author)
    {
        return await _authorRepository.CreateAsync(author);
    }

    public async Task<Author> UpdateAsync(Author author)
    {
        return await _authorRepository.UpdateAsync(author);
    }

    public async Task<bool> DeleteAsync(string id)
    {
        return await _authorRepository.DeleteAsync(id);
    }
}