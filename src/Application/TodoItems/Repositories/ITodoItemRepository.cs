
using Copilot.Application.Common.Models;
using Copilot.Domain.Entities;

namespace Copilot.Application.TodoItems.Repositories;

public interface ITodoItemRepository
{
    public TodoItem? Get(int id);
    public PaginatedList<TodoItem> GetList(int? pageNumber, int? pageSize);

    public TodoItem Create(TodoItem entity);
    public TodoItem Update(TodoItem entity);
    public void Delete(int id);
}
