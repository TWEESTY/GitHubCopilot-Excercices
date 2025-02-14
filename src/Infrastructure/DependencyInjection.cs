using System.IO;
using Copilot.Application.Dinosaurs.Repositories;
using Copilot.Application.TodoItems.Repositories;
using Copilot.Domain.Constants;
using Copilot.Domain.Entities;
using Copilot.Domain.Enums;
using Copilot.Infrastructure.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static void AddInfrastructureServices(this IHostApplicationBuilder builder)
    {
        builder.Services.AddSingleton(TimeProvider.System);

        builder.Services.AddSingleton<ITodoItemRepository>(provider =>
            new TodoItemRepository(todoItems: CreateTodoItemsForSeeding())
        );

        builder.Services.AddSingleton<IDinosaurRepository>(provider =>
            new DinosaurRepository(dinosaurs: CreateDinosaursForSeeding())
        );
    }

    private static List<TodoItem> CreateTodoItemsForSeeding()
    {
        return new List<TodoItem>{
                    new TodoItem{
                        Id = 1,
                        Done = true,
                        Note = "First item",
                        Priority = PriorityLevel.High,
                        Reminder = null,
                        Title = "First item title"
                    },
                    new TodoItem{
                        Id = 2,
                        Done = false,
                        Note = "Second item",
                        Priority = PriorityLevel.Low,
                        Reminder = DateTime.Today,
                        Title = "Second item title"
                    }
                };
    }

    private static List<Dinosaur> CreateDinosaursForSeeding()
    {
        var dinosaurs = new List<Dinosaur>();
        var csvPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Dinosaurs.csv");
        var lines = File.ReadAllLines(csvPath).Skip(1);
        foreach (var line in lines)
        {
            var values = line.Split(',');
            var dinosaur = new Dinosaur
            {
                Id = int.Parse(values[0]),
                Name = values[1],
                Species = values[2],
                Sex = values[3],
                CountryOfOrigin = values[4],
                NumberOfScales = int.Parse(values[5])
            };
            dinosaurs.Add(dinosaur);
        }
        return dinosaurs;
    }
}
