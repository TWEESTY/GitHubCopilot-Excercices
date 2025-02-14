using Copilot.Application.Dinosaurs.Repositories;

namespace Copilot.Application.Dinosaurs.Commands.DeleteDinosaur;

public record DeleteDinosaurCommand(int Id) : IRequest;

public class DeleteDinosaurCommandHandler : IRequestHandler<DeleteDinosaurCommand>
{
    private readonly IDinosaurRepository _repository;

    public DeleteDinosaurCommandHandler(IDinosaurRepository repository)
    {
        _repository = repository;
    }

    public Task Handle(DeleteDinosaurCommand request, CancellationToken cancellationToken)
    {
        _repository.Delete(request.Id);
        return Task.FromResult(Unit.Value);
    }
}
