using ClassModels;
using MuseoCivicoAurora.Service;

namespace MuseoCivicoAurora.Endpoints;

public static class TicketsEndpoint
{
    public static void MapTicketsEndpoint(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/tickets").WithTags("Tickets").DisableAntiforgery();

        group.MapGet("/", async (ITicketsService service) =>
        {
            var tickets = await service.GetTicketsAsync();
            return Results.Ok(tickets);
        });

        group.MapGet("/{id:guid}", async (Guid id, ITicketsService service) =>
        {
            var ticket = await service.GetTicketByIdAsync(id);
            return ticket is null
                ? Results.NotFound()
                : Results.Ok(ticket);
        });

        group.MapPost("/", async (Ticket ticket, ITicketsService service) =>
        {
            try
            {
                await service.AddTicketAsync(ticket);
                return Results.Created($"/api/tickets/{ticket.Id}", ticket);
            }
            catch (Exception ex)
            {
                return Results.BadRequest(new { message = ex.Message });
            }
        });

        group.MapPut("/{id:guid}", async (Guid id, Ticket ticket, ITicketsService service) =>
        {
            try
            {
                var existing = await service.GetTicketByIdAsync(id);
                if (existing is null) return Results.NotFound();

                ticket.Id = id;
                await service.UpdateTicketAsync(ticket);
                return Results.Ok(ticket);
            }
            catch (Exception ex)
            {
                return Results.BadRequest(new { message = ex.Message });
            }
        });

        group.MapDelete("/{id:guid}", async (Guid id, ITicketsService service) =>
        {
            try
            {
                var existing = await service.GetTicketByIdAsync(id);
                if (existing is null) return Results.NotFound();

                await service.DeleteTicketByIdAsync(id);
                return Results.NoContent();
            }
            catch (Exception ex)
            {
                return Results.BadRequest(new { message = ex.Message });
            }
        });
    }
}