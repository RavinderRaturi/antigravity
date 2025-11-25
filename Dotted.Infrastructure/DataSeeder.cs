using Dotted.Core;

namespace Dotted.Infrastructure;

public static class DataSeeder
{
    public static void Seed(ITodoRepository repository)
    {
        if (repository.GetAll().Any())
        {
            return;
        }

        var initialTodos = new[]
        {
            new TodoItem { Title = "Learn ASP.NET Core", IsCompleted = true },
            new TodoItem { Title = "Build a Todo App", IsCompleted = false },
            new TodoItem { Title = "Test with Postman", IsCompleted = false }
        };

        foreach (var todo in initialTodos)
        {
            repository.Add(todo);
        }
    }
}
