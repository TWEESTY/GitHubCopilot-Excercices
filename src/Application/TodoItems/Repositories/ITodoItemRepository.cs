
using Copilot.Application.Common.Models;
using Copilot.Domain.Entities;

namespace Copilot.Application.TodoItems.Repositories;

public interface ITodoItemRepository
{
    public Task<TodoItem?> GetAsync(int id);
    public Task<PaginatedList<TodoItem>> GetListAsync(int? pageNumber, int? pageSize);

    public Task<TodoItem> CreateAsync(TodoItem entity);
    public Task<TodoItem> UpdateAsync(TodoItem entity);
    public Task DeleteAsync(int id);
}
