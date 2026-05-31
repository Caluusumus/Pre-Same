using ClassModels;
using Microsoft.AspNetCore.Http.HttpResults;
using MuseoCivicoAurora.Service;

namespace MuseoCivicoAurora.Endpoints;

public static class ToursEndpoint
{
    public static IEndpointRouteBuilder MapToursEndpoint(
                                                this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("api/tours");

        group.MapGet("", GetAllAsync);
        group.MapGet("{id:guid}", GetByIdAsync);
        group.MapPost("", AddAsync);
        group.MapPut("{id:guid}", UpdateAsync);
        group.MapDelete("{id:guid}", DeleteAsync);

        return app;
    }

    private static async Task<Ok<IEnumerable<Tour>>> GetAllAsync(ToursService service)
    {
        var tour = await service.GetToursAsync();

        return TypedResults.Ok(tour);
    }

    private static async Task<Results<NotFound, Ok<Tour>>> GetByIdAsync(Guid id, ToursService service)
    {
        var tour = await service.GetTourByIdAsync(id);
        if (tour is null)
            return TypedResults.NotFound();

        return TypedResults.Ok(tour);
    }

    private static async Task<Created<Tour>> AddAsync(Tour tour, ToursService service)
    {
        await service.AddTourAsync(tour);

        return TypedResults.Created($"/api/tours/{tour.Id}", tour);
    }

    private static async Task<Results<NoContent, NotFound>> UpdateAsync(Guid id, Tour tour, ToursService service)
    {
        var found = await service.GetTourByIdAsync(id);
        if (found is null)
            return TypedResults.NotFound();

        await service.UpdateTourAsync(tour);

        return TypedResults.NoContent();
    }

    private static async Task<Results<NoContent, NotFound>> DeleteAsync(Guid id, ToursService service)
    {
        var found = await service.GetTourByIdAsync(id);
        if (found is null)
            return TypedResults.NotFound();

        await service.DeleteTourByIdAsync(id);

        return TypedResults.NoContent();
    }
}
