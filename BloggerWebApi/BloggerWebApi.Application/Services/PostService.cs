using System.Linq;
using AutoMapper;
using BloggerWebApi.BloggerWebApi.Application.DTOs;
using BloggerWebApi.BloggerWebApi.Application.Interfaces;
using BloggerWebApi.BloggerWebApi.Domain.Entities;
using BloggerWebApi.BloggerWebApi.Infrastructure.Persistence.InMemory;

namespace BloggerWebApi.BloggerWebApi.Application.Services;

public class PostService
{
    // private readonly InMemoryPostRepository _storage;
    private readonly IPostRepository _postRepository;
    private readonly IMapper _mapper;
    
    public PostService(InMemoryPostRepository storage, IPostRepository postRepository, IMapper mapper)
    {
        // _storage = storage;
        _postRepository = postRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<PostDto>> GetAllAsync()
    {
        var posts = await _postRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<PostDto>>(posts);
    }

    public async Task<PostDto?> GetByIdAsync(string id)
    {
        var post = await _postRepository.GetByIdAsync(id);
        return post == null ? null : _mapper.Map<PostDto>(post);
    }
    
    public async Task<IEnumerable<PostDto?>> GetByAuthorIdAsync(string authorId)
    {
        var posts = await _postRepository.GetByAuthorIdAsync(authorId);
        return _mapper.Map<IEnumerable<PostDto>>(posts);
    }
    
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
        var post = _mapper.Map<Post>(dto);
        var createdPost = _postRepository.CreateAsync(post);
        return _mapper.Map<PostDto>(createdPost);
    }
    
    public async Task<PostDto?> UpdateAsync(string id, CreatePostDto dto)
    {
        var post = _mapper.Map<Post>(dto);
        post.Id = Guid.Parse(id);
        
        var updated = await _postRepository.UpdateAsync(post);
        return updated == null ? null : _mapper.Map<PostDto>(updated);
    }
    
    public Task DeleteAsync(string id)
    {
        return _postRepository.DeleteAsync(id);
    }
}