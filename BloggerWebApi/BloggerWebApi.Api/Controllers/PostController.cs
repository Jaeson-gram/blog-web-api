using System.ComponentModel.DataAnnotations;
using BloggerWebApi.BloggerWebApi.Application.DTOs;
using BloggerWebApi.BloggerWebApi.Application.Services;
// using BloggerWebApi.BloggerWebApi.Application.Services;
using BloggerWebApi.BloggerWebApi.Domain.Entities;
using BloggerWebApi.BloggerWebApi.Infrastructure.Persistence.InMemory;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.AspNetCore.Http.StatusCodes;

namespace BloggerWebApi.BloggerWebApi.Api.Controllers;

[Route("api/[controller]")]
[ApiController]

public class PostController : ControllerBase
{
    // private readonly InMemoryRepository _memoryRepository;
    private readonly PostService _postService;

    public PostController(PostService postService)
    {
        // _memoryRepository = memoryRepository;
        _postService = postService;
    }

    [HttpGet("AllPosts")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IEnumerable<Post>>> GetAllPosts()
    {
        // var posts = _memoryRepository.Posts.ToList();
        
        var posts = _postService.GetAllAsync();
        return Ok(posts);
    }

    [HttpGet("GetById/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<PostDto?>> GetById(string id)
    {
        var post = _postService.GetByIdAsync(id);
        return Ok(post);
    }

    [HttpGet("getByAuthorId/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<PostDto?>> GetByAuthorIdAsync(string id)
    {
        var post = _postService.GetByAuthorIdAsync(id);
        return Ok(post);
    }
    
    [HttpGet("GetByAuthorName")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<PostDto?>> GetByAuthorNameAsync(string authorName)
    {
        var post = _postService.GetByAuthorNameAsync(authorName);
        return Ok(post);
    }

    [HttpPost("CreatePost")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<PostDto>> CreatePost([FromBody] CreatePostDto postDto)
    {
        if (!ModelState.IsValid)
        {
            return ValidationProblem(ModelState);
        }

        var created = await _postService.CreateAsync(postDto);
        return CreatedAtRoute(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPut("Update")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Update(string id, [FromBody] CreatePostDto postDto)
    {
        if (!ModelState.IsValid)
        {
            return ValidationProblem(ModelState);
        }
        
        var found = await _postService.GetByIdAsync(id);
        
        if (found == null)
        {
            return NotFound("no post with that id was found");
        }
        
        var updated = await _postService.UpdateAsync(id, postDto);
        
        return StatusCode(StatusCodes.Status500InternalServerError);
    }

    [HttpDelete("Delete/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Delete(string id)
    {
        var deleted =  _postService.DeleteAsync(id);

        return NoContent();
    }

}