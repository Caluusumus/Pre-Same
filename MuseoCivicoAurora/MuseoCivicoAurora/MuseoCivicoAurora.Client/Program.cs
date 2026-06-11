using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MuseoCivicoAurora.Client.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddAuthorizationCore();
builder.Services.AddCascadingAuthenticationState();
builder.Services.AddAuthenticationStateDeserialization();
builder.Services.AddScoped<IExhibitionApiClient,ExhibitionApiClient>();
builder.Services.AddScoped<IArtworkApiClient, ArtworkApiClient>();
builder.Services.AddScoped<IBookingApiClient, BookingApiClient>();
builder.Services.AddScoped<ITicketApiClient, TicketApiClient>();
builder.Services.AddScoped<ITourApiClient, TourApiClient>();

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });


await builder.Build().RunAsync();
