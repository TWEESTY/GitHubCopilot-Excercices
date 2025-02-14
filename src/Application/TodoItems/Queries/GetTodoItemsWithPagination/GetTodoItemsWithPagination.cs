using Copilot.Application.Common.Mappings;
using Copilot.Application.Common.Models;
using Copilot.Application.TodoItems.Repositories;
using Copilot.Domain.Entities;
namespace Copilot.Application.TodoItems.Queries.GetTodoItemsWithPagination;

public record GetTodoItemsWithPaginationQuery : IRequest<PaginatedList<TodoItemBriefDto>>
{
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 10;
}

public class GetTodoItemsWithPaginationQueryHandler(ITodoItemRepository todoItemRepository, IMapper mapper) : IRequestHandler<GetTodoItemsWithPaginationQuery, PaginatedList<TodoItemBriefDto>>
{
    private readonly ITodoItemRepository _todoItemRepository = todoItemRepository;
    private readonly IMapper _mapper = mapper;

    public async Task<PaginatedList<TodoItemBriefDto>> Handle(GetTodoItemsWithPaginationQuery request, CancellationToken cancellationToken)
    {
        var validator = new GetTodoItemsWithPaginationQueryValidator();
        await validator.ValidateAndThrowAsync(request, cancellationToken);

        PaginatedList<TodoItem> todoItems = _todoItemRepository.GetList(request.PageNumber, request.PageSize);

        return new PaginatedList<TodoItemBriefDto>(
            todoItems.Items.Select(x => _mapper.Map<TodoItemBriefDto>(x)).ToList(),
            todoItems.TotalCount, 
            request.PageNumber,
            request.PageSize
        );

    }
}
