using Copilot.Application.Common.Models;
using Copilot.Application.TodoItems.Repositories;
using Copilot.Domain.Entities;

namespace Copilot.Infrastructure.Repositories;
public class TodoItemRepository : ITodoItemRepository
{
    public Task<TodoItem?> GetAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<TodoItem> CreateAsync(TodoItem entity)
    {
        throw new NotImplementedException();
    }
    public Task<TodoItem> UpdateAsync(TodoItem entity)
    {
        throw new NotImplementedException();
    }
    public Task DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }

    Task<PaginatedList<TodoItem>> ITodoItemRepository.GetListAsync(int? pageNumber, int? pageSize)
    {
        throw new NotImplementedException();
    }
}
