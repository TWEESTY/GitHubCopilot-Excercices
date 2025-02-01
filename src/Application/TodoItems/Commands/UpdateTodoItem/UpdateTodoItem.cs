using Copilot.Application.TodoItems.Repositories;

namespace Copilot.Application.TodoItems.Commands.UpdateTodoItem;

public record UpdateTodoItemCommand : IRequest
{
    public int Id { get; init; }

    public string? Title { get; init; }

    public bool Done { get; init; }
}

public class UpdateTodoItemCommandHandler(ITodoItemRepository todoItemRepository) : IRequestHandler<UpdateTodoItemCommand>
{
    private readonly ITodoItemRepository _todoItemRepository = todoItemRepository;

    public Task Handle(UpdateTodoItemCommand request, CancellationToken cancellationToken)
    {
        var entity = _todoItemRepository.Get(request.Id);

        Guard.Against.Null(entity);

        entity.Title = request.Title;
        entity.Done = request.Done;

        _todoItemRepository.Update(entity);

        return Task.CompletedTask;
    }
}
