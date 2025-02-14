using Copilot.Application.Dinosaurs.Commands.CreateDinosaur;
using Copilot.Application.Dinosaurs.Commands.DeleteDinosaur;
using Copilot.Application.Dinosaurs.Commands.UpdateDinosaur;
using Copilot.Application.Dinosaurs.Queries.GetDinosaur;
using Copilot.Application.Dinosaurs.Queries.GetDinosaurs;
using Copilot.Domain.Entities;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Copilot.Web.Endpoints;

public class Dinosaurs : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            .MapGet(GetDinosaurs)
            .MapGet(GetDinosaur, "{id}")
            .MapPost(CreateDinosaur)
            .MapPut(UpdateDinosaur, "{id}")
            .MapDelete(DeleteDinosaur, "{id}");
    }

    public async Task<Ok<List<Dinosaur>>> GetDinosaurs(ISender sender)
    {
        var result = await sender.Send(new GetDinosaursQuery());
        return TypedResults.Ok(result);
    }

    public async Task<Results<Ok<Dinosaur>, NotFound>> GetDinosaur(ISender sender, int id)
    {
        var result = await sender.Send(new GetDinosaurQuery(id));
        return result is not null ? TypedResults.Ok(result) : TypedResults.NotFound();
    }

    public async Task<Created<int>> CreateDinosaur(ISender sender, CreateDinosaurCommand command)
    {
        var createdDinosaur = await sender.Send(command);
        return TypedResults.Created($"/{nameof(Dinosaurs)}/{createdDinosaur.Id}", createdDinosaur.Id);
    }

    public async Task<Results<NoContent, BadRequest>> UpdateDinosaur(ISender sender, int id, UpdateDinosaurCommand command)
    {
        if (id != command.Id) return TypedResults.BadRequest();

        await sender.Send(command);
        return TypedResults.NoContent();
    }

    public async Task<NoContent> DeleteDinosaur(ISender sender, int id)
    {
        await sender.Send(new DeleteDinosaurCommand(id));
        return TypedResults.NoContent();
    }
}
