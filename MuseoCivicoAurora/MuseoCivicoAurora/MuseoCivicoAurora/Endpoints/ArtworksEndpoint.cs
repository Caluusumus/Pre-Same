using ClassModels;
using Microsoft.AspNetCore.Http.HttpResults;
using MuseoCivicoAurora.Service;

namespace MuseoCivicoAurora.Endpoints;

public static class ArtworksEndpoint
{
    public static IEndpointRouteBuilder MapArtWorksEndpoint(
                                                this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("api/artworks");

        group.MapGet("", GetAllAsync);
        group.MapGet("{id:guid}", GetByIdAsync);
        group.MapPost("", AddAsync);
        group.MapPut("{id:guid}", UpdateAsync);
        group.MapDelete("{id:guid}", DeleteAsync);

        return app;
    }

    private static async Task<Ok<IEnumerable<Artwork>>> GetAllAsync(ArtworksService service)
    {
        var artworks = await service.GetArtworksAsync();

        return TypedResults.Ok(artworks);
    }

    private static async Task<Results<NotFound, Ok<Artwork>>> GetByIdAsync(Guid id, ArtworksService service)
    {
        var artwork = await service.GetArtworkByIdAsync(id);
        if (artwork is null)
            return TypedResults.NotFound();

        return TypedResults.Ok(artwork);
    }

    private static async Task<Created<Artwork>> AddAsync(Artwork artwork, ArtworksService service)
    {
        await service.AddArtworkAsync(artwork);

        return TypedResults.Created($"/api/artworks/{artwork.Id}", artwork);
    }

    private static async Task<Results<NoContent, NotFound>> UpdateAsync(Guid id, Artwork artwork, ArtworksService service)
    {
        var found = await service.GetArtworkByIdAsync(id);
        if (found is null)
            return TypedResults.NotFound();

        await service.UpdateArtworkAsync(artwork);

        return TypedResults.NoContent();
    }

    private static async Task<Results<NoContent, NotFound>> DeleteAsync(Guid id, ArtworksService service)
    {
        var found = await service.GetArtworkByIdAsync(id);
        if (found is null)
            return TypedResults.NotFound();

        await service.DeleteArtworkByIdAsync(id);

        return TypedResults.NoContent();
    }
}
