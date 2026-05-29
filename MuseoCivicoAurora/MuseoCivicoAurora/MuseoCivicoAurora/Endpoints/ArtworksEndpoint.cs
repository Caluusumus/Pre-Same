using ClassModels;
using Microsoft.AspNetCore.Http.HttpResults;
using MuseoCivicoAurora.Service;

namespace MuseoCivicoAurora.Endpoints;

public static class ArtworksEndpoint
{
    public static IEndpointRouteBuilder MapOpereEndpoints(
                                                this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("api/opere");

        group.MapGet("", GetAllAsync);
        group.MapGet("{id:int}", GetByIdAsync);
        group.MapPost("", AddAsync);
        group.MapPut("{id:int}", UpdateAsync);
        group.MapDelete("{id:int}", DeleteAsync);

        return app;
    }

    private static async Task<Ok<IEnumerable<Opera>>> GetAllAsync(OpereService service)
    {
        var opere = await service.GetOpereAsync();

        return TypedResults.Ok(opere);
    }

    private static async Task<Results<NotFound, Ok<Opera>>> GetByIdAsync(Guid id, OpereService service)
    {
        var opera = await service.GetOperaByIdAsync(id);
        if (opera is null)
            return TypedResults.NotFound();

        return TypedResults.Ok(opera);
    }

    private static async Task<Created<Opera>> AddAsync(Opera opera, OpereService service)
    {
        await service.AddOperaAsync(opera);

        return TypedResults.Created($"/api/products/{opera.Id}", opera);
    }

    private static async Task<Results<NoContent, NotFound>> UpdateAsync(Guid id, Opera opera, OpereService service)
    {
        var found = await service.GetOperaByIdAsync(id);
        if (found is null)
            return TypedResults.NotFound();

        await service.UpdateOperaAsync(opera);

        return TypedResults.NoContent();
    }

    private static async Task<Results<NoContent, NotFound>> DeleteAsync(Guid id, OpereService service)
    {
        var found = await service.GetOperaByIdAsync(id);
        if (found is null)
            return TypedResults.NotFound();

        await service.DeleteOperaByIdAsync(id);

        return TypedResults.NoContent();
    }
}
