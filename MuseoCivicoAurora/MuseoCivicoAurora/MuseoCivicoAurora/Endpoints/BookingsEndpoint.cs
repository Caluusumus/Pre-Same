using ClassModels;
using Microsoft.AspNetCore.Http.HttpResults;
using MuseoCivicoAurora.Service;

namespace MuseoCivicoAurora.Endpoints;

public static class BookingsEndpoint
{
    public static IEndpointRouteBuilder MapBookingsEndpoints(
                                                this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("api/bookings");

        group.MapGet("", GetAllAsync);
        group.MapGet("{id:guid}", GetByIdAsync);
        group.MapPost("", AddAsync);
        group.MapPut("{id:guid}", UpdateAsync);
        group.MapDelete("{id:guid}", DeleteAsync);

        return app;
    }

    private static async Task<Ok<IEnumerable<Booking>>> GetAllAsync(BookingsService service)
    {
        var bookings = await service.GetBookingsAsync();

        return TypedResults.Ok(bookings);
    }

    private static async Task<Results<NotFound, Ok<Booking>>> GetByIdAsync(Guid id, BookingsService service)
    {
        var booking = await service.GetBookingByIdAsync(id);
        if (booking is null)
            return TypedResults.NotFound();

        return TypedResults.Ok(booking);
    }

    private static async Task<Created<Booking>> AddAsync(Booking booking, BookingsService service)
    {
        await service.AddBookingAsync(booking);

        return TypedResults.Created($"/api/bookings/{booking.Id}", booking);
    }

    private static async Task<Results<NoContent, NotFound>> UpdateAsync(Guid id, Booking booking, BookingsService service)
    {
        var found = await service.GetBookingByIdAsync(id);
        if (found is null)
            return TypedResults.NotFound();

        await service.UpdateBookingAsync(booking);

        return TypedResults.NoContent();
    }

    private static async Task<Results<NoContent, NotFound>> DeleteAsync(Guid id, BookingsService service)
    {
        var found = await service.GetBookingByIdAsync(id);
        if (found is null)
            return TypedResults.NotFound();

        await service.DeleteBookingByIdAsync(id);

        return TypedResults.NoContent();
    }
}
