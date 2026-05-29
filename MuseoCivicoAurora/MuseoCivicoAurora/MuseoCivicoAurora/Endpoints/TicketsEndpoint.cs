using ClassModels;
using Microsoft.AspNetCore.Http.HttpResults;
using MuseoCivicoAurora.Service;

namespace MuseoCivicoAurora.Endpoints;

public static class TicketsEndpoint
{
    public static IEndpointRouteBuilder MapBigliettiEndpoints(
                                                this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("api/biglietti");

        group.MapGet("", GetAllAsync);
        group.MapGet("{id:int}", GetByIdAsync);
        group.MapPost("", AddAsync);
        group.MapPut("{id:int}", UpdateAsync);
        group.MapDelete("{id:int}", DeleteAsync);

        return app;
    }

    private static async Task<Ok<IEnumerable<Biglietto>>> GetAllAsync(BigliettiService service)
    {
        var opere = await service.GetBigliettiAsync();

        return TypedResults.Ok(opere);
    }

    private static async Task<Results<NotFound, Ok<Biglietto>>> GetByIdAsync(Guid id, BigliettiService service)
    {
        var biglietto = await service.GetBigliettoByIdAsync(id);
        if (biglietto is null)
            return TypedResults.NotFound();

        return TypedResults.Ok(biglietto);
    }

    private static async Task<Created<Biglietto>> AddAsync(Biglietto biglietto, BigliettiService service)
    {
        await service.AddBigliettoAsync(biglietto);

        return TypedResults.Created($"/api/products/{biglietto.Id}", biglietto);
    }

    private static async Task<Results<NoContent, NotFound>> UpdateAsync(Guid id, Biglietto biglietto, BigliettiService service)
    {
        var found = await service.GetBigliettoByIdAsync(id);
        if (found is null)
            return TypedResults.NotFound();

        await service.UpdateBigliettoAsync(biglietto);

        return TypedResults.NoContent();
    }

    private static async Task<Results<NoContent, NotFound>> DeleteAsync(Guid id, BigliettiService service)
    {
        var found = await service.GetBigliettoByIdAsync(id);
        if (found is null)
            return TypedResults.NotFound();

        await service.DeleteBigliettoByIdAsync(id);

        return TypedResults.NoContent();
    }
}
