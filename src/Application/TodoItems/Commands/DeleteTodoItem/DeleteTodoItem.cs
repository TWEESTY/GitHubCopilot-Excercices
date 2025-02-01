using Copilot.Application.TodoItems.Repositories;
using Copilot.Domain.Events;

namespace Copilot.Application.TodoItems.Commands.DeleteTodoItem;

public record DeleteTodoItemCommand(int Id) : IRequest;

public class DeleteTodoItemCommandHandler(ITodoItemRepository todoItemRepository) : IRequestHandler<DeleteTodoItemCommand>
{
    private readonly ITodoItemRepository _todoItemRepository = todoItemRepository;

    public Task Handle(DeleteTodoItemCommand request, CancellationToken cancellationToken)
    {
        Guard.Against.NotFound(request.Id, nameof(request.Id));

         _todoItemRepository.Delete(request.Id);

        return Task.CompletedTask;
    }

}
