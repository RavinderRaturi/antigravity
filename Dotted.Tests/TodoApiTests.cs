using System.Net;
using System.Net.Http.Json;
using Dotted.Core;
using Microsoft.AspNetCore.Mvc.Testing;

namespace Dotted.Tests;

public class TodoApiTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;

    public TodoApiTests(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }

    [Fact]
    public async Task GetTodos_ReturnsOkAndEmptyList_WhenNoTodos()
    {
        var client = _factory.CreateClient();
        var response = await client.GetAsync("/todos");

        response.EnsureSuccessStatusCode();
        var todos = await response.Content.ReadFromJsonAsync<List<TodoItem>>();
        Assert.NotNull(todos);
    }

    [Fact]
    public async Task PostTodo_ReturnsCreatedAndItem()
    {
        var client = _factory.CreateClient();
        var newItem = new TodoItem { Title = "Test Todo" };

        var response = await client.PostAsJsonAsync("/todos", newItem);

        response.EnsureSuccessStatusCode();
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        var createdItem = await response.Content.ReadFromJsonAsync<TodoItem>();
        Assert.NotNull(createdItem);
        Assert.Equal("Test Todo", createdItem.Title);
        Assert.True(createdItem.Id > 0);
    }

    [Fact]
    public async Task PutTodo_CompletesItem()
    {
        var client = _factory.CreateClient();
        var newItem = new TodoItem { Title = "To Complete" };
        var createResponse = await client.PostAsJsonAsync("/todos", newItem);
        var createdItem = await createResponse.Content.ReadFromJsonAsync<TodoItem>();

        var response = await client.PutAsync($"/todos/{createdItem!.Id}/complete", null);

        response.EnsureSuccessStatusCode();
        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
    }

    [Fact]
    public async Task DeleteTodo_RemovesItem()
    {
        var client = _factory.CreateClient();
        var newItem = new TodoItem { Title = "To Delete" };
        var createResponse = await client.PostAsJsonAsync("/todos", newItem);
        var createdItem = await createResponse.Content.ReadFromJsonAsync<TodoItem>();

        var response = await client.DeleteAsync($"/todos/{createdItem!.Id}");

        response.EnsureSuccessStatusCode();
        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
    }
}
