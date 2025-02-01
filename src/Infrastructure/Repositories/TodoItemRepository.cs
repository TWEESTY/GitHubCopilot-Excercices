using Copilot.Application.Common.Models;
using Copilot.Application.TodoItems.Repositories;
using Copilot.Domain.Entities;

namespace Copilot.Infrastructure.Repositories;
public class TodoItemRepository(List<TodoItem> todoItems) : ITodoItemRepository
{
    private readonly List<TodoItem> _todoItems = todoItems;

    public TodoItem? Get(int id)
    {
        return _todoItems.FirstOrDefault(x => x.Id == id);
    }

    public TodoItem Create(TodoItem entity)
    {
        entity.Id = _todoItems.Max(x => x.Id) + 1;
        _todoItems.Add(entity);
        return entity;
    }
    public TodoItem Update(TodoItem entity)
    {
       TodoItem oldTodoItem = _todoItems.First(x => x.Id == entity.Id);
       _todoItems.Remove(oldTodoItem);
       _todoItems.Add(entity);
       return entity;
    }
    public void Delete(int id)
    {
        _todoItems.Remove(_todoItems.Single(x => x.Id == id));
    }

    public PaginatedList<TodoItem> GetList(int? pageNumber, int? pageSize)
    {
        List<TodoItem> result = [.. _todoItems];
        if(pageSize is not null && pageNumber is not null){
           result = _todoItems
            .Take(pageSize.Value)
            .Skip((pageNumber.Value-1) * pageSize.Value)
            .ToList();
        }

        return new PaginatedList<TodoItem>(result, _todoItems.Count, pageNumber ?? 0, pageSize ?? _todoItems.Count);
    }
}
