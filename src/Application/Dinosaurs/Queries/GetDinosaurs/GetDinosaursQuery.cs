using Copilot.Application.Dinosaurs.Repositories;
using Copilot.Domain.Entities;

namespace Copilot.Application.Dinosaurs.Queries.GetDinosaurs;

public record GetDinosaursQuery : IRequest<List<Dinosaur>>;

public class GetDinosaursQueryHandler : IRequestHandler<GetDinosaursQuery, List<Dinosaur>>
{
    private readonly IDinosaurRepository _repository;

    public GetDinosaursQueryHandler(IDinosaurRepository repository)
    {
        _repository = repository;
    }

    public Task<List<Dinosaur>> Handle(GetDinosaursQuery request, CancellationToken cancellationToken)
    {
        return Task.FromResult(_repository.GetList());
    }
}
