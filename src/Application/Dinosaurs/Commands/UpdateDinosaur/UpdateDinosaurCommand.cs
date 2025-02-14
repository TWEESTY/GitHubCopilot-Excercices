using Copilot.Application.Dinosaurs.Repositories;
using Copilot.Domain.Entities;

namespace Copilot.Application.Dinosaurs.Commands.UpdateDinosaur
{
    public record UpdateDinosaurCommand(int Id, string Name, string Species, string Sex, string CountryOfOrigin, int NumberOfScales) : IRequest;

    public class UpdateDinosaurCommandHandler : IRequestHandler<UpdateDinosaurCommand>
    {
        private readonly IDinosaurRepository _repository;

        public UpdateDinosaurCommandHandler(IDinosaurRepository repository)
        {
            _repository = repository;
        }

        public Task Handle(UpdateDinosaurCommand request, CancellationToken cancellationToken)
        {
            var dinosaur = _repository.Get(request.Id);
            Guard.Against.Null(dinosaur);

            dinosaur.Name = request.Name;
            dinosaur.Species = request.Species;
            dinosaur.Sex = request.Sex;
            dinosaur.CountryOfOrigin = request.CountryOfOrigin;
            dinosaur.NumberOfScales = request.NumberOfScales;

            _repository.Update(dinosaur);

            return Task.FromResult(Unit.Value);
        }
    }
}
