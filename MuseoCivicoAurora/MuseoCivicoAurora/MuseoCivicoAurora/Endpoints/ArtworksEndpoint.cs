using MuseoCivicoAurora.shared;
using MuseoCivicoAurora.Service;

namespace MuseoCivicoAurora.Endpoints;

public static class ArtworksEndpoint
{
    public static void MapArtWorksEndpoint(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/artworks").WithTags("Artworks").DisableAntiforgery();

        group.MapGet("/", async (IArtworksService service) =>
        {
            var artworks = await service.GetArtworksAsync();
            return Results.Ok(artworks);
        });

        group.MapGet("/{id:guid}", async (Guid id, IArtworksService service) =>
        {
            var artwork = await service.GetArtworkByIdAsync(id);
            return artwork is null
                ? Results.NotFound()
                : Results.Ok(artwork);
        });

        group.MapPost("/", async (Artwork artwork, IArtworksService service) =>
        {
            try
            {
                await service.AddArtworkAsync(artwork);
                return Results.Created($"/api/artworks/{artwork.Id}", artwork);
            }
            catch (Exception ex)
            {
                return Results.BadRequest(new { message = ex.Message });
            }
        });

        group.MapPut("/{id:guid}", async (Guid id, Artwork artwork, IArtworksService service) =>
        {
            try
            {
                var existing = await service.GetArtworkByIdAsync(id);
                if (existing is null) return Results.NotFound();

                artwork.Id = id;
                await service.UpdateArtworkAsync(artwork);
                return Results.Ok(artwork);
            }
            catch (Exception ex)
            {
                return Results.BadRequest(new { message = ex.Message });
            }
        });

        group.MapDelete("/{id:guid}", async (Guid id, IArtworksService service) =>
        {
            try
            {
                var existing = await service.GetArtworkByIdAsync(id);
                if (existing is null) return Results.NotFound();

                await service.DeleteArtworkByIdAsync(id);
                return Results.NoContent();
            }
            catch (Exception ex)
            {
                return Results.BadRequest(new { message = ex.Message });
            }
        });
    }
}