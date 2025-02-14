using Copilot.Domain.Entities;

namespace Copilot.Application.Dinosaurs.Repositories;

public interface IDinosaurRepository
{
    Dinosaur? Get(int id);
    List<Dinosaur> GetList();
    Dinosaur Create(Dinosaur entity);
    Dinosaur Update(Dinosaur entity);
    void Delete(int id);
}
