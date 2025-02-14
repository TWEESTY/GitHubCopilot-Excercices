using Copilot.Application.Common.Models;
using Copilot.Application.TodoItems.Commands.CreateTodoItem;
using Copilot.Application.TodoItems.Commands.DeleteTodoItem;
using Copilot.Application.TodoItems.Commands.UpdateTodoItem;
using Copilot.Application.TodoItems.Commands.UpdateTodoItemDetail;
using Copilot.Application.TodoItems.Queries.GetTodoItemsWithPagination;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Copilot.Web.Endpoints;

public class TodoItems : EndpointGroupBase
{
    /// <summary>
    /// Maps the endpoints for the TodoItems group.
    /// </summary>
    /// <param name="app">The WebApplication instance.</param>
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            .MapGet(GetTodoItemsWithPagination)
            .MapPost(CreateTodoItem)
            .MapPut(UpdateTodoItem, "{id}")
            .MapPut(UpdateTodoItemDetail, "UpdateDetail/{id}")
            .MapDelete(DeleteTodoItem, "{id}");
    }

    /// <summary>
    /// Retrieves a paginated list of TodoItems.
    /// </summary>
    /// <param name="sender">The sender instance.</param>
    /// <param name="query">The query parameters for retrieving TodoItems.</param>
    /// <returns>A response containing the paginated list of TodoItems.</returns>
    public async Task<Ok<PaginatedList<TodoItemBriefDto>>> GetTodoItemsWithPagination(ISender sender, [AsParameters] GetTodoItemsWithPaginationQuery query)
    {
        var result = await sender.Send(query);

        return TypedResults.Ok(result);
    }

    /// <summary>
    /// Creates a new TodoItem.
    /// </summary>
    /// <param name="sender">The sender instance.</param>
    /// <param name="command">The command for creating a TodoItem.</param>
    /// <returns>A response containing the ID of the created TodoItem.</returns>
    public async Task<Created<int>> CreateTodoItem(ISender sender, CreateTodoItemCommand command)
    {
        var id = await sender.Send(command);

        return TypedResults.Created($"/{nameof(TodoItems)}/{id}", id);
    }

    /// <summary>
    /// Updates a TodoItem.
    /// </summary>
    /// <param name="sender">The sender instance.</param>
    /// <param name="id">The ID of the TodoItem to update.</param>
    /// <param name="command">The command for updating the TodoItem.</param>
    /// <returns>A response indicating the success or failure of the update operation.</returns>
    public async Task<Results<NoContent, BadRequest>> UpdateTodoItem(ISender sender, int id, UpdateTodoItemCommand command)
    {
        if (id != command.Id) return TypedResults.BadRequest();

        await sender.Send(command);

        return TypedResults.NoContent();
    }

    /// <summary>
    /// Updates the details of a TodoItem.
    /// </summary>
    /// <param name="sender">The sender instance.</param>
    /// <param name="id">The ID of the TodoItem to update.</param>
    /// <param name="command">The command for updating the TodoItem details.</param>
    /// <returns>A response indicating the success or failure of the update operation.</returns>
    public async Task<Results<NoContent, BadRequest>> UpdateTodoItemDetail(ISender sender, int id, UpdateTodoItemDetailCommand command)
    {
        if (id != command.Id) return TypedResults.BadRequest();

        await sender.Send(command);

        return TypedResults.NoContent();
    }

    /// <summary>
    /// Deletes a TodoItem.
    /// </summary>
    /// <param name="sender">The sender instance.</param>
    /// <param name="id">The ID of the TodoItem to delete.</param>
    /// <returns>A response indicating the success or failure of the delete operation.</returns>
    public async Task<NoContent> DeleteTodoItem(ISender sender, int id)
    {
        await sender.Send(new DeleteTodoItemCommand(id));

        return TypedResults.NoContent();
    }
}
