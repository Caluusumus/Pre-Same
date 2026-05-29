using ClassModels;
using Microsoft.AspNetCore.Http.HttpResults;
using MuseoCivicoAurora.Service;

namespace MuseoCivicoAurora.Endpoints;

public static class BookingsEndpoint
{
    public static IEndpointRouteBuilder MapPrenotazioniEndpoints(
                                                this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("api/prenotazioni");

        group.MapGet("", GetAllAsync);
        group.MapGet("{id:int}", GetByIdAsync);
        group.MapPost("", AddAsync);
        group.MapPut("{id:int}", UpdateAsync);
        group.MapDelete("{id:int}", DeleteAsync);

        return app;
    }

    private static async Task<Ok<IEnumerable<Prenotazione>>> GetAllAsync(PrenotazioniService service)
    {
        var prenotazioni = await service.GetPrenotazioniAsync();

        return TypedResults.Ok(prenotazioni);
    }

    private static async Task<Results<NotFound, Ok<Prenotazione>>> GetByIdAsync(Guid id, PrenotazioniService service)
    {
        var prenotazione = await service.GetPrenotazioneByIdAsync(id);
        if (prenotazione is null)
            return TypedResults.NotFound();

        return TypedResults.Ok(prenotazione);
    }

    private static async Task<Created<Prenotazione>> AddAsync(Prenotazione prenotazione, PrenotazioniService service)
    {
        await service.AddPrenotazioneAsync(prenotazione);

        return TypedResults.Created($"/api/products/{prenotazione.Id}", prenotazione);
    }

    private static async Task<Results<NoContent, NotFound>> UpdateAsync(Guid id, Prenotazione prenotazione, PrenotazioniService service)
    {
        var found = await service.GetPrenotazioneByIdAsync(id);
        if (found is null)
            return TypedResults.NotFound();

        await service.UpdatePrenotazioneAsync(prenotazione);

        return TypedResults.NoContent();
    }

    private static async Task<Results<NoContent, NotFound>> DeleteAsync(Guid id, PrenotazioniService service)
    {
        var found = await service.GetPrenotazioneByIdAsync(id);
        if (found is null)
            return TypedResults.NotFound();

        await service.DeletePrenotazioneByIdAsync(id);

        return TypedResults.NoContent();
    }
}
