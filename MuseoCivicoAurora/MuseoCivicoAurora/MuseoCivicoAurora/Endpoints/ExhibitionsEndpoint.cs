using ClassModels;
using Microsoft.AspNetCore.Http.HttpResults;
using MuseoCivicoAurora.Service;

namespace MuseoCivicoAurora.Endpoints;

public static class ExhibitionsEndpoint
{
    public static IEndpointRouteBuilder MapMostreEndpoints(
                                                this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("api/mostre");

        group.MapGet("", GetAllAsync);
        group.MapGet("{id:int}", GetByIdAsync);
        group.MapPost("", AddAsync);
        group.MapPut("{id:int}", UpdateAsync);
        group.MapDelete("{id:int}", DeleteAsync);

        return app;
    }

    private static async Task<Ok<IEnumerable<Mostra>>> GetAllAsync(MostreService service)
    {
        var mostre = await service.GetMostreAsync();

        return TypedResults.Ok(mostre);
    }

    private static async Task<Results<NotFound, Ok<Mostra>>> GetByIdAsync(Guid id, MostreService service)
    {
        var mostra = await service.GetMostraByIdAsync(id);
        if (mostra is null)
            return TypedResults.NotFound();

        return TypedResults.Ok(mostra);
    }

    private static async Task<Created<Mostra>> AddAsync(Mostra mostra, MostreService service)
    {
        await service.AddMostraAsync(mostra);

        return TypedResults.Created($"/api/products/{mostra.Id}", mostra);
    }

    private static async Task<Results<NoContent, NotFound>> UpdateAsync(Guid id, Mostra mostra, MostreService service)
    {
        var found = await service.GetMostraByIdAsync(id);
        if (found is null)
            return TypedResults.NotFound();

        await service.UpdateMostraAsync(mostra);

        return TypedResults.NoContent();
    }

    private static async Task<Results<NoContent, NotFound>> DeleteAsync(Guid id, MostreService service)
    {
        var found = await service.GetMostraByIdAsync(id);
        if (found is null)
            return TypedResults.NotFound();

        await service.DeleteMostraByIdAsync(id);

        return TypedResults.NoContent();
    }
}
