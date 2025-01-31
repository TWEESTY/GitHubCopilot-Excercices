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

    public async Task Handle(UpdateTodoItemCommand request, CancellationToken cancellationToken)
    {
        var entity = await _todoItemRepository.GetAsync(request.Id);

        Guard.Against.Null(entity);

        entity.Title = request.Title;
        entity.Done = request.Done;

        await _todoItemRepository.UpdateAsync(entity);
    }
}
