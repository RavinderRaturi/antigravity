using Dotted.Core;
using Dotted.Infrastructure;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSingleton<ITodoRepository, InMemoryTodoRepository>();
builder.Services.AddLogging();

var app = builder.Build();

// Seed initial data
var repo = app.Services.GetRequiredService<ITodoRepository>();
DataSeeder.Seed(repo);

// Configure the HTTP request pipeline.
app.UseHttpsRedirection();

app.MapGet("/todos", (ITodoRepository repo, ILogger<Program> logger) =>
{
    logger.LogInformation("Getting all todos");
    return Results.Ok(repo.GetAll());
});

app.MapPost("/todos", (TodoItem item, ITodoRepository repo, ILogger<Program> logger) =>
{
    logger.LogInformation("Adding todo: {Title}", item.Title);
    repo.Add(item);
    return Results.Created($"/todos/{item.Id}", item);
});

app.MapPut("/todos/{id}/complete", (int id, ITodoRepository repo, ILogger<Program> logger) =>
{
    var todo = repo.GetById(id);
    if (todo is null) return Results.NotFound();

    todo.IsCompleted = true;
    repo.Update(todo);
    logger.LogInformation("Completed todo: {Id}", id);
    return Results.NoContent();
});

app.MapDelete("/todos/{id}", (int id, ITodoRepository repo, ILogger<Program> logger) =>
{
    if (repo.GetById(id) is null) return Results.NotFound();
    
    repo.Delete(id);
    logger.LogInformation("Deleted todo: {Id}", id);
    return Results.NoContent();
});

app.Run();

// Make the implicit Program class public so test projects can access it
public partial class Program { }
