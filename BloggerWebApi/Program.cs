using BloggerWebApi.BloggerWebApi.Application.Interfaces;
using BloggerWebApi.BloggerWebApi.Application.Mappings;
using BloggerWebApi.BloggerWebApi.Application.Services;
using BloggerWebApi.BloggerWebApi.Domain.Entities;
using BloggerWebApi.BloggerWebApi.Infrastructure.Persistence.InMemory;
// using AutoMapper;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSingleton<PostService>();
builder.Services.AddSingleton<InMemoryPostRepository>();
builder.Services.AddSingleton<InMemoryDB>();

builder.Services.AddScoped<IPostRepository, InMemoryPostRepository>();

 builder.Services.AddAutoMapper(typeof(MapProfile));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();