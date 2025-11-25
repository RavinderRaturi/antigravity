using Dotted.Core;
using System.Collections.Concurrent;

namespace Dotted.Infrastructure;

public class InMemoryTodoRepository : ITodoRepository
{
    private readonly ConcurrentDictionary<int, TodoItem> _todos = new();
    private int _nextId = 1;

    public IEnumerable<TodoItem> GetAll()
    {
        return _todos.Values;
    }

    public void Add(TodoItem item)
    {
        item.Id = _nextId++;
        _todos.TryAdd(item.Id, item);
    }

    public TodoItem? GetById(int id)
    {
        _todos.TryGetValue(id, out var item);
        return item;
    }

    public void Update(TodoItem item)
    {
        if (_todos.ContainsKey(item.Id))
        {
            _todos[item.Id] = item;
        }
    }

    public void Delete(int id)
    {
        _todos.TryRemove(id, out _);
    }
}
