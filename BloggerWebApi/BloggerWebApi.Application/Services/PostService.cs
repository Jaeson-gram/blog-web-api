using System.Linq;
using AutoMapper;
using BloggerWebApi.BloggerWebApi.Application.DTOs;
using BloggerWebApi.BloggerWebApi.Application.Interfaces;
using BloggerWebApi.BloggerWebApi.Domain.Entities;
using BloggerWebApi.BloggerWebApi.Infrastructure.Persistence.InMemory;

namespace BloggerWebApi.BloggerWebApi.Application.Services;

public class PostService
{
    private readonly InMemoryRepository _storage;
    private readonly IPostRepository _postRepository;
    private readonly IMapper _mapper;
    
    public PostService(InMemoryRepository storage, IPostRepository postRepository, IMapper mapper)
    {
        _storage = storage;
        _postRepository = postRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<PostDto>> GetAllAsync()
    {
        // var result = _storage.Posts.Select(p => MapToDto(p));
        // return Task.FromResult(result);
        var posts = _postRepository.GetAllAsync();
        
        return _mapper.Map<IEnumerable<PostDto>>(posts);
    }

    public async Task<PostDto?> GetByIdAsync(int id)
    {
        // var post = _storage.Posts.FirstOrDefault(p => p.Id == id);
        // return Task.FromResult(post is null ? null : MapToDto(post));

        var post = await _postRepository.GetByIdAsync(id);
        return post == null ? null : _mapper.Map<PostDto>(post);
    }
    
    // public Task<IEnumerable<PostDto>> GetByAuthorIdAsync(int authorId)
    // make it work with postdto..
    public async Task<IEnumerable<PostDto?>> GetByAuthorIdAsync(int authorId)
    {
        // Select doesn't work.. why?
        // var result = _storage.Posts.Where(p => p.Author.Id == authorId);

        // foreach (var post in result)
        // {
        //     MapToDto(post);
        // }
        // return result;
        
        var posts = await _postRepository.GetByAuthorIdAsync(authorId);
        return _mapper.Map<IEnumerable<PostDto>>(posts);
    }

    // author name..
    public async Task<IEnumerable<PostDto?>> GetByAuthorNameAsync(string authorName)
    {
        // Select doesn't work.. why?
        // var result = _storage.Posts.Where(p => p.AuthorName.ToLower() == authorName.ToLower());
    
        // foreach (var post in result)
        // {
        //     MapToDto(post);
        // }
            
        // return result;
        
        var posts = await _postRepository.GetByAuthorName(authorName);
        return _mapper.Map<IEnumerable<PostDto>>(posts);
    }
    
    public async Task<PostDto> CreateAsync(CreatePostDto dto)
    {
        // var post = new Post(dto.Title, dto.Content, dto.AuthorName)
        // {
        //     Id = _storage.GetNextId()
        // };
        // _storage.Posts.Add(post);
        //
        // return Task.FromResult(MapToDto(post));

        var post = _mapper.Map<Post>(dto);
        var createdPost = _postRepository.CreateAsync(post);
        return _mapper.Map<PostDto>(createdPost);
    }
    
    public Task<bool> UpdateAsync(int id, CreatePostDto dto)
    {
        var postToEdit = _storage.Posts.SingleOrDefault(p => p.Id == id);
        if(postToEdit is null)
        {
            return Task.FromResult(false);
        }
        
        postToEdit.Update(dto.Title, dto.Content);
        
        return Task.FromResult(true);
    }
    
    public Task<bool> DeleteAsync(int id)
    {
        var deleted = _storage.Posts.RemoveAll(p => p.Id == id);
        
        return Task.FromResult(true);

    }
    
    // method to map data to dto instead of automapper - not ready for all those setup rn
    public static PostDto MapToDto(Post post) => new PostDto
    {
        Id = post.Id,
        Title = post.Title,
        Content = post.Content,
        CreatedAt = post.CreatedAt,
        UpdatedAt = post.UpdatedAt,
        // AuthorId = post.AuthorId
        AuthorName = post.AuthorName,
        Author = post.Author
    };
}