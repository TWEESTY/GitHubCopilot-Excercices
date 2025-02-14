   using Copilot.Application.Dinosaurs.Repositories;
   using Copilot.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Copilot.Infrastructure.Repositories
   {
       public class DinosaurRepository(List<Dinosaur> dinosaurs) : IDinosaurRepository
       {
           private readonly List<Dinosaur> _dinosaurs = dinosaurs;

           public Dinosaur? Get(int id)
           {
               return _dinosaurs.FirstOrDefault(x => x.Id == id);
           }

           public List<Dinosaur> GetList()
           {
               return _dinosaurs;
           }

           public Dinosaur Create(Dinosaur entity)
           {
               entity.Id = _dinosaurs.Max(x => x.Id) + 1;
               _dinosaurs.Add(entity);
               return entity;
           }

           public Dinosaur Update(Dinosaur entity)
           {
               Dinosaur oldDinosaur = _dinosaurs.First(x => x.Id == entity.Id);
               _dinosaurs.Remove(oldDinosaur);
               _dinosaurs.Add(entity);
               return entity;
           }

           public void Delete(int id)
           {
               _dinosaurs.Remove(_dinosaurs.Single(x => x.Id == id));
           }

           public void DoSomething(string parameter)
            {
                string query = "SELECT * FROM Dinosaurs WHERE Name = " + parameter;
                var context = new DbContext(new DbContextOptionsBuilder().Options);
                var result = context.Database.ExecuteSqlRaw(query);
        }
    }
   }
