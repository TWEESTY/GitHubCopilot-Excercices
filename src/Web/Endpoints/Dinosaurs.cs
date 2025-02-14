using Copilot.Application.Dinosaurs.Commands.CreateDinosaur;
using Copilot.Application.Dinosaurs.Commands.DeleteDinosaur;
using Copilot.Application.Dinosaurs.Commands.UpdateDinosaur;
using Copilot.Application.Dinosaurs.Queries.GetDinosaur;
using Copilot.Application.Dinosaurs.Queries.GetDinosaurs;
using Copilot.Domain.Entities;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Copilot.Web.Endpoints;

/// <summary>
/// Represents the endpoint group for managing dinosaurs.
/// </summary>
public class Dinosaurs : EndpointGroupBase
{
    /// <summary>
    /// Maps the endpoints for managing dinosaurs.
    /// </summary>
    /// <param name="app">The web application.</param>
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            .MapGet(GetDinosaurs)
            .MapGet(GetDinosaur, "{id}")
            .MapPost(CreateDinosaur)
            .MapPut(UpdateDinosaur, "{id}")
            .MapDelete(DeleteDinosaur, "{id}");
    }

    /// <summary>
    /// Gets the list of dinosaurs.
    /// </summary>
    /// <param name="sender">The sender.</param>
    /// <returns>The list of dinosaurs.</returns>
    public async Task<Ok<List<Dinosaur>>> GetDinosaurs(ISender sender)
    {
        var result = await sender.Send(new GetDinosaursQuery());
        return TypedResults.Ok(result);
    }

    /// <summary>
    /// Gets a dinosaur by ID.
    /// </summary>
    /// <param name="sender">The sender.</param>
    /// <param name="id">The ID of the dinosaur.</param>
    /// <returns>The dinosaur if found, otherwise NotFound.</returns>
    public async Task<Results<Ok<Dinosaur>, NotFound>> GetDinosaur(ISender sender, int id)
    {
        var result = await sender.Send(new GetDinosaurQuery(id));
        return result is not null ? TypedResults.Ok(result) : TypedResults.NotFound();
    }

    /// <summary>
    /// Creates a new dinosaur.
    /// </summary>
    /// <param name="sender">The sender.</param>
    /// <param name="command">The create dinosaur command.</param>
    /// <returns>The ID of the created dinosaur.</returns>
    public async Task<Created<int>> CreateDinosaur(ISender sender, CreateDinosaurCommand command)
    {
        var createdDinosaur = await sender.Send(command);
        return TypedResults.Created($"/{nameof(Dinosaurs)}/{createdDinosaur.Id}", createdDinosaur.Id);
    }

    /// <summary>
    /// Updates a dinosaur.
    /// </summary>
    /// <param name="sender">The sender.</param>
    /// <param name="id">The ID of the dinosaur.</param>
    /// <param name="command">The update dinosaur command.</param>
    /// <returns>NoContent if successful, otherwise BadRequest.</returns>
    public async Task<Results<NoContent, BadRequest>> UpdateDinosaur(ISender sender, int id, UpdateDinosaurCommand command)
    {
        if (id != command.Id) return TypedResults.BadRequest();

        await sender.Send(command);
        return TypedResults.NoContent();
    }

    /// <summary>
    /// Deletes a dinosaur.
    /// </summary>
    /// <param name="sender">The sender.</param>
    /// <param name="id">The ID of the dinosaur.</param>
    /// <returns>NoContent if successful.</returns>
    public async Task<NoContent> DeleteDinosaur(ISender sender, int id)
    {
        await sender.Send(new DeleteDinosaurCommand(id));
        return TypedResults.NoContent();
    }
}
