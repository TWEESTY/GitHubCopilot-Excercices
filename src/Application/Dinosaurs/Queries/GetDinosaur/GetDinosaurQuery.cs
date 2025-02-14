using Copilot.Application.Dinosaurs.Repositories;
using Copilot.Domain.Entities;

namespace Copilot.Application.Dinosaurs.Queries.GetDinosaur
{
    public record GetDinosaurQuery(int Id) : IRequest<Dinosaur?>;
    
    public class GetDinosaurQueryHandler : IRequestHandler<GetDinosaurQuery, Dinosaur?>
    {
        private readonly IDinosaurRepository _repository;

        public GetDinosaurQueryHandler(IDinosaurRepository repository)
        {
            _repository = repository;
        }

        public Task<Dinosaur?> Handle(GetDinosaurQuery request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_repository.Get(request.Id));
        }
    }
}
