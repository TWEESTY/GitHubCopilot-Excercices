
using Copilot.Application.Common.Models;
using Copilot.Domain.Entities;

namespace Copilot.Application.TodoItems.Repositories;

public interface ITodoItemRepository
{
    public TodoItem? Get(int id);
    public PaginatedList<TodoItem> GetList(int? pageNumber = null, int? pageSize = null);

    public TodoItem Create(TodoItem entity);
    public TodoItem Update(TodoItem entity);
    public void Delete(int id);
}
