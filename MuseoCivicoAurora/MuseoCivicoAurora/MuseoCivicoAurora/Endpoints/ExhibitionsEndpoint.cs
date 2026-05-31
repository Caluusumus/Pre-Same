using ClassModels;
using MuseoCivicoAurora.Service;

namespace MuseoCivicoAurora.Endpoints;

public static class ExhibitionsEndpoints
{
    public static void MapExhibitionsEndpoints(this IEndpointRouteBuilder app)
    {
        // ==========================================
        // 1. ENDPOINT PER LE MOSTRE (EXHIBITIONS)
        // ==========================================
        var group = app.MapGroup("/api/exhibitions").WithTags("Exhibitions").DisableAntiforgery();

        group.MapGet("/", async (IExhibitionsService service) =>
        {
            var exhibitions = await service.GetExhibitionsAsync();
            return Results.Ok(exhibitions);
        });

        group.MapGet("/{id:guid}", async (Guid id, IExhibitionsService service) =>
        {
            var exhibition = await service.GetExhibitionByIdAsync(id);
            return exhibition is null
                ? Results.NotFound(new { message = $"Mostra con id {id} non trovata." })
                : Results.Ok(exhibition);
        });

        group.MapPost("/", async (Exhibition exhibition, IExhibitionsService service) =>
        {
            try
            {
                // Nei tuoi servizi originali AddExhibitionAsync non ritorna un valore, 
                // quindi aspettiamo che finisca e poi ritorniamo l'oggetto ricevuto.
                await service.AddExhibitionAsync(exhibition);
                return Results.Created($"/api/exhibitions/{exhibition.Id}", exhibition);
            }
            catch (Exception ex)
            {
                return Results.BadRequest(new { message = ex.Message });
            }
        });

        group.MapPut("/{id:guid}", async (Guid id, Exhibition exhibition, IExhibitionsService service) =>
        {
            try
            {
                // Prima controlliamo se la mostra esiste davvero
                var existing = await service.GetExhibitionByIdAsync(id);
                if (existing is null)
                {
                    return Results.NotFound(new { message = $"Mostra con id {id} non trovata." });
                }

                // Assicuriamoci che l'ID dell'oggetto corrisponda a quello dell'URL
                exhibition.Id = id;

                await service.UpdateExhibitionAsync(exhibition);
                
                return Results.Ok(exhibition);
            }
            catch (Exception ex)
            {
                return Results.BadRequest(new { message = ex.Message });
            }
        });

        group.MapDelete("/{id:guid}", async (Guid id, IExhibitionsService service) =>
        {
            try
            {
                var existing = await service.GetExhibitionByIdAsync(id);
                if (existing is null)
                {
                    return Results.NotFound(new { message = $"Mostra con id {id} non trovata." });
                }

                await service.DeleteExhibitionByIdAsync(id);
                return Results.NoContent();
            }
            catch (Exception ex)
            {
                return Results.BadRequest(new { message = ex.Message });
            }
        });

        // ==========================================
        // 2. EVENTUALI ENDPOINT SECONDARI (ES. MENU A TENDINA)
        // ==========================================
        // Se in futuro ti servirà recuperare dati correlati per i menu a tendina 
        // (ad esempio, uno stato o delle categorie), potrai aggiungerli qui sotto
        // esattamente come nel tuo esempio di "ApplicationDbContext".
    }
}