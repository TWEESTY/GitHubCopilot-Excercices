using Copilot.Application.Dinosaurs.Repositories;
using Copilot.Domain.Entities;

namespace Copilot.Application.Dinosaurs.Commands.CreateDinosaur
{
    public record CreateDinosaurCommand(string Name, string Species, string Sex, string CountryOfOrigin, int NumberOfScales) : IRequest<Dinosaur>;

    public class CreateDinosaurCommandHandler : IRequestHandler<CreateDinosaurCommand, Dinosaur>
    {
        private readonly IDinosaurRepository _repository;

        public CreateDinosaurCommandHandler(IDinosaurRepository repository)
        {
            _repository = repository;
        }

        public Task<Dinosaur> Handle(CreateDinosaurCommand request, CancellationToken cancellationToken)
        {
            var dinosaur = new Dinosaur
            {
                Name = request.Name,
                Species = request.Species,
                Sex = request.Sex,
                CountryOfOrigin = request.CountryOfOrigin,
                NumberOfScales = request.NumberOfScales
            };

            return Task.FromResult(_repository.Create(dinosaur));
        }
    }
}
