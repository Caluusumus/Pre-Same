using MuseoCivicoAurora.shared;
using MuseoCivicoAurora.Service;

namespace MuseoCivicoAurora.Endpoints;

public static class ToursEndpoint
{
    public static void MapToursEndpoint(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/tours").WithTags("Tours").DisableAntiforgery();

        group.MapGet("/", async (IToursService service) =>
        {
            var tours = await service.GetToursAsync();
            return Results.Ok(tours);
        });

        group.MapGet("/{id:guid}", async (Guid id, IToursService service) =>
        {
            var tour = await service.GetTourByIdAsync(id);
            return tour is null
                ? Results.NotFound()
                : Results.Ok(tour);
        });

        group.MapPost("/", async (Tour tour, IToursService service) =>
        {
            try
            {
                await service.AddTourAsync(tour);
                return Results.Created($"/api/tours/{tour.Id}", tour);
            }
            catch (Exception ex)
            {
                return Results.BadRequest(new { message = ex.Message });
            }
        });

        group.MapPut("/{id:guid}", async (Guid id, Tour tour, IToursService service) =>
        {
            try
            {
                var existing = await service.GetTourByIdAsync(id);
                if (existing is null) return Results.NotFound();

                tour.Id = id;
                await service.UpdateTourAsync(tour);
                return Results.Ok(tour);
            }
            catch (Exception ex)
            {
                return Results.BadRequest(new { message = ex.Message });
            }
        });

        group.MapDelete("/{id:guid}", async (Guid id, IToursService service) =>
        {
            try
            {
                var existing = await service.GetTourByIdAsync(id);
                if (existing is null) return Results.NotFound();

                await service.DeleteTourByIdAsync(id);
                return Results.NoContent();
            }
            catch (Exception ex)
            {
                return Results.BadRequest(new { message = ex.Message });
            }
        });
    }
}