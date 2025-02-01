using Copilot.Application.TodoItems.Repositories;
using Copilot.Domain.Enums;

namespace Copilot.Application.TodoItems.Commands.UpdateTodoItemDetail;

public record UpdateTodoItemDetailCommand : IRequest
{
    public int Id { get; init; }

    public PriorityLevel Priority { get; init; }

    public string? Note { get; init; }
}

public class UpdateTodoItemDetailCommandHandler(ITodoItemRepository todoItemRepository) : IRequestHandler<UpdateTodoItemDetailCommand>
{
    private readonly ITodoItemRepository _todoItemRepository = todoItemRepository;

    public Task Handle(UpdateTodoItemDetailCommand request, CancellationToken cancellationToken)
    {
        var entity = _todoItemRepository.Get(request.Id);

        Guard.Against.NotFound(request.Id, entity);

        entity.Priority = request.Priority;
        entity.Note = request.Note;

        return Task.CompletedTask;
    }
}
