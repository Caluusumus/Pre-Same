using MuseoCivicoAurora.shared;
using MuseoCivicoAurora.Service;

namespace MuseoCivicoAurora.Endpoints;

public static class BookingsEndpoint
{
    public static void MapBookingsEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/bookings").WithTags("Bookings").DisableAntiforgery();

        group.MapGet("/", async (IBookingsService service) =>
        {
            var bookings = await service.GetBookingsAsync();
            return Results.Ok(bookings);
        });

        group.MapGet("/{id:guid}", async (Guid id, IBookingsService service) =>
        {
            var booking = await service.GetBookingByIdAsync(id);
            return booking is null
                ? Results.NotFound()
                : Results.Ok(booking);
        });

        group.MapPost("/", async (Booking booking, IBookingsService service) =>
        {
            try
            {
                await service.AddBookingAsync(booking);
                return Results.Created($"/api/bookings/{booking.Id}", booking);
            }
            catch (Exception ex)
            {
                return Results.BadRequest(new { message = ex.Message });
            }
        });

        group.MapPut("/{id:guid}", async (Guid id, Booking booking, IBookingsService service) =>
        {
            try
            {
                var existing = await service.GetBookingByIdAsync(id);
                if (existing is null) return Results.NotFound();

                booking.Id = id;
                await service.UpdateBookingAsync(booking);
                return Results.Ok(booking);
            }
            catch (Exception ex)
            {
                return Results.BadRequest(new { message = ex.Message });
            }
        });

        group.MapDelete("/{id:guid}", async (Guid id, IBookingsService service) =>
        {
            try
            {
                var existing = await service.GetBookingByIdAsync(id);
                if (existing is null) return Results.NotFound();

                await service.DeleteBookingByIdAsync(id);
                return Results.NoContent();
            }
            catch (Exception ex)
            {
                return Results.BadRequest(new { message = ex.Message });
            }
        });
    }
}