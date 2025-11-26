using System.Linq;
using AutoMapper;
using BloggerWebApi.BloggerWebApi.Application.DTOs;
using BloggerWebApi.BloggerWebApi.Application.Interfaces;
using BloggerWebApi.BloggerWebApi.Domain.Entities;
using BloggerWebApi.BloggerWebApi.Infrastructure.Persistence.InMemory;

namespace BloggerWebApi.BloggerWebApi.Application.Services;

public class PostService : IPostService
{
    // private readonly InMemoryPostRepository _storage;
    private readonly IPostRepository _postRepository;
    private readonly IAuthorRepository _authorRepository;
    private readonly IMapper _mapper;
    
    public PostService(IAuthorRepository _authorRepository, IPostRepository postRepository, IMapper mapper)
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

    public async Task<IEnumerable<PostSummaryDto>> GetPublishedPostsAsync(int page = 1, int pageSize = 10)
    {
        var publishedPosts = await _postRepository.GetPublishedAsync(page, pageSize);
        // IEnumerable<PostSummaryDto> posts = new List<PostSummaryDto>();
        
        return _mapper.Map<IEnumerable<PostSummaryDto>>(publishedPosts);
    }

    public async Task<PostDetailDto> GetBySlugAsync(string slug)
    {
        var posts = await _postRepository.GetBySlugAsync(slug);
        // IEnumerable<PostSummaryDto> posts = new List<PostSummaryDto>();
        
        return _mapper.Map<PostDetailDto>(posts);
    }

    async Task<PostDetailDto> IPostService.GetByIdAsync(string id)
    {
        var post = await _postRepository.GetByIdAsync(id);
        return _mapper.Map<PostDetailDto>(post);
    }

    // public async Task<Post> CreateAsync(CreatePostDto post, string authorId)
    // {
    //     throw new NotImplementedException();
    // }

    // public async Task UpdateAsync(UpdatePostDto post, string authorId)
    // {
    //     throw new NotImplementedException();
    // }

    public async Task DeleteAsync(string postId, string authorId)
    {
        var postToDelete = await _postRepository.GetByIdAsync(postId);

        if (postToDelete.Author.Id == Guid.Parse(authorId))
        {
            await _postRepository.DeleteAsync(postId);
        }
        
    }

    public async Task<PostDetailDto?> GetByIdAsync(string id)
    {
        var post = await _postRepository.GetByIdAsync(id);
        return post == null ? null : _mapper.Map<PostDetailDto>(post);
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
    
    public async Task<PostDto> CreateAsync(CreatePostDto dto, string authorId)
    {
        var post = _mapper.Map<Post>(dto);
        post.AuthorId = Guid.Parse(authorId);
        
        var createdPost = _postRepository.AddAsync(post);
        return _mapper.Map<PostDto>(createdPost);
    }
    
    public async Task<PostDto?> UpdateAsync(UpdatePostDto dto, string id, string authorId)
    {
        var post = _mapper.Map<Post>(dto);
        post.Id = Guid.Parse(id);
        post.AuthorId = Guid.Parse(authorId); 
        
        var updated = await _postRepository.UpdateAsync(post);
        return updated == null ? null : _mapper.Map<PostDto>(updated);
    }
    
    // public Task DeleteAsync(string id)
    // {
    //     return _postRepository.DeleteAsync(id);
    // }
    
    
    // utility .. mappings : ), excerpt creation, 

    private static PostSummaryDto MapToSummaryDto(Post post)
        => new PostSummaryDto()
        {
            Id = post.Id,
            Title = post.Title,
            Slug = post.Slug,
            Excerpt = post.Excerpt,
            AuthorName = post.Author.Name,
            PublishedAt = post.PublishedAt ?? post.CreatedAt,
            Tags = post.Tags,
            CommentCount = post.Comments.Count(),
        };

    private static PostDetailDto MapToDetailDto(Post post)
        => new PostDetailDto()
        {
            Id = post.Id,
            Title = post.Title,
            Slug = post.Slug,
            Content = post.Content,
            Excerpt = post.Excerpt,
            Status = post.PostStatus,
            AuthorName = post.Author.Name,
            PublishedAt = post.PublishedAt ?? post.CreatedAt,
            CreatedAt = post.CreatedAt,
            Tags = post.Tags,
            Comments = post.Comments.OrderBy(c => c.CommentedAt).ToList()
            
        };

    private static string CreatExcerpt(int length, string content)
    => length > 200 ? content[..200] + "..." : content;
    
}