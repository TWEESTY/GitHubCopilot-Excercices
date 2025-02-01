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
    }

    private static List<TodoItem> CreateTodoItemsForSeeding(){
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
}
