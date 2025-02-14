   namespace Copilot.Domain.Entities;

   public class Dinosaur
   {
       public int Id { get; set; }
       public required string Name { get; set; }
       public required string Species { get; set; }
       public required string Sex { get; set; }
       public required string CountryOfOrigin { get; set; }
       public int NumberOfScales { get; set; }
   }

