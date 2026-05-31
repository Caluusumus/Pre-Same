using ClassModels;
using Microsoft.AspNetCore.Http.HttpResults;
using MuseoCivicoAurora.Service;

namespace MuseoCivicoAurora.Endpoints;

public static class ExhibitionsEndpoint
{
    public static IEndpointRouteBuilder MapExhibitionsEndpoints(
                                                this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("api/exhibitions");

        group.MapGet("", GetAllAsync);
        group.MapGet("{id:guid}", GetByIdAsync);
        group.MapPost("", AddAsync);
        group.MapPut("{id:guid}", UpdateAsync);
        group.MapDelete("{id:guid}", DeleteAsync);

        return app;
    }

    private static async Task<Ok<IEnumerable<Exhibition>>> GetAllAsync(ExhibitionsService service)
    {
        var exhibitions = await service.GetExhibitionsAsync();

        return TypedResults.Ok(exhibitions);
    }

    private static async Task<Results<NotFound, Ok<Exhibition>>> GetByIdAsync(Guid id, ExhibitionsService service)
    {
        var exhibition = await service.GetExhibitionByIdAsync(id);
        if (exhibition is null)
            return TypedResults.NotFound();

        return TypedResults.Ok(exhibition);
    }

    private static async Task<Created<Exhibition>> AddAsync(Exhibition exhibition, ExhibitionsService service)
    {
        await service.AddExhibitionAsync(exhibition);

        return TypedResults.Created($"/api/exhibitions/{exhibition.Id}", exhibition);
    }

    private static async Task<Results<NoContent, NotFound>> UpdateAsync(Guid id, Exhibition exhibition, ExhibitionsService service)
    {
        var found = await service.GetExhibitionByIdAsync(id);
        if (found is null)
            return TypedResults.NotFound();

        await service.UpdateExhibitionAsync(exhibition);

        return TypedResults.NoContent();
    }

    private static async Task<Results<NoContent, NotFound>> DeleteAsync(Guid id, ExhibitionsService service)
    {
        var found = await service.GetExhibitionByIdAsync(id);
        if (found is null)
            return TypedResults.NotFound();

        await service.DeleteExhibitionByIdAsync(id);

        return TypedResults.NoContent();
    }
}
