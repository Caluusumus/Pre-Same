using ClassModels;
using Microsoft.AspNetCore.Http.HttpResults;
using MuseoCivicoAurora.Service;

namespace MuseoCivicoAurora.Endpoints;

public static class TicketsEndpoint
{
    public static IEndpointRouteBuilder MapTicketsEndpoint(
                                                this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("api/tickets");

        group.MapGet("", GetAllAsync);
        group.MapGet("{id:guid}", GetByIdAsync);
        group.MapPost("", AddAsync);
        group.MapPut("{id:guid}", UpdateAsync);
        group.MapDelete("{id:guid}", DeleteAsync);

        return app;
    }

    private static async Task<Ok<IEnumerable<Ticket>>> GetAllAsync(TicketsService service)
    {
        var tickets = await service.GetTicketsAsync();

        return TypedResults.Ok(tickets);
    }

    private static async Task<Results<NotFound, Ok<Ticket>>> GetByIdAsync(Guid id, TicketsService service)
    {
        var ticket = await service.GetTicketByIdAsync(id);
        if (ticket is null)
            return TypedResults.NotFound();

        return TypedResults.Ok(ticket);
    }

    private static async Task<Created<Ticket>> AddAsync(Ticket ticket, TicketsService service)
    {
        await service.AddTicketAsync(ticket);

        return TypedResults.Created($"/api/tickets/{ticket.Id}", ticket);
    }

    private static async Task<Results<NoContent, NotFound>> UpdateAsync(Guid id, Ticket ticket, TicketsService service)
    {
        var found = await service.GetTicketByIdAsync(id);
        if (found is null)
            return TypedResults.NotFound();

        await service.UpdateTicketAsync(ticket);

        return TypedResults.NoContent();
    }

    private static async Task<Results<NoContent, NotFound>> DeleteAsync(Guid id, TicketsService service)
    {
        var found = await service.GetTicketByIdAsync(id);
        if (found is null)
            return TypedResults.NotFound();

        await service.DeleteTicketByIdAsync(id);

        return TypedResults.NoContent();
    }
}
