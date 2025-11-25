namespace Dotted.Core;

public interface ITodoRepository
{
    IEnumerable<TodoItem> GetAll();
    void Add(TodoItem item);
    TodoItem? GetById(int id);
    void Update(TodoItem item);
    void Delete(int id);
}
