using Copilot.Application.Common.Models;
using Copilot.Application.TodoItems.Repositories;
using Copilot.Domain.Entities;

namespace Copilot.Application.TodoItems.Queries.GetAllTitlesTodoItems;

public record GetAllTitlesTodoItemsQuery : IRequest<string>;

public class GetAllTitlesTodoItemsQueryHandler(ITodoItemRepository todoItemRepository) : IRequestHandler<GetAllTitlesTodoItemsQuery, string>
{
    private readonly ITodoItemRepository _todoItemRepository = todoItemRepository;

    public Task<string> Handle(GetAllTitlesTodoItemsQuery request, CancellationToken cancellationToken)
    {
        PaginatedList<TodoItem> paginatedList = _todoItemRepository.GetList();
    
        return Task.FromResult(string.Join(";",paginatedList.Items.Select(x => x.Title)));
    }
}
